using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy : MonoBehaviour
{
    public int speed = 3;
    public int waitingTime = 2;

    private Rigidbody2D rigidEnemy;
    public float horizontalWalk = -1;
    private bool rightdirection = true;

    public Vector3 puntoA;
    public Vector3 puntoB;

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy = GetComponent<Rigidbody2D>();

        puntoA = new Vector3(-10, 0, 0);
        puntoB = new Vector3(10, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidEnemy.velocity = Vector2.right.speed.horizontalwalk;
 
        if (rigidEnemy.transform.position == rigidEnemy.transform.position + puntoA && rightdirection)
        {
            TurnEnemy();
        }
        if (rigidEnemy.transform.position == rigidEnemy.transform.position + puntoB && !rightdirection)
        {
            TurnEnemy();
        }
        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);
    }

    void TurnEnemy()
    {
        rightdirection =! rightdirection;
        Vector2 enemyScale = gameObject.transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
        horizontalWalk = horizontalWalk *(-1);
    }
}
