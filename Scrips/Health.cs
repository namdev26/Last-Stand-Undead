using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
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
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    private void OnEnable()
    {
        OnDeath.RemoveListener(Death);
    }
    public void TakeDam(int damage)
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
            healthBar.UpdateHealth(currentHealth, maxHealth);
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
            TakeDam(10);
        }
    }

}