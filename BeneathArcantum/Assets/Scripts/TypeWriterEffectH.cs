using UnityEngine;
using System.Collections;
using TMPro;

public class TypeWriterEffectH : MonoBehaviour
{
    public float delay = 0.5f; // Delay between words
    public string fullText;
    private string currentText = "";

    // Called when the script is enabled
    void OnEnable()
    {
        // Reset the text and start the animation
        currentText = "";
        this.GetComponent<TextMeshProUGUI>().text = currentText;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        // Split fullText into words
        string[] words = fullText.Split(' ');

        // Iterate through each word
        for (int i = 0; i < words.Length; i++)
        {
            // Append the next word and a space
            currentText += words[i] + " ";
            this.GetComponent<TextMeshProUGUI>().text = currentText;

            // Wait for the specified delay
            yield return new WaitForSeconds(delay);
        }
    }
}
