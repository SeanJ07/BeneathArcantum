using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MessageList : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    public int currentMessage;
    public int currentLetter;
    public float textDelay = 0.05f;
    public UnityEvent endEvent;
    private MessageList messageList;

    [TextArea] public string[] messages;


    private void Awake() // Finds the text output and the current list of messages to be displayed.
    {
        messageList = this.GetComponent<MessageList>();
        textOutput = GameObject.Find("TextOutput").GetComponent<TextMeshProUGUI>();
    }


    void Start() // Starts at the first message and initiates the typewriter effect for it.
    {
        currentMessage = 0;
        StartCoroutine(typewriterEffect());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // E to continue onto the next message.
        {
            StartCoroutine(NextMessage());
        }

        if (currentMessage >= messages.Length) // Once all the messages are gone through, initiate end event ( turn off gameObject)
        {
            currentMessage = 0;
            endEvent.Invoke();
        }
    }

    public IEnumerator NextMessage()
    {
        if (currentMessage < messages.Length) // While the current message hasnt gone through all the messages, go to the next message after the current one is done.
        {
            currentMessage += 1;
            StartCoroutine(typewriterEffect());

            yield break;
        }
    }

    public void UpdateMessage()
    {
        StartCoroutine(typewriterEffect());
    }

    public void PreviousMessage() // Go back to the last message
    {
        currentMessage -= 1;
        textOutput.text = messages[currentMessage];
    }

    public IEnumerator typewriterEffect() // Does the typewriter effect
    {
        StopCoroutine(typewriterEffect());
        string currentText = "";
        currentLetter = 0;

        textOutput.text = currentText;

        for (currentLetter = 0; currentLetter < messages[currentMessage].Length + 1; currentLetter++)
        {
            currentText = messages[currentMessage].Substring(0, currentLetter);
            textOutput.text = currentText;

            yield return new WaitForSeconds(textDelay);
        }


    }
}
