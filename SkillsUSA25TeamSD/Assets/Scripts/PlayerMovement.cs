using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 100;

    Rigidbody playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovements();
    }

    private void PlayerMovements()
    {
        // Checks if the keys for the axis "Horizontal" are being inputted, and gives it a value between -1 and 1.
        float horizontal = Input.GetAxis("Horizontal");

        playerRb.velocity = (Vector3.right * Time.deltaTime * movementSpeed * horizontal);
    }
}
