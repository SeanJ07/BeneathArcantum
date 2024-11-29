using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 50f;
    public float jumpForce = 10f;

    Rigidbody playerRb;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public float groundDrag;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.drag = groundDrag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovements();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);
        while (grounded)
        {
            playerRb.drag = groundDrag;
            Physics.gravity = new Vector3(0, 1f, 0);
        }
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        while (!grounded)
        {
            Physics.gravity = new Vector3(0, 2f, 0);
        }

    }

    private void PlayerMovements()
    {
        // Checks if the keys for the axis "Horizontal" are being inputted, and gives it a value between -1 and 1.
        float horizontal = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.right * movementSpeed * horizontal, ForceMode.Force);
    }

    private void Jump()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, -2, playerRb.velocity.z);
        playerRb.drag /= 2;
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
