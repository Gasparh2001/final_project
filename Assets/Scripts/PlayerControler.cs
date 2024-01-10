using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
 
    private Rigidbody2D rigidPlayer;
    public int speed = 5;
    public float horizontalMov;

    private bool positiveDirection = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        //obtiene la componente Rb2D del objete
    }


    private void FixedUpdate()
    //para manejar calculos de fisicas
    {
        horizontalMov = Input.GetAxis("Horizontal");
        //
        rigidPlayer.velocity = new Vector2(horizontalMov * speed, rigidPlayer.velocity.y);
        //accede a la propiedad v, establece velocidades en X y en Y
    }


    // Update is called once per frame
    void Update()
    {
        if (horizontalMov < 0.0f && positiveDirection) 
        {
            FlipPlayer();
        }
        //si hM es menor a 0 y pD es verdadero, se ejecuta FP
        if (horizontalMov > 0.0f && !positiveDirection)
        {
            FlipPlayer();
        }
    }


    void FlipPlayer() 
    {
        positiveDirection =! positiveDirection;
        Vector2 playerScale = gameObject.transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
        //marco las pautas para girar al player
    }

}
