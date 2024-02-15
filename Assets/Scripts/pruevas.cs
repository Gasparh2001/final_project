using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruevas : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
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
