using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLockPlayer : MonoBehaviour
{

    PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLocking()
    {
        player.locked = true;
    }
}
