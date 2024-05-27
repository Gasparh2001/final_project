using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public LayerMask playerLayer;

    private Vector2 mouth;
    private Vector2 attackDirection = Vector2.right;
    private Color orange = new Color(1f, 0.5f, 0.0f);
    private float lengthAttack = 3f;
    private bool rightdirection = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouth = new Vector2 (transform.position.x, transform.position.y + 0.5f);

        if (rightdirection = true)
        {
            //creo el raycast a la derecha

            RaycastHit2D hit = Physics2D.Raycast (mouth , attackDirection, lengthAttack);

            if (hit.collider != null)
            {
                Debug.Log ("Ataco");
                Debug.DrawRay (mouth, attackDirection * lengthAttack, (orange));
            }

            if (hit.collider == null)
            {
                Debug.Log ("Tengo hambre");
                Debug.DrawRay (mouth, attackDirection * lengthAttack, Color.blue);
            }
        }

        else
        {
            //creo el raycast a la izq

            RaycastHit2D hit = Physics2D.Raycast(mouth, -attackDirection, lengthAttack);

            if (hit.collider != null)
            {
                Debug.Log ("Ataco");
                Debug.DrawRay (mouth, -attackDirection * lengthAttack, (orange));
            }

            if (hit.collider == null)
            {
                Debug.Log ("Tengo hambre");
                Debug.DrawRay (mouth, -attackDirection * lengthAttack, Color.blue);
            }
        }
        
    }

}
