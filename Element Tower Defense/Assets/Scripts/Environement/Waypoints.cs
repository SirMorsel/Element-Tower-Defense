using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] Transform waypointHolder;
    [SerializeField] static Transform[] waypoints;

    // Start is called before the first frame update
    void Awake()
    {
        waypoints = new Transform[waypointHolder.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointHolder.transform.GetChild(i);
        }
    }

    public static Transform[] GetWaypoints()
    {
        return waypoints;
    }
}
