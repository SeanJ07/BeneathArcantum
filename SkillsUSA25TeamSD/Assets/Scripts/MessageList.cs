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
            NextMessage();
        }

        if (currentMessage >= messages.Length) // Once all the messages are gone through, initiate end event ( turn off gameObject)
        {
            currentMessage = 0;
            endEvent.Invoke();
        }
    }

    public void NextMessage()
    {
        if (currentMessage < messages.Length) // While the current message hasnt gone through all the messages, go to the next message after the current one is done.
        {
            currentMessage += 1;
            StartCoroutine(typewriterEffect());
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
        string currentText = "";

        for (int i = 0; i < messages[currentMessage].Length + 1; i++)
        {
            currentText = messages[currentMessage].Substring(0, i);
            textOutput.text = currentText;

            yield return new WaitForSeconds(textDelay);
        }


    }
}
