using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    private Transform currentTarget = null;
    private float range = 5f;
    private float searchInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SearchForTarget", 0f, searchInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SearchForTarget()
    {
        // GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> targets = GameObject.Find("GameManager").GetComponent<WaveSpawner>().GetListOfEnemies();
        // print($"Current amount of items in List: {targets.Count}");
        foreach (var target in targets)
        {
            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            // print($"------------------------------------- {distance}");
            if (distance < range && currentTarget == null)
            {
              print("Found Target");
              currentTarget = target.transform;
            }
        }
        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > range)
            {
                print("Target lost");
                currentTarget = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
