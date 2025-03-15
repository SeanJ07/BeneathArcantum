using UnityEngine;

public class SpinningImage : MonoBehaviour
{
    public bool isClockwise = true; // Set the spin direction in the Inspector
    public float idleSpeed = 10f; // Default idle rotation speed
    public float maxSpeed = 5400f; // Maximum rotation speed (15 rotations per second = 360 degrees * 15)
    public float speedDamping = 10f; // Smoothing for speed changes
    public float returnToIdleSpeed = 3f; // Speed to return to idle speed when mouse stops

    private float currentSpeed = 0f; // Current rotation speed
    private Vector3 lastMousePosition;

    void Start()
    {
        currentSpeed = idleSpeed;
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Calculate mouse movement
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        // Calculate target speed based on mouse movement
        float targetSpeed = idleSpeed + (mouseDelta.magnitude * 800f); // Double sensitivity again
        targetSpeed = Mathf.Clamp(targetSpeed, idleSpeed, maxSpeed);

        // Smoothly update current speed
        if (mouseDelta.magnitude > 0.1f) // When the mouse is moving
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * speedDamping);
        }
        else // When the mouse stops moving
        {
            currentSpeed = Mathf.Lerp(currentSpeed, idleSpeed, Time.deltaTime * returnToIdleSpeed);
        }

        // Determine rotation direction
        float direction = isClockwise ? -1f : 1f;

        // Apply rotation
        transform.Rotate(Vector3.forward, currentSpeed * direction * Time.deltaTime);
    }
}
