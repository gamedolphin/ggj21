using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject deathFx;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int damageFromBoid = 100;

    int currentHealth;
    Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
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
        myAnim.SetTrigger("die");
    }

    private void UpdateHealthText()
    {
        healthText.text = string.Format("Health: {0}", currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boid")
        {
            Damage(damageFromBoid);
        }
    }

    //Animation Event
    public void PlayDeathVfx()
    {
        Instantiate(deathFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
