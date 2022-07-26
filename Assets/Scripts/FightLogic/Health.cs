using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField] private float _healthPerLevel;
    [SerializeField] private float _healthScaler;

    private float _maxHealth;
    private float _currentHealth;
    private StatScaler _statScaler = new StatScaler(); 

    public float HealthPerLevel => _healthPerLevel;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public event Action<float, float, float> HealthChanged;

    public void Init(float level)
    {
        var health = _statScaler.ScaleByLevel(level, _healthScaler, _healthPerLevel);

        _maxHealth = health;
        _currentHealth = health;

        HealthChanged?.Invoke(_currentHealth, _maxHealth, 0);
    }

    public void Decrease(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            _currentHealth = 0;

        HealthChanged?.Invoke(_currentHealth, _maxHealth, -damage);
    }

    public void Increase(float health)
    {
        _currentHealth += health;

        if (_currentHealth >= MaxHealth)
            _currentHealth = MaxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth, health);
    }

    public void IncreaseMaxHealth(float level)
    {
        var health = CalctulateHealth(level);

        _maxHealth = health;
        _currentHealth = health;
    }

    private float CalctulateHealth(float level)
    {
        return level * _healthPerLevel;
    }
}
