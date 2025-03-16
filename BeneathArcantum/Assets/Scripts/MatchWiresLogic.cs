using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchWiresLogic : MonoBehaviour
{
    static MatchWiresLogic Instance;

    public int maxWires = 3;
    public GameObject wireObject;
    private int points = 0;
    private GameObject door;
    private PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        door = GameObject.FindGameObjectWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void UpdateWires()
    {
        if(points == maxWires)
        {
            door.GetComponent<DoorOpening>().DoorOpen();
            wireObject.SetActive(false);
            player.locked = false;
        }
    }

    public static void AddPoint()
    {
        AddPoints(1);
    }

    public static void AddPoints(int points)
    {
        Instance.points += points;
        Instance.UpdateWires();
    }
}
