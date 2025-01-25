using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItems : MonoBehaviour
{
    public GameObject player;
    float healthAdd = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            player.GetComponent<PlayerController>().UpdateHealth(healthAdd);
            Destroy(gameObject);
        }
    }
}
