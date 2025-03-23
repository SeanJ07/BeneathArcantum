using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
public class CameraController : MonoBehaviour
{
    public Transform pivot;
    public float smoothTime = 0.375f;
    public float speed = 5f;
    private float xRotation = 0f;
    private float yRotation = -45f;
    private float xTargetRotation = 0f;
    private float yTargetRotation = -45f;

    public AudioSource audioSource;
    public AudioClip leftAudioClip;
    public AudioClip rightAudioClip;

    private Coroutine rotationCoroutine;
    private Coroutine holdCoroutine;

    // Movement toggle for enabling/disabling camera movement
    public bool movementEnabled = true;


    void Update()
    {
        if (!movementEnabled) return; // Exit the update loop if movement is disabled

        // Handles the X-axis rotation
        if (Input.GetKey(KeyCode.UpArrow))
        {
            xTargetRotation = Mathf.Clamp(xTargetRotation + speed * Time.deltaTime, -13f, 13f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            xTargetRotation = Mathf.Clamp(xTargetRotation - speed * Time.deltaTime, -13f, 13f);
        }

        xRotation = Mathf.Lerp(xRotation, xTargetRotation, Time.deltaTime * speed);
        pivot.localEulerAngles = new Vector3(xRotation, pivot.localEulerAngles.y, pivot.localEulerAngles.z);

        // Handles the Y-axis rotation (Left/Right) with the hold functionality
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            yTargetRotation += 45f; // Rotates left
            PlayAudio(leftAudioClip);
            StartSmoothRotation();

            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            holdCoroutine = StartCoroutine(HandleContinuousRotation(KeyCode.LeftArrow));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            yTargetRotation -= 45f; // Rotates right
            PlayAudio(rightAudioClip);
            StartSmoothRotation();

            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            holdCoroutine = StartCoroutine(HandleContinuousRotation(KeyCode.RightArrow));
        }

        // Stop the hold functionality when the keys are released
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
            }
        }
    }

    private void StartSmoothRotation()
    {
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);

        rotationCoroutine = StartCoroutine(SmoothYRotation());
    }

    private IEnumerator SmoothYRotation()
    {
        float elapsedTime = 0f;
        float startYRotation = yRotation;

        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / smoothTime;
            t = t * t * (3f - 2f * t);
            yRotation = Mathf.Lerp(startYRotation, yTargetRotation, t);
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, yRotation, pivot.localEulerAngles.z);

            yield return null;
        }

        yRotation = yTargetRotation;
        pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, yRotation, pivot.localEulerAngles.z);
    }

    private IEnumerator HandleContinuousRotation(KeyCode key)
    {
        yield return new WaitForSeconds(1f);

        while (Input.GetKey(key))
        {
            if (key == KeyCode.LeftArrow)
            {
                yTargetRotation += 45f;
                PlayAudio(leftAudioClip);
            }
            else if (key == KeyCode.RightArrow)
            {
                yTargetRotation -= 45f;
                PlayAudio(rightAudioClip);
            }

            StartSmoothRotation();
            yield return new WaitForSeconds(0.15f);
        }
    }

    private Coroutine cameraMoveCoroutine;

    public Vector3 likePosition = new Vector3(-0.38f, 3.28f, 0.67f);
    public Quaternion likeRotation = Quaternion.Euler(-7.31f, 0f, 0f);

    public void ActivateLikeTrigger()
    {
        if (cameraMoveCoroutine != null)
            StopCoroutine(cameraMoveCoroutine);

        cameraMoveCoroutine = StartCoroutine(MoveCameraToLikePosition());
    }

    private IEnumerator MoveCameraToLikePosition()
    {
        Camera camera = Camera.main;
        Vector3 startPosition = camera.transform.position;
        Quaternion startRotation = camera.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / smoothTime;
            t = t * t * (3f - 2f * t);

            camera.transform.position = Vector3.Lerp(startPosition, likePosition, t);
            camera.transform.rotation = Quaternion.Lerp(startRotation, likeRotation, t);

            yield return null;
        }

        camera.transform.position = likePosition;
        camera.transform.rotation = likeRotation;
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
