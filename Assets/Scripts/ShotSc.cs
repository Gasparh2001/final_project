using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSc : MonoBehaviour
{
    [SerializeField] private float bulledSpeed;

    private float shotMov = 0;
    private float stopMov = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * bulledSpeed * Time.deltaTime);

        if (transform.position.x == 0)
        {
            Debug.Log("empieza a contar");
            shotMov += Time.fixedDeltaTime;
        }
        if (shotMov >= stopMov)
        {
            Debug.Log("para de contar");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D touch)
    {
        Debug.Log("tocado");
            Destroy(gameObject);
      
        
        /*if (touch.CompareTag("bullet") && touch.gameObject.name == "headzoombie")
        {
            Destroy(gameObject);
        }*/
    }






}
