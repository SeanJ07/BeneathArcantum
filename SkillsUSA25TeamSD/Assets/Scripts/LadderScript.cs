using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerController>().onWall = true;
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.VelocityChange);
        }
    }


}
