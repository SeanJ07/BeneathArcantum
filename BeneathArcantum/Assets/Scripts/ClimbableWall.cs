using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableWall : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().onWall = true;

        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().onWall = false;

        }
    }
}
