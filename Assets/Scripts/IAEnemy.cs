using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy : MonoBehaviour
{
    public int speed = 3;
    public int waitingTime = 2;

    private Rigidbody2D rigidEnemy;
    public float horizontalWalk = -1f;
    private bool rightdirection = true;

    //private Vector3 puntoA;
    //private Vector3 puntoB;
    private float rightLimit;
    private float leftLimit;

    private float maxRange = 0.01f;

    private BoxCollider2D enemyCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidEnemy = GetComponent<Rigidbody2D>();
        leftLimit = rigidEnemy.transform.position.x - 10;
        rightLimit = rigidEnemy.transform.position.x + 10;
        //puntoA = new Vector3(rigidEnemy.transform.position.x - 10, 0, 0);
        //puntoB = new Vector3(rigidEnemy.transform.position.x +10, 0, 0);
    }

    private void Update()
    {

        EnemyVision();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidEnemy.velocity = Vector2.right.speed.horizontalwalk;

       
        //if (Vector3.Distance(rigidEnemy.transform.position, puntoA) < maxRange && rightdirection)
        if (transform.position.x - leftLimit < maxRange)
        {
            TurnEnemy();
            Debug.Log("he llegado a la izq");
        }
        if (rightLimit - transform.position.x < maxRange)
        {
            TurnEnemy();
            Debug.Log("he llegado a la der");
        }

        rigidEnemy.velocity = new Vector2(horizontalWalk * speed, rigidEnemy.velocity.y);
    }

    void TurnEnemy()
    {
        rightdirection = !rightdirection;
        // Vector3 enemyScale = gameObject.transform.localScale;
        // enemyScale.x *= -1;
        // transform.localScale = enemyScale;
        transform.rotation *= Quaternion.Euler(0, 180, 0);
        horizontalWalk = horizontalWalk *(-1);
    }

    private bool EnemyVision()
    {
        float extraHeight = 4f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(enemyCollider2D.bounds.center, Vector2.left, enemyCollider2D.bounds.extents.x + extraHeight);
        bool EnemyVision = raycastHit2D.collider != null;

        Color raycatHitColor = EnemyVision ? Color.green : Color.red;
        Debug.DrawRay(enemyCollider2D.bounds.center, Vector2.left * (enemyCollider2D.bounds.extents.x + extraHeight), raycatHitColor);

        return EnemyVision;
    }
}
