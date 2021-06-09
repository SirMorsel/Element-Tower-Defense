﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private float health = 100.00F;
    private bool isDead = false;
    private Elements monsterElement = Elements.NEUTRAL;

    private int monsterValue = 25;
    // Start is called before the first frame update
    void Start()
    {
        SetMonsterElement();
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
            GameManager.Instance.gameObject.GetComponent<PlayerStats>().CollectCurrency(monsterValue);
            GameManager.Instance.gameObject.GetComponent<WaveSpawner>().RemoveEnemyFromList(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public bool GetMonsterStatus()
    {
        return isDead;
    }

    public void TakeDamage(Elements bulletElementType, float damage)
    {
        if (monsterElement == Elements.NEUTRAL)
        {
            print("Normal Damage");
            health -= damage;
        } else if(monsterElement == bulletElementType)
        {
            print("Immune");
        } else if (monsterElement == Elements.ELECTRO && bulletElementType == Elements.WATER)
        {
            health -= CalcDamage(damage, false);
        } else if (monsterElement == Elements.ELECTRO && bulletElementType == Elements.FIRE)
        {
            health -= CalcDamage(damage, true);
        }
        else if (monsterElement == Elements.FIRE && bulletElementType == Elements.ELECTRO)
        {
            health -= CalcDamage(damage, false);
        }
        else if (monsterElement == Elements.FIRE && bulletElementType == Elements.WATER)
        {
            health -= CalcDamage(damage, true);
        }
        else if (monsterElement == Elements.WATER && bulletElementType == Elements.FIRE)
        {
            health -= CalcDamage(damage, false);
        }
        else if (monsterElement == Elements.WATER && bulletElementType == Elements.ELECTRO)
        {
            health -= CalcDamage(damage, true);
        }
        if (health <= 0)
        {
            isDead = true;
        }
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

    private void SetMonsterElement()
    {
        monsterElement = (Elements)Random.Range(0, 4);
        switch (monsterElement)
        {
            case Elements.NEUTRAL:
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
            case Elements.ELECTRO:
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                break;
            case Elements.FIRE:
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            case Elements.WATER:
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                break;
            default:
                print("Default case (Somthing bad happend)");
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                break;
        }
    }
}