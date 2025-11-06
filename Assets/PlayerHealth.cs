using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int startingHealth = 100;

    [SerializeField] int collisionDamage = 10;  

    int currentHealth;
    readonly List<IHealthObserver> observers = new List<IHealthObserver>();

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    void Awake()
    {
        currentHealth = Mathf.Clamp(startingHealth, 0, maxHealth);
    }

    void Start() // Start is called before the first frame update
    {
        NotifyObservers();
    }

    public void RegisterObserver(IHealthObserver observer)
    {
        if (observer == null) return;
        if (!observers.Contains(observer)) observers.Add(observer);
        observer.OnHealthChanged(currentHealth, maxHealth);
    }

    public void UnregisterObserver(IHealthObserver observer)
    {
        if (observer == null) return;
        observers.Remove(observer);
    }

    void NotifyObservers()
    {
        foreach (var obs in observers)
            obs.OnHealthChanged(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        NotifyObservers();

        if (currentHealth == 0)
            OnDeath();
    }


    void OnDeath()
    {
        SceneManager.LoadScene(1);
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == gameObject) return;

        TakeDamage(collisionDamage);

    }

}
