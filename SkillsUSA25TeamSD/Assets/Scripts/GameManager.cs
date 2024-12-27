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

    public bool threeDCam;
    private bool camTransitioning;

    private AudioSource gameAudio;
    public AudioClip win;
    public AudioClip lose;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        player.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, player.transform.position.z);
        currentCheckpoint = startPoint.gameObject;
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
        PauseGame();
    }

    void CameraFollow()
    {
        // takes the position of the player
        Vector3 playerPosition = player.transform.position;

        if (threeDCam == false)
        {
            // makes the camera follow the position of the player, but adds the additional vector3 to its position.
            mainCam.transform.position = playerPosition + new Vector3(0, 0, -20);
            mainCam.transform.rotation = new Quaternion(0, 0, 0,0);
        }
        else if (threeDCam == true)
        {
            mainCam.transform.position = playerPosition + new Vector3(7.5f, 7, 0);
            mainCam.transform.rotation = new Quaternion(20, -90, 0, 0);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    
    public void PauseGame()
    {
        // Pauses the game when escape is pressed.
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
        gameAudio.PlayOneShot(win);
        level += 1;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<PlayerController>().enabled = false;
        
    }
    
}
