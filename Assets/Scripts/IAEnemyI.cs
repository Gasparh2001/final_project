using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IAEnemyI : MonoBehaviour
{
    private Rigidbody2D rigidEnemy;

    [SerializeField] private Transform player;

    public LayerMask playerLayer;

    public bool onTheFloor = false;
    private bool rightdirection = true;

    public float horizontalWalk;//-1
    public float speed;//3
    public float speedDetection;

    private int groundContactCount = 0; // Contador para rastrear el número de colisiones con objetos "ground"
    public int rayCount = 3;

    private float startMov = 0f;
    private float endMov = 4f;
    private float frontLookDistance = 5f;
    private float backLookDistance = 1f;

    private bool[] hitFrontArray = new bool[3];
    private bool[] hitBackArray = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hitFrontArray.Length ; i++)
        {
            hitFrontArray[i] = Physics2D.Raycast(transform.position + new Vector3 ( 0, i*2, 0), transform.right, frontLookDistance, playerLayer);
            hitBackArray[i] = Physics2D.Raycast(transform.position + new Vector3(0, i * 2, 0), -transform.right, backLookDistance, playerLayer);

            Debug.DrawRay(transform.position + new Vector3(0, i * 2, 0), transform.right * frontLookDistance, hitFrontArray[i] ? Color.green : Color.red);
            Debug.DrawRay(transform.position + new Vector3(0, i * 2, 0), -transform.right * backLookDistance, hitBackArray[i] ? Color.green : Color.red);

            if (hitFrontArray[i])
            {
                Debug.Log("Veo el jugador delante");
                startMov = 0;

                break;
            }
            if (hitBackArray[i])
            {
                Debug.Log("Veo el jugador detras");
                TurnEnemy();
                startMov = 0;

                break;
            }
        }
        /*
        for (int i = 0; i < hitBackArray.Length; i++)
        {
            hitBackArray[i] = Physics2D.Raycast(transform.position + new Vector3(0, i * 2, 0), -transform.right, backLookDistance, playerLayer);
        }
        foreach (bool backbool in hitBackArray)
        {
            if (backbool == true)
            {
                Debug.Log("Veo el jugador a mi detras");
            }

            else
            {
                Debug.Log("No veo el jugador a detras");
            }
        }
        foreach (bool frontbool in hitFrontArray)
        {
            if (frontbool == true)
            {
                Debug.Log("Veo el jugador a delante");
            }

            else
            {
                Debug.Log("No veo el jugador a delante");
            }
        }
        RaycastHit2D hitBack = Physics2D.Raycast(transform.position, -transform.right, backLookDistance, playerLayer);//debuelve un booleano

        if (hitBack.collider != null)
        {
            Debug.Log("Perseguiratras");
        }
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
        */
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
        else
        {
            Debug.Log("Me muevo");
            speed = 3f;

            startMov += Time.fixedDeltaTime;

            if (startMov >= endMov)
            {
                TurnEnemy();
                startMov = 0f; // Reiniciar el contador de tiempo
            }
        }
        if ((rigidEnemy.transform.position.x - player.transform.position.x) < 0f && rightdirection)
        {
            Debug.Log("Esta a la drc");
            if ((rigidEnemy.transform.position.x - player.transform.position.x) > -5f)
            {
                Debug.Log("Perseguir drc");
                startMov = 0;
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