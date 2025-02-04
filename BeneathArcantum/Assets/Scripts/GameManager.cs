using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public SceneStuff sceneStuff;
    public Camera mainCam;
    public GameObject pauseScreen;
    public GameObject levelCompletedScreen;
    public TextMeshProUGUI levelCompletedText;

    public GameObject deathScreen;

    [Header("GameObjects")]
    GameObject player;


    [Header("Checkpoints")]
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject currentCheckpoint;
    private float respawnTransitionTimer;

    [Header("StoredInfo")]
    private int level = 1;

    public bool threeDCam;
    private bool camTransitioning;

    private AudioSource gameAudio;
    public AudioClip win;
    public AudioClip lose;



    // Start is called before the first frame update
    void Start() // Assigns everything, including: Player gameobject, player position, the first checkpoint, the game audiosource, and the scene manager.
    {
        player = GameObject.Find("Player");
        Time.timeScale = 1;
        //pauseScreen.SetActive(false);
        player.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, player.transform.position.z);
        currentCheckpoint = startPoint.gameObject;
        gameAudio = Camera.main.GetComponent<AudioSource>();
        sceneStuff = GameObject.Find("GameManager").GetComponent<SceneStuff>();

    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
        PauseGame();
    }

    void CameraFollow() // Depends on if topDown is checked in the gamemanager. makes camera follow player.
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
            float horizontal = Input.GetAxis("Horizontal2");

            mainCam.transform.position = playerPosition + new Vector3(0 + horizontal, 8, -10);
            mainCam.transform.rotation =  Quaternion.Euler(45, 0, 0);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    
    public void PauseGame()
    {
        
    }
    public void ResumeButton() // Reseumes game
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton() // Loads the main menu.
    {

        SceneManager.LoadScene("GameMenu");
    }

    public void LevelFinish() // When player reaches the final checkpoint (end point), finish the level.
    {
        Debug.Log("Finished Level");
        levelCompletedScreen.gameObject.SetActive(true);
        gameAudio.PlayOneShot(win);
        level += 1;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<PlayerController>().enabled = false;
        
    }

    public void Respawn() // Starts the respawn ienumerator (look down further).
    {
        StartCoroutine(RespawnIEnumerator());
    }
    public IEnumerator RespawnIEnumerator() // Fade black and back respawn, respawns player at last checkpoint.
    {
        
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(sceneStuff.SceneTransitioningIn());
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerController>().UpdateHealth(player.GetComponent<PlayerController>().maxHealth);
        player.transform.position = currentCheckpoint.transform.position;
        StartCoroutine(sceneStuff.SceneTransitioningOut());
        Debug.Log("5");
        Time.timeScale = 1f;
    }
    
}
