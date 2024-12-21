using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MessageList : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    public int currentMessage;
    public UnityEvent endEvent;
    private MessageList messageList;

    [TextArea] public string[] messages;


    private void Awake()
    {
        messageList = this.GetComponent<MessageList>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentMessage = 0;
        UpdateMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
                NextMessage();
        }
    }

    public void NextMessage()
    {
        if (currentMessage < messages.Length)
        {
            currentMessage += 1;
            UpdateMessage();
        }
        else if (currentMessage >= messages.Length)
        {
            endEvent.Invoke();
        }
    }

    public void PreviousMessage()
    {
        currentMessage -= 1;
        UpdateMessage();
    }

    public void UpdateMessage()
    {
        textOutput.text = messages[currentMessage];
    }

    public void Stop()
    {
        messageList.enabled = !messageList.enabled;
    }
}
