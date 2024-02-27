using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IAEnemy : MonoBehaviour
{
    public Transform raycastOrigin;

    public float speed;
    public int waitingTime = 2;

    public float horizontalWalk = -1f;
    private bool rightdirection = true;

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
        }
        */

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
                }
                else
                {
                    Debug.Log("Serch...");
                    Debug.DrawRay(vertex, direction * 3.5f, Color.green);
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
