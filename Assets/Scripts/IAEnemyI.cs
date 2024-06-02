using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class IAEnemyI : MonoBehaviour
{
    private Rigidbody2D rigidEnemy;

    public LayerMask playerLayer;

    private Vector2 attackDirection = Vector2.right;

    private Color orange = new Color(1f, 0.5f, 0.0f);

    private bool onTheFloor = false;
    private bool rightdirection = true;

    private float horizontalWalk = 1;
    private float speed = 3;

    private int groundContactCount = 0;// Contador para rastrear el número de colisiones con objetos "ground"

    private float startMov = 0f;
    private float endMov = 4f;
    private float frontLookDistance = 5f;
    private float backLookDistance = 1f;
    private float lengthAttack = 1f;
    private float enemiTakeDmg = 0f;

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

        Vector2 mouth = new Vector2(transform.position.x, transform.position.y + 0.9f);

        if (rightdirection)
        {
            

            RaycastHit2D hit = Physics2D.Raycast(mouth, attackDirection, lengthAttack, playerLayer);

            if (hit.collider != null)
            {
                Debug.Log("Attack");
                Debug.DrawRay(mouth, attackDirection * lengthAttack, orange);
                enemiTakeDmg += Time.fixedDeltaTime;
                ScLife scLife = hit.collider.GetComponent<ScLife>();

                if (enemiTakeDmg >= 2 && scLife != null)
                {
                    Debug.Log("MUERDO");
                    scLife.TakeDamage();
                    enemiTakeDmg = 0;
                } 
            }
            else
            {
                Debug.Log("Hungry");
                Debug.DrawRay(mouth, attackDirection * lengthAttack, Color.blue);
            }
            
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(mouth, -attackDirection, lengthAttack, playerLayer);
            if (hit.collider != null)
            {
                Debug.Log("Attack");
                Debug.DrawRay(mouth, -attackDirection * lengthAttack, orange);
                enemiTakeDmg += Time.fixedDeltaTime;
                ScLife scLife = hit.collider.GetComponent<ScLife>();

                if ((enemiTakeDmg >= 2 && scLife != null))
                {
                    Debug.Log("MUERDO");
                    scLife.TakeDamage();
                    enemiTakeDmg = 0;
                }
            }
            else
            {
                Debug.Log("Hungry");
                Debug.DrawRay(mouth, -attackDirection * lengthAttack, Color.blue);
            }
        }
    }

    void FixedUpdate()
    {
        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);
        
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
                startMov = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContactCount++; //Incrementa el contador cuando colisiona con un objeto "ground"
            onTheFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContactCount--; //Decrementa el contador cuando deja de estar en contacto con un objeto "ground"

            if (groundContactCount <= 0)
            {
                onTheFloor = false;
            }
        }
    } 

    void TurnEnemy()
    {
        rightdirection = !rightdirection;
        transform.rotation *= Quaternion.Euler(0, 180, 0);
        horizontalWalk = horizontalWalk * (-1);
    }

    private void OnTriggerEnter2D(Collider2D touch)
    {
        if (touch.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
    }
}