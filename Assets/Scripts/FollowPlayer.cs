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
        transform.position = new Vector3(player.position.x, 0, -10); 
    }

}
