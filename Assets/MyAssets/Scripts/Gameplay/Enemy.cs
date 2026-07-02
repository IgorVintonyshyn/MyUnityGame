using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDeath;
    public GameObject crystalPrefab;

    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthBarFill;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(crystalPrefab, transform.position, Quaternion.identity);
        OnDeath?.Invoke(this);

        Destroy(gameObject);
    }

}