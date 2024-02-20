using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
 
    private Rigidbody2D rigidPlayer;
    public int speed = 5;
    public float horizontalMov;
    private BoxCollider2D playerCollider2D;

    private float jumpForce = 7f;
    public float jumpMov;

    [SerializeField] private LayerMask floorLayerMask;


    private bool positiveDirection = true;
    private bool onTheFloor = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        //obtiene la componente Rb2D del objeto
    }


    private void FixedUpdate()
    //para manejar calculos de fisicas
    {
        horizontalMov = Input.GetAxis("Horizontal");
        //
        rigidPlayer.velocity = new Vector2(horizontalMov * speed, rigidPlayer.velocity.y);
        //accede a la propiedad v, establece velocidades en X y en Y

       /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rigidPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //* rigidPlayer.velocity = new Vector2 (jumpMov * jumpForce);
            rigidPlayer.velocity += Vector2.up * jumpForce;
        }
        */
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

        if (Input.GetKeyDown(KeyCode.Space) && onTheFloor)
        {
            //rigidPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //* rigidPlayer.velocity = new Vector2 (jumpMov * jumpForce);
            rigidPlayer.velocity += Vector2.up * jumpForce;//correcto
            onTheFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onTheFloor = true;
        }
    }

    void FlipPlayer() 
    {
        positiveDirection = !positiveDirection;
        //Vector2 playerScale = gameObject.transform.localScale;
        //playerScale.x *= -1;
        //transform.localScale = playerScale;
        transform.rotation *= Quaternion.Euler(0, 180, 0);
        //marco las pautas para girar al player
    }

   
    private bool IsOnTheGround()
    {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(playerCollider2D.bounds.center, Vector2.down, playerCollider2D.bounds.extents.y + extraHeight, floorLayerMask);
        bool isOnTheGround = raycastHit2D.collider != null;

        Color raycatHitColor = isOnTheGround ? Color.green : Color.red;
        Debug.DrawRay(playerCollider2D.bounds.center, Vector2.down * (playerCollider2D.bounds.extents.y + extraHeight), raycatHitColor);

        return isOnTheGround;
    }
   

}
