using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfos : MonoBehaviour
{

    private Elements bulletElement = Elements.NEUTRAL;
    private float bulletDamage;
    private float bulletSpeed = 10f;
    private float bulletLifetime = 10f; // Lifetime in seconds
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        SetOpticalBulletProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        } else
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * (bulletSpeed * GameManager.Instance.GetGameSpeed()) * Time.deltaTime, Space.World);
            bulletLifetime -= Time.deltaTime;
            if (bulletLifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Chase(Transform enemy)
    {
        target = enemy;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log($"HIT! with a damage of: {bulletDamage}");
            if (!other.gameObject.GetComponent<EnemyStats>().GetEnemyStatus())
            {
                other.gameObject.GetComponent<EnemyStats>().TakeDamage(bulletElement, bulletDamage);
            }
        }
        Destroy(this.gameObject);
    }

    public void SetBulletElementType(Elements elementType)
    {
        bulletElement = elementType;
    }

    public void SetBulletDamage(float damage)
    {
        bulletDamage = damage;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    // Set bullet color and particle effect
    private void SetOpticalBulletProperties()
    {
        switch (bulletElement)
        {
            case Elements.ELECTRO:
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                break;
            case Elements.FIRE:
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                break;
            case Elements.WATER:
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                break;
            default:
                print("An almighty element that is bursting with neutrality. (This is a placeholder element and shouldn't actually appear in the game.");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
        }
    }
}
