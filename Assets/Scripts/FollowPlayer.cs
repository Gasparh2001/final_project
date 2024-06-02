using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
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
    }

}
