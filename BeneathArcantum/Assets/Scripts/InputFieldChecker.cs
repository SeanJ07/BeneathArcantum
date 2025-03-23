using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class InputFieldChecker : MonoBehaviour
{
    [Header("Input Field")]
    [SerializeField] private TMP_InputField inputField;

    [Header("Text Output")]
    [SerializeField] private TMP_Text outputText;

    [Header("Keywords and Responses")]
    [SerializeField] private List<KeywordResponse> keywordResponses = new List<KeywordResponse>();

    [Header("Thinking Animation")]
    [SerializeField] private float animationSpeed = 0.5f; // Time between each dot animation

    private Coroutine thinkingCoroutine;

    private void Awake()
    {
        if (inputField != null)
        {
            inputField.onSubmit.AddListener(ExecuteOnEnter);
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

        // Loop through the animation for a few cycles (example: 3 full cycles)
        for (int i = 0; i < 3; i++)
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
                return;
            }
        }

        // If no keywords are matched, clear the text
        outputText.text = string.Empty;
    }

    private void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onSubmit.RemoveListener(ExecuteOnEnter);
        }
    }
}

[System.Serializable]
public class KeywordResponse
{
    public string keyword;      // The word to detect
    public string responseText; // The response to display
}
