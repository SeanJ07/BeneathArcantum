using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPath : MonoBehaviour
{

    // This script is used to direct the path of a platform by using empty waypoint gameobjects.
    public Transform GetWaypoint(int waypointIndex)
    {
        // There will be a waypoint collection gameobject for storing waypoints for each individual moving platform.
        // In these waypoint collections, there will be a set of waypoints that the platform must follow.
        // The GetWaypoint function gets the value of the current child waypoint in the waypoint collection. (GO TO MOVINGPLATFORM SCRIPT)

        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        // This function finds the next waypoint in the waypoint collection in order to direct the path.

        int nextWaypointIndex = currentWaypointIndex + 1;

        // Once the current waypoint reaches the max, the next waypoint reaches the first one (the beginning).
        if(nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
