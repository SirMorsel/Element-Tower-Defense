using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    private int towerLv = 1;
    private int towerMaxLv = 3;
    private Elements towerElement = Elements.NEUTRAL;
    private int towerValue = 0;

    private Transform currentTarget = null;
    private GameObject towerRangeCircle;
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
        towerValue = GameManager.Instance.GetComponent<BuildManager>().GetTowerBaseValue();
        turretCrystal = this.transform.GetChild(1).transform;
        towerRangeCircle = this.transform.GetChild(2).gameObject;
        ChangeRangeCircleState(false);
        bulletSpawn = turretCrystal.GetChild(0).transform;
        InvokeRepeating("SearchForTarget", 0f, searchInterval);
        print($"Base value of {towerValue}");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            LockTarget();
            Fire();
        }
        if (turretCrystal != null)
        {
           SetTowerCrystalColor();
        }
        // print($"Tower Element {towerElement}");
    }

    public void SetTowerElement(Elements towerType)
    {
        towerElement = towerType;
        print($"ELEMENT {towerElement}");
        print($"CRYSTAL {turretCrystal}");
        // SetTowerCrystalColor();
    }

    public Elements GetTowerType()
    {
        return towerElement;
    }

    public void UpgradeTower()
    {
        towerLv++;
        towerValue = towerValue * towerLv;
    }

    public int GetTowerLv()
    {
        return towerLv;
    }

    public int GetTowerMaxLv()
    {
        return towerMaxLv;
    }

    public int GetTowerValue()
    {
        return towerValue;
    }

    public void ChangeRangeCircleState(bool state)
    {
        towerRangeCircle.gameObject.SetActive(state);
    }

    private void SetTowerCrystalColor()
    {
        switch (towerElement)
        {
            case Elements.ELECTRO:
                // print("Case Electro");
                turretCrystal.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                break;
            case Elements.FIRE:
                // print("Case Fire");
                turretCrystal.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            case Elements.WATER:
                // print("Case Water");
                turretCrystal.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                break;
            default:
                print("An almighty element that is bursting with neutrality. (This is a placeholder element and shouldn't actually appear in the game.");
                turretCrystal.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
        }
    }
    private void SearchForTarget()
    {
        // GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        //List<GameObject> targets = GameObject.Find("GameManager").GetComponent<WaveSpawner>().GetListOfEnemies();
        List<GameObject> targets = GameManager.Instance.gameObject.GetComponent<WaveSpawner>().GetListOfEnemies();
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
        if (fireoffset <= 0f && !GameManager.Instance.GetComponent<PlayerStats>().IsGameOver())
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
