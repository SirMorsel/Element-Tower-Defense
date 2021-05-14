using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int level;
    private float health = 100.00F;
    private bool isDead = false;
    private Elements monsterElement = Elements.NEUTRAL;
    // Start is called before the first frame update
    void Start()
    {
        // Set monster element
        monsterElement = (Elements)Random.Range(0, 4);
        SetMonsterElement(monsterElement);
    }

    // Update is called once per frame
    void Update()
    {
        SetDeath();
    }

    public void SetDeath()
    {
        if (isDead)
        {
            Debug.Log($"Enemy health: {health}");
            // Increase player coins
            Destroy(this.gameObject);
        }
    }

    public bool GetMonsterStatus()
    {
        return isDead;
    }

    public void TakeDamage(Elements elementType, float damage)
    {
        if (monsterElement == elementType)
        {
            print("Normal Damage");
            health -= damage;
        } else if(monsterElement == elementType)
        {
            print("Immune");
        } else if (monsterElement == Elements.ELECTRO && elementType == Elements.WATER)
        {
            health -= CalcDamage(damage, true);
        } else if (monsterElement == Elements.ELECTRO && elementType == Elements.FIRE)
        {
            health -= CalcDamage(damage, false);
        }
        else if (monsterElement == Elements.FIRE && elementType == Elements.ELECTRO)
        {
            health -= CalcDamage(damage, true);
        }
        else if (monsterElement == Elements.FIRE && elementType == Elements.WATER)
        {
            health -= CalcDamage(damage, false);
        }
        else if (monsterElement == Elements.WATER && elementType == Elements.FIRE)
        {
            health -= CalcDamage(damage, true);
        }
        else if (monsterElement == Elements.WATER && elementType == Elements.ELECTRO)
        {
            health -= CalcDamage(damage, false);
        }

        if (health <= 0)
        {
            isDead = true;
        }
    }

    public void SetDamageToPlayer()
    {
        // Decrease Player healh
        Destroy(this.gameObject);
    }

    private float CalcDamage(float damage, bool isEffective)
    {
        if (isEffective)
        {
            return damage * 2;
        } else
        {
            return damage / 2;
        }
    }

    private void SetMonsterElement(Elements elementType)
    {
        switch (elementType)
        {
            case Elements.NEUTRAL:
                print("Case Electro");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
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
                print("Default case (Somthing bad happend)");
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
        }
    }
}
