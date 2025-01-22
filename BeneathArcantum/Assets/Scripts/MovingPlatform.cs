using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WayPointPath waypointPath;

    [SerializeField]
    private float _speed;
    private Vector3 objectTransform;

    private int targetWaypointIndex;

    private Transform previousWaypoint;
    private Transform targetWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        // 9:18 https://www.youtube.com/watch?v=ly9mK0TGJJo

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / _speed;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        objectTransform = GetObjectTransform(other.gameObject);
        other.transform.SetParent(transform);
        other.gameObject.transform.localScale = objectTransform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

    private Vector3 GetObjectTransform(GameObject other)
    {
        return other.transform.localScale;
    }

}
