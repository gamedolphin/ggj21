using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] int maxHealth = 100;

    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void Damage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        UpdateHealthText();
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        //todo: add death fx
        Destroy(gameObject);
    }

    private void UpdateHealthText()
    {
        healthText.text = string.Format("Health: {0}", currentHealth);
    }
}
