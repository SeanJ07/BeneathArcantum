using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject player;
    public bool activated;
    public Material unactivatedMaterial;
    public Material activatedMaterial;

    public GameObject connectedObject;

    private MeshRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        player = GameObject.Find("Player");
        activated = false;
        render.material = unactivatedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            activated = true;
            render.material = activatedMaterial;
            
            if (connectedObject.CompareTag("Door"))
            {
                connectedObject.GetComponent<DoorOpening>().DoorOpen();
            }

            if (connectedObject.CompareTag("MovingPlatform"))
            {
                if (connectedObject.GetComponent<MovingPlatform>().enabled == false)
                {
                    connectedObject.GetComponent<MovingPlatform>().enabled = true;
                }
            }
        }
    }

   
}
