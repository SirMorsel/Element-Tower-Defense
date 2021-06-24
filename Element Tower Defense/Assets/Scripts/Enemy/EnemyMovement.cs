using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Manager informations
    private PlayerStats player;
    private WaveSpawner waveSpawner;

    // Movement informations
    private float speed = 2.5f;
    private float rotationSpeed = 5f;
    private Transform target;
    private int waypointID = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.GetWaypoints()[0];
        player = GameManager.Instance.GetComponent<PlayerStats>();
        waveSpawner = GameManager.Instance.GetComponent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsGameOver())
        {
            LookAtWaypoint();
            MoveToWaypoint();
            SetNewWaypointTarget();
        }
    }

    private void MoveToWaypoint()
    {
        transform.Translate((target.position - transform.position).normalized * (speed * GameManager.Instance.GetGameSpeed()) * Time.deltaTime);
    }

    private void SetNewWaypointTarget()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {

            if (waypointID < Waypoints.GetWaypoints().Length - 1)
            {
                waypointID++;
                target = Waypoints.GetWaypoints()[waypointID];
            }
            else
            {
                waveSpawner.RemoveEnemyFromList(this.gameObject);
                Destroy(this.gameObject);
                player.TakeDamage();
            }
        }
    }

    private void LookAtWaypoint()
    {
        Vector3 direction = target.position - transform.GetChild(0).position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.GetChild(0).rotation, lookRotation, (rotationSpeed * GameManager.Instance.GetGameSpeed()) * Time.deltaTime).eulerAngles;
        transform.GetChild(0).rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
