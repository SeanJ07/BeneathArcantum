using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Camera mainCam;
    public GameObject pauseScreen;
    public GameObject levelCompletedScreen;
    public TextMeshProUGUI levelCompletedText;

    [Header("GameObjects")]
    GameObject player;
    public GameObject currentCheckpoint;


    [Header("Checkpoints")]
    public GameObject startPoint;
    public GameObject endPoint;

    [Header("StoredInfo")]
    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        player.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, player.transform.position.z);
        currentCheckpoint = startPoint.gameObject;
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

    public void LevelFinish()
    {
        Debug.Log("Finished Level");
        levelCompletedScreen.gameObject.SetActive(true);
        levelCompletedText.text = "Level " + level + " Completed!";
        level += 1;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<PlayerMovement>().enabled = false;
        
    }
    
}
