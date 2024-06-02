using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSc : MonoBehaviour
{
    [SerializeField] private float bulledSpeed;

    private float shotMov = 0;
    private float stopMov = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * bulledSpeed * Time.deltaTime);

        if (transform.position.x == 0)
        {
            Debug.Log("Start Counting");
            shotMov += Time.fixedDeltaTime;
        }

        if (shotMov >= stopMov)
        {
            Debug.Log("Stop Counting");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D touch)
    {
        Debug.Log("Has Collided");
        Destroy(gameObject);
    }






}
