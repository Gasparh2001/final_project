using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IAEnemyI : MonoBehaviour
{
    private Rigidbody2D rigidEnemy;

    [SerializeField] private Transform player;

    public LayerMask enemy;

    public bool onTheFloor = false;
    private bool rightdirection = true;

    public float horizontalWalk;//-1
    public float speed;//3
    public float speedDetection;

    private int groundContactCount = 0; // Contador para rastrear el número de colisiones con objetos "ground"
    public int rayCount = 3;

    private float startMov = 0f;
    private float endMov = 4f;
    private float look = 5f;


    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast
        Vector2 enemyPosition = rigidEnemy.position;
        float halfSize = look / 5;
        Vector2[] horizontalVertex = new Vector2[]
        { 
            enemyPosition + new Vector2((rightdirection ? -1.5f : 1.5f), 4),
            enemyPosition + new Vector2((rightdirection ? -1.5f : 1.5f), 0),
            enemyPosition + new Vector2((rightdirection ? -1.5f : 1.5f), 2),
        };
        
        foreach (Vector2 vertex in horizontalVertex)
        {
            for (int i = 0; i < rayCount; i++)
            {
                float angle = i * (360f / 2);
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast(vertex, direction, 3.5f, enemy);

                if (hit.collider != null)
                {
                    Debug.Log("Perseguir");
                    Debug.DrawRay(vertex, direction * 3.5f, Color.red);

                    if (( rigidEnemy.transform.position.x - player.transform.position.x ) > 0f && !rightdirection)
                    {
                        TurnEnemy();
                        startMov = 0;
                    }
                    if ((rigidEnemy.transform.position.x - player.transform.position.x) < 0f && rightdirection)
                    {
                        TurnEnemy();
                        startMov = 0;
                    }
                    startMov = 0;
                }
                else
                {
                    Debug.Log("Serch...");
                    Debug.DrawRay(vertex, direction * 3.5f, Color.green);
                }
            }
        }
    }

    void FixedUpdate()
    {
        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);
        //Movment
        if (onTheFloor == false)
        {
            Debug.Log("No me muevo");
            speed = 0f;
        }
        if (onTheFloor == true)
        {
            Debug.Log("Me muevo");
            speed = 3f;

            startMov += Time.deltaTime;

            if (startMov >= endMov)
            {
                TurnEnemy();
                startMov = 0f; // Reiniciar el contador de tiempo
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContactCount++; // Incrementa el contador cuando colisiona con un objeto "ground"
            onTheFloor = true; // Establece en true cuando colisiona con un objeto "ground"
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContactCount--; // Decrementa el contador cuando deja de estar en contacto con un objeto "ground"

            if (groundContactCount <= 0)
            {
                onTheFloor = false; // Establece en false si ya no hay colisiones con objetos "ground"
            }
        }
    } 

    void TurnEnemy()
    {
        rightdirection = !rightdirection;
        transform.rotation *= Quaternion.Euler(0, 180, 0);
        horizontalWalk = horizontalWalk * (-1);
    }   
}