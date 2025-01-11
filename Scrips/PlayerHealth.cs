using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHealth;
    int currentHealth;
    public float safeTime;
    float _safeTimeCooldown;
    public HealthBar healthBar;
    public UnityEvent OnDeath;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private void OnEnable()
    {
        OnDeath.RemoveListener(Death);
    }
    public void TakeDamage(int damage)
    {
        if (_safeTimeCooldown <= 0)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                OnDeath.Invoke();
            }
            _safeTimeCooldown =  safeTime;
            healthBar.UpdateBar(currentHealth, maxHealth);
        }
    }
    // Update is called once per frame
    public void Death()
    {

        Destroy(gameObject);
    }
    private void Update()
    {   
        _safeTimeCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

}
