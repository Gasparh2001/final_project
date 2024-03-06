using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruevas : MonoBehaviour
{
    private bool lastOnTheFloor = false; // Variable para rastrear si el jugador estaba en el suelo previamente

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onTheFloor = true; // Establece onTheFloor en true cuando colisiona con un objeto con la etiqueta "ground"
            lastOnTheFloor = true; // Marca que el jugador estaba en el suelo previamente
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            lastOnTheFloor = false; // Marca que el jugador ya no está en el suelo
            if (!IsPlayerStillOnGround()) // Si el jugador ya no está en contacto con ningún objeto con la etiqueta "ground"
            {
                onTheFloor = false; // Establece onTheFloor en false
            }
        }
    }

    private bool IsPlayerStillOnGround()
    {
        // Verifica si todavía hay alguna colisión con un objeto que tenga la etiqueta "ground"
        foreach (var contact in GetComponent<Collider2D>().GetContacts())
        {
            if (contact.collider.CompareTag("ground"))
            {
                return true; // Si hay una colisión con "ground", devuelve true
            }
        }
        return false; // Si no hay colisión con "ground", devuelve false
    }
    private void FixedUpdate()
    //para manejar calculos de fisicas
    {
        
           RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            //calcula distancia vertical entre el punto de colisión y la posición actual del obj (valor abs.)
            float heightError = floatHeight - distance;

            float force = liftForce * heightError - rigidPlayer.velocity.y * damping;

            rigidPlayer.AddForce(Vector2.up * force);
        }

    StartCoroutine(Motion());
        IEnumerator Motion()
        {
            while (true)
            {
                rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);
                yield return new WaitForSeconds(waitingTime);
                speed *= -1;
                yield return null;
            }
        }
    }
    */
}
