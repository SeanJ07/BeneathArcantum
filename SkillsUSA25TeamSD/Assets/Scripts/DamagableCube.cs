using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableCube : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Stops play mode
            UnityEditor.EditorApplication.isPlaying = false;
            //Use "Application.Quit();" for a build

            Debug.Log("Player Died, Game Lost");
        }
    }
}
