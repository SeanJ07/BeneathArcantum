using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// This script will be used for UI puzzles where the player activates multiple things to make something happen.
public class CounterScript : MonoBehaviour
{
    public UnityEvent onFinished;

    public int count;

    public List<GameObject> attachedObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (count == attachedObjects.Count)
        {
            onFinished.Invoke();
            enabled = false;
        }
    }

    public void AddCount()
    {
        count += 1;
    }

    public void SubtractCount()
    {
        count -= 1;
    }
}
