using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 5f;
    private Transform target;
    private int waypointID = 0;



    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.GetWaypoints()[0];
        print($"{this.name} current target is {target.name}");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
        SetNewWaypointTarget();
    }

    private void SetNewWaypointTarget()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            if (waypointID < Waypoints.GetWaypoints().Length -1)
            {
                waypointID++;
                target = Waypoints.GetWaypoints()[waypointID];
            }
        }
    }
}
