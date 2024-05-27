using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // GameObject transform.position = FollowingThePlayer();
        //transform.position = player.position + new Vector3(0, 0, -10);
        //transform.position = new Vector3(player.position.x,player.position.y, -10);



        /*transform.position = new Vector3(player.position.x, player.position.y + 2, -10);

        if (player.position.y > 0)
        {
            transform.position = new Vector3(player.position.x, transform.position.y * 0.5f , -10);
        }
        if (player.position.y < 0)
        {
            transform.position = new Vector3(player.position.x, transform.position.y * 0.9f, -10);
        }*/


        float newY;
        if (player.position.y >= 0)
        {
            newY = Mathf.Pow(player.position.y, 0.6f); // Raíz cuadrada para valores no negativos
        }
        else
        {
            newY = -Mathf.Pow(Mathf.Abs(player.position.y), 0.94f); // Raíz cuadrada del valor absoluto para valores negativos
        }

        if (player.position.y > 0)
        {
            transform.position = new Vector3(player.position.x, newY +2 , -10);
        }
        else if (player.position.y < 0)
        {
            transform.position = new Vector3(player.position.x, newY +2, -10);
        }

        /*if (player.position.y > 0)
        {
            transform.position = new Vector3(player.position.x, newY , -10);
        }
        if (player.position.y < 0)
        {
            transform.position = new Vector3(player.position.x, newSndY , -10);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y , -10);
        }*/

        /*float newY = Mathf.Pow(transform.position.y, 0.5f);
         float newSndY = Mathf.Pow(-(transform.position.y), 0.5f);

         if (player.position.y > 0)
         {
             transform.position = new Vector3(player.position.x, newY, -10);
         }


         if (player.position.y < 0)
         {
             transform.position = new Vector3(player.position.x, -(newY), -10);
         }*/


    }

}
