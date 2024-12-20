using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableCube : MonoBehaviour
{

    private GameManager gameManager;
    private SceneStuff sceneManager;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sceneManager = GameObject.Find("GameManager").GetComponent<SceneStuff>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = gameManager.currentCheckpoint.transform.position;
        }
    }
}
