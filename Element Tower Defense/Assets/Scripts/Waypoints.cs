using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform waypointHolder;
    public static Transform[] waypoints;
    // Start is called before the first frame update
    void Awake()
    {
        waypoints = new Transform[waypointHolder.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            print($"Point {waypointHolder.transform.GetChild(i).name}");
            waypoints[i] = waypointHolder.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Transform[] GetWaypoints()
    {
        return waypoints;
    }
}
