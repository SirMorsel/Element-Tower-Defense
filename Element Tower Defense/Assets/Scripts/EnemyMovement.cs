using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2.5f;
    private Transform target;
    private int waypointID = 0;


    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.GetWaypoints()[0];
    } 

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GetComponent<PlayerStats>().IsGameOver())
        {
            MoveToWaypoint();
            SetNewWaypointTarget();
        }
    }

    private void MoveToWaypoint()
    {
        transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
    }

    private void SetNewWaypointTarget()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            if (waypointID < Waypoints.GetWaypoints().Length -1)
            {
                waypointID++;
                target = Waypoints.GetWaypoints()[waypointID];
                // Rotate slime to next waypoint
            } else
            {
                print("Slime reached town!!!!!!");
                GameManager.Instance.gameObject.GetComponent<WaveSpawner>().RemoveEnemyFromList(this.gameObject);
                Destroy(this.gameObject);
                GameManager.Instance.gameObject.GetComponent<PlayerStats>().TakeDamage();
            }
        }
    }
}
