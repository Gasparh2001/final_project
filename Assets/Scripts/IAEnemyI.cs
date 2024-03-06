using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IAEnemyI : MonoBehaviour
{
    private Rigidbody2D rigidEnemy;

    public bool onTheFloor = false;
    private bool rightdirection = true;

    public float horizontalWalk;//-1
    public float speed;//3

    private int groundContactCount = 0; // Contador para rastrear el número de colisiones con objetos "ground"

    private float startMov = 0f;
    private float endMov = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
    void FixedUpdate()
    {
        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);

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

    /*
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                onTheFloor = true;

            }
        }private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                onTheFloor =false;

            }
        }
       
        */
