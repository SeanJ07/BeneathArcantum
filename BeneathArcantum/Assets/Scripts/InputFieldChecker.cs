using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class InputFieldChecker : MonoBehaviour
{
    [Header("Input Field")]
    [SerializeField] private TMP_InputField inputField;

    [Header("Text Output")]
    [SerializeField] private TMP_Text outputText;

    [Header("Keywords and Responses")]
    [SerializeField] private List<KeywordResponse> keywordResponses = new List<KeywordResponse>();

    [Header("Thinking Animation")]
    [SerializeField] private float animationSpeed = 0.2f; // Faster animation speed
    [SerializeField] private int animationCycles = 2; // Reduced cycles for faster display

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] typingSounds; // Array to assign sound effects
    [SerializeField] private AudioSource audioSource;  // AudioSource to play the sounds
    [SerializeField] private AudioClip unrecognizedCommandSound; // Sound for unrecognized command

    private bool isBackspaceHeld = false; // Tracks if Backspace is held
    private Coroutine thinkingCoroutine;

    private void Awake()
    {
        if (inputField != null)
        {
            inputField.onSubmit.AddListener(ExecuteOnEnter);
            inputField.onValueChanged.AddListener(PlayTypingSound);
        }
    }

    private void Update()
    {
        // Detect if Backspace is held
        if (Input.GetKey(KeyCode.Backspace))
        {
            isBackspaceHeld = true;
        }
        else
        {
            isBackspaceHeld = false;
        }
    }

    private void PlayTypingSound(string input)
    {
        // Only play sounds if Backspace isn't being held
        if (!isBackspaceHeld && typingSounds != null && typingSounds.Length > 0 && audioSource != null)
        {
            // Select a random sound effect from the array
            AudioClip randomSound = typingSounds[Random.Range(0, typingSounds.Length)];
            audioSource.PlayOneShot(randomSound);
        }
    }

    private void ExecuteOnEnter(string input)
    {
        if (thinkingCoroutine != null)
        {
            StopCoroutine(thinkingCoroutine);
        }

        // Clear the output text
        outputText.text = string.Empty;

        // Start the "thinking" animation
        thinkingCoroutine = StartCoroutine(ThinkingAnimation(input));
    }

    private IEnumerator ThinkingAnimation(string input)
    {
        string[] dots = { ".", "..", "..." };
        int index = 0;

        // Loop through the animation for a few cycles
        for (int i = 0; i < animationCycles; i++)
        {
            outputText.text = dots[index];
            index = (index + 1) % dots.Length; // Loop through the dots array
            yield return new WaitForSeconds(animationSpeed);
        }

        // After the animation, display the response based on the input
        DisplayResponse(input);
    }

    private void DisplayResponse(string input)
    {
        foreach (var keywordResponse in keywordResponses)
        {
            if (input.Contains(keywordResponse.keyword))
            {
                outputText.text = keywordResponse.responseText;

                // Invoke the associated action if it exists
                keywordResponse.action?.Invoke();
                return;
            }
        }

        // If no keywords are matched, play sound and display "not recognized" message
        outputText.text = $"\"{input}\" is not a recognized command.";
        if (audioSource != null && unrecognizedCommandSound != null)
        {
            audioSource.PlayOneShot(unrecognizedCommandSound);
        }
    }

    private void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onSubmit.RemoveListener(ExecuteOnEnter);
            inputField.onValueChanged.RemoveListener(PlayTypingSound);
        }
    }
}

[System.Serializable]
public class KeywordResponse
{
    public string keyword;      // The word to detect
    public string responseText; // The response to display
    public UnityEvent action;   // Action to invoke when the keyword is matched
}
