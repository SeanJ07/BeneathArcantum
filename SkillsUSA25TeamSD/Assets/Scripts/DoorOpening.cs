using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorOpen()
    {
        Vector3 currentTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.Translate(new Vector3(0,-20,0) * speed * Time.deltaTime);
    }
}
