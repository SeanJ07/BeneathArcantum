using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public GameObject player;
    public bool activated;
    public bool weighted;
    public Material unactivatedMaterial;
    public Material activatedMaterial;

    public GameObject connectedObject;
    public UnityEvent onActivated;
    public UnityEvent onUnactivated;

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
        else
        {
            onActivated.Invoke();
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        if (weighted)
        {
            activated = false;
            render.material = unactivatedMaterial;
            onUnactivated.Invoke();
        }
    }


}
