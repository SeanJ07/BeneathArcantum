using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TextInteractible : MonoBehaviour
{
    [TextArea] public string[] messages;
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onStart;
    public UnityEvent onUnactivated;

    public MessageList messageList;

    void Start()
    {
        onStart.Invoke();
        messageList.messages = messages;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            messageList.messages = messages;
            messageList.attatchedInteraction = this;
            StartCoroutine(messageList.typewriterEffect());
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }

    public void Exit()
    {
        onExit.Invoke();
    }
}
