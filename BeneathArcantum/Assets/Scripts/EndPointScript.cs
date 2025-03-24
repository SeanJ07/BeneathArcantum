using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointScript : MonoBehaviour
{
    private GameManager gameManager;
    private SceneStuff scenes;
    public string sceneToTransition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scenes = GameObject.Find("GameManager").GetComponent<SceneStuff>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // if the endpoint detects a player entering its collider, it will finish level.
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.LevelFinish();
            LoadScene();
        }
    }

    public void LoadScene()
    {
        scenes.SceneTransitioner(sceneToTransition);
    }
}
