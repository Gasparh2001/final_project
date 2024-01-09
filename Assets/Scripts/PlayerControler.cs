using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
 
    private Rigidbody2D rigidPlayer;

    public int speed = 5;

    private bool positiveDirection = true;

    public float horizontalMov;


    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        horizontalMov = Input.GetAxis("Horizontal");
        rigidPlayer.velocity = new Vector2(horizontalMov * speed, rigidPlayer.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
