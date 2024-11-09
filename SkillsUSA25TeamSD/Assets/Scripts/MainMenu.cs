using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //if we replace the rawimage might need to change this to game object
    public GameObject startScreen;
    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        //load scene main game. if we have multiple levels make a new scene or new object with more buttons for more levels.
        SceneManager.LoadScene("MainGame");
    }
    public void LoadSandBox()
    {
        //load sandbox
        SceneManager.LoadScene("SandBox");
    }
}
