using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    // Manager informations
    private PlayerStats player;
    private WaveSpawner waveSpawner;

    // Tower base informations
    private int towerLv = 1;
    private int towerMaxLv = 3;
    private Elements towerElement = Elements.NEUTRAL;
    private int towerValue = 0;

    // Aiming and detection
    private Transform currentTarget = null;
    private GameObject towerRangeCircle;
    private float range = 8f;
    private float searchInterval = 1f;
    private float rotationSpeed = 5f;

    // Tower offensive informations
    private Transform turretCrystal;
    [SerializeField] GameObject bulletPrefab;
    private Transform bulletSpawn;
    private float towerDamage = 30f;
    private float fireRate = 2f;
    private float fireoffset = 0f;

    // Audio
    private AudioSource source;
    [SerializeField] AudioClip[] elementSounds;


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetComponent<PlayerStats>();
        waveSpawner = GameManager.Instance.GetComponent<WaveSpawner>();
        towerValue = GameManager.Instance.GetComponent<BuildManager>().GetTowerBaseValue();
        turretCrystal = this.transform.GetChild(0);
        towerRangeCircle = this.transform.GetChild(2).gameObject;
        ChangeRangeCircleState(false);
        bulletSpawn = turretCrystal.GetChild(1).transform;
        InvokeRepeating("SearchForTarget", 0f, searchInterval);
        source = gameObject.GetComponent<AudioSource>();
        SetTowerCrystalColor();
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

    // Public Functions
    public void SetTowerElement(Elements towerType)
    {
        towerElement = towerType;
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

    public float GetTowerDamage()
    {
        return towerDamage;
    }

    // Private Functions
    private void SetTowerCrystalColor()
    {
        Renderer turretCrystalRenderer = turretCrystal.GetChild(0).GetChild(0).GetComponent<Renderer>();
        Color turretCrystalColor;
        switch (towerElement)
        {
            case Elements.ELECTRO:
                turretCrystalColor = Color.magenta;
                source.clip = elementSounds[0];
                break;
            case Elements.FIRE:
                turretCrystalColor = Color.red;
                source.clip = elementSounds[1];
                break;
            case Elements.WATER:
                turretCrystalColor = Color.blue;
                source.clip = elementSounds[2];
                break;
            default:
                print("An almighty element that is bursting with neutrality. (This is a placeholder element and shouldn't actually appear in the game.)");
                turretCrystalColor = Color.white;
                break;
        }
        turretCrystalRenderer.material.SetColor("_Color", turretCrystalColor);
    }

    private void SearchForTarget()
    {
        List<GameObject> targets = waveSpawner.GetListOfEnemies();
        foreach (var target in targets)
        {
            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < range && currentTarget == null)
            {
              currentTarget = target.transform;
            }
        }
        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > range)
            {
                currentTarget = null;
            }
        }
    }

    private void LockTarget()
    {
        Vector3 direction = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turretCrystal.rotation, lookRotation, (rotationSpeed * GameManager.Instance.GetGameSpeed()) * Time.deltaTime).eulerAngles;
        turretCrystal.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Fire()
    {
        fireoffset -= Time.deltaTime;
        if (fireoffset <= 0f && !player.IsGameOver())
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            source.PlayOneShot(source.clip, AudioManager.Instance.GetSFXVolume());
            bullet.GetComponent<BulletInfos>().SetBulletElementType(towerElement);
            bullet.GetComponent<BulletInfos>().SetBulletDamage(towerLv * towerDamage);
            bullet.GetComponent<BulletInfos>().Chase(currentTarget);

            fireoffset = fireRate / GameManager.Instance.GetGameSpeed();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
