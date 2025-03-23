using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pivot; // ???????????????
    public float smoothTime = 0.375f; // ???????????????????????????????
    public float speed = 5f; // ???????????
    private float xRotation = 0f;
    private float yRotation = -45f; // ??????y?????-45??????????
    private float xTargetRotation = 0f;
    private float yTargetRotation = -45f;

    public AudioSource audioSource; // AudioSource ???????
    public AudioClip leftAudioClip; // ?????????????
    public AudioClip rightAudioClip; // ?????????????

    private Coroutine rotationCoroutine;
    private Coroutine holdCoroutine; 

    void Update()
    {
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

        // Handles the Y-axis rotation (Left/Right) with the hold functionality... for some reason
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            yTargetRotation += 45f; // Rotates left
            PlayAudio(leftAudioClip); // Plays left audio effect
            StartSmoothRotation();

            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            holdCoroutine = StartCoroutine(HandleContinuousRotation(KeyCode.LeftArrow));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            yTargetRotation -= 45f; // Rotates right
            PlayAudio(rightAudioClip); // Plays right audio effect
            StartSmoothRotation();

            if (holdCoroutine != null) StopCoroutine(holdCoroutine);
            holdCoroutine = StartCoroutine(HandleContinuousRotation(KeyCode.RightArrow));
        }

        // Stop the hold functionality when the keys are released, still don't know why I added this
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
            t = t * t * (3f - 2f * t); // The smooth step interpolation, god I hated this part 
            yRotation = Mathf.Lerp(startYRotation, yTargetRotation, t);
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, yRotation, pivot.localEulerAngles.z);

            yield return null;
        }

        yRotation = yTargetRotation;
        pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, yRotation, pivot.localEulerAngles.z);
    }

    private IEnumerator HandleContinuousRotation(KeyCode key)
    {
        yield return new WaitForSeconds(1f); // Waits for the hold duration

        while (Input.GetKey(key))
        {
            if (key == KeyCode.LeftArrow)
            {
                yTargetRotation += 45f; // Moves 45 increments rotate left
                PlayAudio(leftAudioClip); // Play left audio effect
            }
            else if (key == KeyCode.RightArrow)
            {
                yTargetRotation -= 45f; // Moves 45 increments rotate right
                PlayAudio(rightAudioClip); // Play right audio effect
            }

            StartSmoothRotation();
            yield return new WaitForSeconds(0.15f); // Rotates every 0.1 seconds
        }
    }

    private Coroutine cameraMoveCoroutine;

    public Vector3 likePosition = new Vector3(-0.38f, 3.28f, 0.67f); // Target camera position
    public Quaternion likeRotation = Quaternion.Euler(-7.31f, 0f, 0f); // Target camera rotation

    // Call this method to activate the "like trigger"
    public void ActivateLikeTrigger()
    {
        if (cameraMoveCoroutine != null)
            StopCoroutine(cameraMoveCoroutine);

        cameraMoveCoroutine = StartCoroutine(MoveCameraToLikePosition());
    }

    // Coroutine to smoothly move the camera
    private IEnumerator MoveCameraToLikePosition()
    {
        Camera camera = Camera.main; // Assuming the main camera, adjust if using another
        Vector3 startPosition = camera.transform.position;
        Quaternion startRotation = camera.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / smoothTime;
            t = t * t * (3f - 2f * t); // Smooth step interpolation

            camera.transform.position = Vector3.Lerp(startPosition, likePosition, t);
            camera.transform.rotation = Quaternion.Lerp(startRotation, likeRotation, t);

            yield return null;
        }

        camera.transform.position = likePosition;
        camera.transform.rotation = likeRotation;
    }


    // Plays audio
    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
