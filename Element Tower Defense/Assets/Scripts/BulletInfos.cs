using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfos : MonoBehaviour
{

    private Elements bulletElement = Elements.NEUTRAL;
    private float damage = 10f;
    private float bulletSpeed = 10f;
    private float bulletLifetime = 10f; // Lifetime in seconds
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
       // bulletLiftimeCounter = bulletLifetime;
        Debug.Log($"Bullet {bulletElement} says hello with a damage of: {damage}");
        switch (bulletElement)
        {
            case Elements.ELECTRO:
                print("Case Electro");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                break;
            case Elements.FIRE:
                print("Case Fire");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            case Elements.WATER:
                print("Case Water");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                break;
            default:
                print("An almighty element that is bursting with neutrality. (This is a placeholder element and shouldn't actually appear in the game.");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
        }
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
            transform.Translate(direction.normalized * bulletSpeed * Time.deltaTime, Space.World);
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
            Debug.Log($"HIT! with a damage of: {damage}");
            if (!other.gameObject.GetComponent<EnemyStats>().GetMonsterStatus())
            {
                other.gameObject.GetComponent<EnemyStats>().TakeDamage(bulletElement, damage);
            }
        }
        Destroy(this.gameObject);
    }

    public void SetBulletElementType(Elements elementType)
    {
        bulletElement = elementType;
    }

    public void SetBulletDamage(int towerLv)
    {
        damage = damage * towerLv;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
}
