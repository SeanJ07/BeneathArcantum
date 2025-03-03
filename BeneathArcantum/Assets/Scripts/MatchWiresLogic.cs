using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchWiresLogic : MonoBehaviour
{
    static MatchWiresLogic Instance;

    public int maxWires = 3;
    private int points = 0;
    private GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        door = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void UpdateWires()
    {
        if(points == maxWires)
        {
            door.GetComponent<DoorOpening>().DoorOpen();
        }
    }

    public static void AddPoint()
    {
        AddPoints(1);
    }

    public static void AddPoints(int points)
    {
        Instance.points += points;

    }
}
