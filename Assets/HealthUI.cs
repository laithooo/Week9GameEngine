using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IHealthObserver
{
    [SerializeField] Text healthText; 
    PlayerHealth playerHealth;

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            playerHealth = GameManager.Instance.player;
            playerHealth.RegisterObserver(this);
        }
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        if (healthText != null)
            healthText.text = $"Health: {currentHealth}";
    }
}
