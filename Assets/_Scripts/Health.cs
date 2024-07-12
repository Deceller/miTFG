using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public bool isEnemy = true;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage, " + currentHealth + " health remaining.");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Código para manejar la muerte del objeto
        Debug.Log(gameObject.name + " died.");
        if(isEnemy == false)
        {
            if (gameManager != null)
            {
                gameManager.OnPlayerFell();
            }
        }
        Destroy(gameObject);
    }

    // Método para curar al objeto
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log(gameObject.name + " healed " + amount + " health, " + currentHealth + " health remaining.");
    }
}