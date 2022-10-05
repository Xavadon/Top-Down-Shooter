using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [SerializeField] private bool _enemy = true;
    public static Action EnemyDied;
    [SerializeField] private bool _player = false;
    public static Action PlayerDied;
    public event Action<float> HealthChanged;

    [Space(height: 10)]
    [SerializeField] private RandomSoundPlayer _hurtSoundPlayer;
    [SerializeField] private RandomSoundPlayer _healSoundPlayer;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(1f);
    }

    public void ApplyDamage(float value)
    {
        _hurtSoundPlayer?.PlaySound();
        _currentHealth -= value;
        if (_currentHealth <= 0)
        {
            Death();
        }
        else
        {
            ChangeHealth();
        }
    }

    public void ApplyHeal(float value)
    {
        _healSoundPlayer?.PlaySound();
        _currentHealth += value;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        ChangeHealth();
    }

    private void ChangeHealth()
    {
        float currentHealthAsPercantage = (float)_currentHealth / _maxHealth;
        HealthChanged?.Invoke(currentHealthAsPercantage);
    }

    private void Death()
    {
        HealthChanged?.Invoke(0);
        gameObject.SetActive(false);
        if(_enemy) EnemyDied?.Invoke();
        if(_player) PlayerDied?.Invoke();
    }
}
