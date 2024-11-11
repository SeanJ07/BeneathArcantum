using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public GameObject pauseScreen;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
        PauseGame();
    }

    void CameraFollow()
    {
        Vector3 playerPosition = player.transform.position;

        mainCam.transform.position = playerPosition + new Vector3(0, 0, -10);
    }

    void StartGame()
    {

    }
    
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ResumeButton()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {

        SceneManager.LoadScene("GameMenu");
    }
}
