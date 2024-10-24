using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        Vector3 playerPosition = player.transform.position;

        mainCam.transform.position = playerPosition + new Vector3(0, 0, -10);
    }

    void StartGame()
    {

    }
}
