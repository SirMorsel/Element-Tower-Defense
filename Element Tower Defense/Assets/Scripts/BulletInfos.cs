using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfos : MonoBehaviour
{

    private float damage = 10;
    private Elements bulletElement = Elements.NEUTRAL;

    // Start is called before the first frame update
    void Start()
    {
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

    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT!");
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
        damage *= towerLv;
    }
}
