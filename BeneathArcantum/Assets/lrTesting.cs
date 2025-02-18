using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    // Start is called before the first frame update
    void Start()
    {
        line.SetUpLine(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
