using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    private int towerLv = 3;
    private Elements towerElement = Elements.WATER;

    private Transform currentTarget = null;
    private float range = 5f;
    private float searchInterval = 1f;
    private float rotationSpeed = 5f;

    private Transform turretCrystal;

    public GameObject bulletPrefab;
    private Transform bulletSpawn;
    private float fireRate = 2f;
    private float fireoffset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        turretCrystal = this.transform.GetChild(1).transform;
        bulletSpawn = turretCrystal.GetChild(0).transform;
        InvokeRepeating("SearchForTarget", 0f, searchInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            LockTarget();
            Fire();
        }
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

    private void LockTarget()
    {
        Vector3 direction = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turretCrystal.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        turretCrystal.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Fire()
    {
        fireoffset -= Time.deltaTime;
        if (fireoffset <= 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<BulletInfos>().SetBulletElementType(towerElement);
            bullet.GetComponent<BulletInfos>().SetBulletDamage(towerLv);
            bullet.GetComponent<BulletInfos>().Chase(currentTarget);

            fireoffset = fireRate;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
