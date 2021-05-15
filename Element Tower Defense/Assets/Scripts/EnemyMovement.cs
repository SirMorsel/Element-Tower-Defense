using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 5f;
    private Transform target;
    private int waypointID = 0;

    // Waypoint
    public GameObject waypointGameObjectHolder;
    private Transform[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        GetWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
        SetNewWaypointTarget();
    }

    private void GetWaypoints()
    {
        waypoints = new Transform[waypointGameObjectHolder.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointGameObjectHolder.transform.GetChild(i);
        }
        target = waypoints[0];
    }

    private void SetNewWaypointTarget()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            if (waypointID < waypoints.Length -1)
            {
                waypointID++;
                target = waypoints[waypointID];
            }
        }
    }
}
