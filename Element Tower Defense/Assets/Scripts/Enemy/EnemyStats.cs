using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    // Enemy information
    private Renderer enemyRenderer;
    private float maxHealth = 100F;
    private float health;
    private bool isDead = false;
    private Elements enemyElement = Elements.NEUTRAL;
    private int enemyValue = 25;

    // UI Elements
    private Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
        health = maxHealth;
        healthbar = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        SetEnemyElement();
    }

    // Public Functions
    public void SetDeath()
    {
        if (isDead)
        {
            GameManager.Instance.gameObject.GetComponent<PlayerStats>().CollectCurrency(enemyValue);
            GameManager.Instance.gameObject.GetComponent<WaveSpawner>().RemoveEnemyFromList(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public bool GetEnemyStatus()
    {
        return isDead;
    }

    public void TakeDamage(Elements bulletElementType, float damage)
    {
        if (enemyElement == Elements.NEUTRAL)
        {
            print("Normal Damage");
            health -= damage;
        } else if(enemyElement == bulletElementType)
        {
            print("Immune");
        } else if (enemyElement == Elements.ELECTRO && bulletElementType == Elements.WATER)
        {
            health -= CalcDamage(damage, false);
        } else if (enemyElement == Elements.ELECTRO && bulletElementType == Elements.FIRE)
        {
            health -= CalcDamage(damage, true);
        }
        else if (enemyElement == Elements.FIRE && bulletElementType == Elements.ELECTRO)
        {
            health -= CalcDamage(damage, false);
        }
        else if (enemyElement == Elements.FIRE && bulletElementType == Elements.WATER)
        {
            health -= CalcDamage(damage, true);
        }
        else if (enemyElement == Elements.WATER && bulletElementType == Elements.FIRE)
        {
            health -= CalcDamage(damage, false);
        }
        else if (enemyElement == Elements.WATER && bulletElementType == Elements.ELECTRO)
        {
            health -= CalcDamage(damage, true);
        }

        // Update healthbar
        healthbar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            isDead = true;
            SetDeath();
        }
    }

    // Private Functions
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

    private void SetEnemyElement()
    {
        enemyElement = (Elements)Random.Range(0, 4);
        Color elementColor;

        switch (enemyElement)
        {
            case Elements.NEUTRAL:
                elementColor = Color.white;
                break;
            case Elements.ELECTRO:
                elementColor = Color.magenta;
                break;
            case Elements.FIRE:
                elementColor = Color.red;
                break;
            case Elements.WATER:
                elementColor = Color.blue;
                break;
            default:
                print("Default case (Somthing bad happend)");
                elementColor = Color.white;
                break;
        }
        enemyRenderer.material.SetColor("_Color", elementColor);
    }
}
