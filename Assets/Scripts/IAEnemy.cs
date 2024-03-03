using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IAEnemy : MonoBehaviour
{
    public Transform raycastOrigin;

    public float speed;
    private float waitingTime = 0f;
    private float turnTheEnemy = 9f;
    public float horizontalWalk = -1f;
    private bool rightdirection = true;

    public float speedDetection;

    //private Vector3 puntoA;
    //private Vector3 puntoB;
    private float rightLimit;
    private float leftLimit;


    private float maxRange = 0.01f;

    private BoxCollider2D enemyCollider2D;

    [SerializeField] private Transform player;

    public LayerMask enemy;
    private float look = 5f;
    public int rayCount = 3;
    private Rigidbody2D rigidEnemy;

    public Color orangeColor = new Color(1f, 0.5f, 0.0f); // R: , G: , B: 

    //private float rangeAttack = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy = GetComponent<Rigidbody2D>();

        leftLimit = rigidEnemy.transform.position.x -8;
        rightLimit = rigidEnemy.transform.position.x +8;

        //look = rigidEnemy.transform.position.x + 4;
        
        //puntoA = new Vector3(rigidEnemy.transform.position.x - 10, 0, 0);
        //puntoB = new Vector3(rigidEnemy.transform.position.x +10, 0, 0);
    }

    private void Update()
    {
        /*
        
        if (transform.position.x == player.position.x + look)
        {
            Debug.Log("te sigo");
        }
        
       RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position,Vector2.right, look);
       if (Physics.Raycast ( transform.position, -transform.right, out hit, 5f))
       { }
       if (hit.collider.CompareTag("Player"))
       {
           Debug.Log("Follow");
       }
       else
       {
           Debug.Log("Serch");
       }
       
        Vector2 enemyPosition = rigidEnemy.position;
        float startAngle = transform.eulerAngles.z;
        float angleStep = 360f / rayCount;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
            Vector2 vertexPosition = enemyPosition + direction * (look / 4);
            RaycastHit2D hit = Physics2D.Raycast(vertexPosition, direction, look, enemy);
            Debug.DrawRay(vertexPosition , direction * look, Color.red);
            if (hit.collider != null)
            {
                Debug.Log("perseguir");
            }

        if (transform.position.x - leftLimit < maxRange)
        {
            TurnEnemy();
            Debug.Log("he llegado a la izq");
        }
        if (rightLimit - transform.position.x < maxRange)
        {
            TurnEnemy();
            Debug.Log("he llegado a la der");
        }

        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);

        }
        */

        waitingTime += Time.deltaTime;


        if (waitingTime >= turnTheEnemy)
        {
            TurnEnemy();
            waitingTime = 0f; // Reiniciar el contador de tiempo
        }

        // Actualizar la velocidad del enemigo
        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);

        Vector2 enemyPosition = rigidEnemy.position;
        float halfSize = look / 5;
        Vector2[] horizontalVertex = new Vector2[]
        {
            enemyPosition + new Vector2((-rigidEnemy.velocity.x > 0 ? -1.5f : 1.5f), 4),
            enemyPosition + new Vector2((-rigidEnemy.velocity.x > 0 ? -1.5f : 1.5f), 0),
            enemyPosition + new Vector2((-rigidEnemy.velocity.x > 0 ? -1.5f : 1.5f), 2),
        };

        foreach (Vector2 vertex in horizontalVertex)
        {
            for (int i = 0; i < rayCount; i++)
            {
                float angle = i * (360f / 2);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast(vertex, direction, 3.5f, enemy);
                //Debug.DrawRay(vertex, direction * 3.5f, Color.red);

                //Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    Debug.Log("Perseguir");
                    Debug.DrawRay(vertex, direction * 3.5f, Color.red);

                    // Calcular la dirección hacia el jugador
                    Vector2 playerDirection = (player.position - transform.position).normalized;
                    // Calcular la velocidad de movimiento del enemigo
                    Vector2 movement = playerDirection * speedDetection * Time.deltaTime;
                    // Rotar al enemigo si no está mirando hacia la dirección del jugador
                    if (!IsFacingPlayer(playerDirection))
                    {
                        //transform.rotation = Quaternion.Euler(0f, playerDirection.x < 180f ? 0f : 0f, 0f);
                        TurnEnemy();
                        waitingTime = 0f;
                    }
                    // Mover al enemigo en la dirección del jugador
                    rigidEnemy.MovePosition(rigidEnemy.position + movement);

                }
                else
                {
                    Debug.Log("Serch...");
                    Debug.DrawRay(vertex, direction * 3.5f, Color.green);
                }
            }
            /* Si el enemigo no está mirando hacia la posición del jugador, girar 180 grados en Y
             if (!IsFacingPlayer(playerDirection))
             {
                 transform.rotation = Quaternion.Euler(0f, playerDirection.x < 0 ? 180f : 0f, 0f);
             }
            */
        }

        Vector2[] attackVertex = new Vector2[]
        {
            enemyPosition + new Vector2((-rigidEnemy.velocity.x > 0 ? -1.5f : 0.5f), 1),
        };
        foreach (Vector2 vertex in attackVertex)
        {
            for (int i = 0; i < rayCount; i++)
            {
                float angle = i * (360f / 1);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast(vertex, direction, 1f, enemy);
                //Debug.DrawRay(vertex, direction * 3.5f, Color.red);

                //Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    Debug.Log("Attack");
                    Debug.DrawRay(vertex, direction * 1f, (orangeColor));
                }
                else
                {
                    Debug.Log("Hungry...");
                    Debug.DrawRay(vertex, direction * 1f, Color.blue);
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidEnemy.velocity = Vector2.right.speed.horizontalwalk;

       
        //if (Vector3.Distance(rigidEnemy.transform.position, puntoA) < maxRange && rightdirection)
      
    }

    void TurnEnemy()
    {
        rightdirection = !rightdirection;
        // Vector3 enemyScale = gameObject.transform.localScale;
        // enemyScale.x *= -1;
        // transform.localScale = enemyScale;
        transform.rotation *= Quaternion.Euler(0, 180, 0);
        horizontalWalk = horizontalWalk *(-1);
    }

    bool IsFacingPlayer(Vector2 directionToPlayer)
    {   
        /*
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float angle = Vector3.Angle(transform.up, directionToPlayer);
            return angle < 90f; // Devuelve true si el ángulo es menor que 90 grados
        }
        return false;
       
        return !(transform.localScale.x > 0 && directionToPlayer.x > 0) || (transform.localScale.x < 0 && directionToPlayer.x < 0);
        */

        // Obtener la dirección relativa del jugador al enemigo en el espacio local del enemigo
        Vector2 relativeDirection = transform.InverseTransformDirection(directionToPlayer);
        // Verificar si el jugador está a la izquierda o a la derecha del enemigo en el espacio local
        bool isFacingRight = relativeDirection.x <= 0;
        // Verificar si el enemigo está girado hacia la dirección correcta
        return (isFacingRight && directionToPlayer.x < 0) || (!isFacingRight && directionToPlayer.x > 0);
    }
    /* 
     private bool EnemyVision()
     {
         float extraHeight = 4f;
         RaycastHit2D raycastHit2D = Physics2D.Raycast(enemyCollider2D.bounds.center, Vector2.left, enemyCollider2D.bounds.extents.x + extraHeight);
         bool EnemyVision = raycastHit2D.collider != null;

         Color raycatHitColor = EnemyVision ? Color.green : Color.red;
         Debug.DrawRay(enemyCollider2D.bounds.center, Vector2.left * (enemyCollider2D.bounds.extents.x + extraHeight), raycatHitColor);

         return EnemyVision;
     }
    */
}
