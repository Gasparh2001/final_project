using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private LayerMask floorLayerMask;

    [SerializeField] private Transform shotPoint;
    
    private Animator playerAnimator;

    private Rigidbody2D rigidPlayer;
   
    private BoxCollider2D playerCollider2D;

    private int speed = 5;
    private int speedLadder = 3;
    private int ladderContactCount = 0;

    private float horizontalMov;
    private float jumpForce = 7.75f;
    private float reloadTheGun = 0;
    private float gunIsReloaded = 3f;
    
    private bool positiveDirection = true;
    public bool onTheFloor = false;
    public bool onTheLadder = false;
    public bool gunIsReady = true;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        //obtiene la componente Rb2D del objeto
        playerAnimator = GetComponent<Animator>();
    }

    //para manejar calculos de fisicas
    private void FixedUpdate()
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
        
        /*if (horizontalMov < 0.0f && positiveDirection) 
        {
            FlipPlayer();
        }
        //si hM es menor a 0 y pD es verdadero, se ejecuta FP
        if (horizontalMov > 0.0f && !positiveDirection)
        {
            FlipPlayer();
        }*/
        if (horizontalMov == 0)
        {
            playerAnimator.SetBool("playerRun", false);
        }
        else
        {
            playerAnimator.SetBool("playerRun", true);

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


        if (Input.GetKeyDown(KeyCode.Space) && onTheFloor)
        {
            //rigidPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //* rigidPlayer.velocity = new Vector2 (jumpMov * jumpForce);
            rigidPlayer.velocity += Vector2.up * jumpForce;//correcto
            onTheFloor = false;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onTheLadder)
        {
            //verticalMov = Input.GetAxis("vertical");
            //transform.position = new Vector2 (transform.position.x, transform.position.y + 0.5f);
            rigidPlayer.velocity = Vector2.up * speedLadder ;
        }

        if (gunIsReady)
        {
            Debug.Log("You can shot");

            if (Input.GetButtonDown("Fire1"))
            {
                ShotTheGun();
                gunIsReady = false;
            }
        }
        else
        {
            Debug.Log("Start Reload");
            reloadTheGun += Time.fixedDeltaTime;

            if (reloadTheGun >= gunIsReloaded)
            {
                gunIsReady=true;
                reloadTheGun = 0.0f;
            }
        }
        

    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onTheFloor = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D touch)
    {
        if (touch.CompareTag("ladder"))
        {
            ladderContactCount++;
            onTheLadder = true;
            onTheFloor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D donttouch)
    {
        if (donttouch.CompareTag("ladder"))
        {
            ladderContactCount--;

            if (ladderContactCount <= 0)
            {
                onTheLadder = false;
            }
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

    private void ShotTheGun()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
    }

    /*
    private bool IsOnTheGround()
    {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(playerCollider2D.bounds.center, Vector2.down, playerCollider2D.bounds.extents.y + extraHeight, floorLayerMask);
        bool isOnTheGround = raycastHit2D.collider != null;

        Color raycatHitColor = isOnTheGround ? Color.green : Color.red;
        Debug.DrawRay(playerCollider2D.bounds.center, Vector2.down * (playerCollider2D.bounds.extents.y + extraHeight), raycatHitColor);

        return isOnTheGround;
    }
    */

}
