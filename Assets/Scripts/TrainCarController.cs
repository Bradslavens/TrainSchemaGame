using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainCarController : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 2f;
    public bool isMoving = true;

    private int currentWaypointIndex = 0;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTrainCar();
        }
    }

    private void MoveTrainCar()
    {
        if (currentWaypointIndex >= waypoints.Count)
        {
            isMoving = false;
            uiManager.ShowScoreBoard(); // Call ShowScoreBoard when the last waypoint is reached
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = new Vector3(targetWaypoint.position.x, targetWaypoint.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentWaypointIndex++;
        }
    }

    public void StopTrainCar()
    {
        isMoving = false;
    }

    public void ResumeTrainCar()
    {
        isMoving = true;
    }
}
