using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private ParticleSystem shotParticle;

    [SerializeField] private Transform shotPoint;

    private Animator playerAnimator;

    public AudioSource audShot;

    private Rigidbody2D rigidPlayer;

    private bool gunIsReady = true;
    private bool onTheBox = false;
    private bool onTheFloor = false;
    private bool onTheLadder = false;
    private bool positiveDirection = true;
    
    private float gunIsReloaded = 2.5f;
    private float horizontalMov;
    private float jumpForce = 7.75f;
    private float reloadTheGun = 0;

    private int ladderContactCount = 0;
    private int speed = 5;
    private int speedLadder = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    //Physics Calculs
    void FixedUpdate()
    {
        horizontalMov = Input.GetAxis("Horizontal");
        
        rigidPlayer.velocity = new Vector2(horizontalMov * speed, rigidPlayer.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
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

            if (horizontalMov > 0.0f && !positiveDirection)
            {
                FlipPlayer();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && (onTheFloor))
        {
            rigidPlayer.velocity += Vector2.up * jumpForce;
            onTheFloor = false;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onTheLadder)
        {
            rigidPlayer.velocity = Vector2.up * speedLadder ;
        }

        if (gunIsReady)
        {
            Debug.Log("You can shot");

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                ShotTheGun();
                shotParticle.Play();
                gunIsReady = false;
                audShot.Play();
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

        if (collision.gameObject.CompareTag("box"))
        {
            onTheFloor = true;
            onTheBox = true;
        }
    }
    void OnCollisionExit2D(Collision2D dontcollision)
    {
        if (dontcollision.gameObject.CompareTag("box"))
        {
            onTheBox = false;
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
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }

    private void ShotTheGun()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
    }
}
