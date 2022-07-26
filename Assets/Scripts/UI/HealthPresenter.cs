using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Slider _slider;
    [SerializeField] private FloatingText _floatingText;

    private float _currentValue;
    private float _changeSpeed => _slider.maxValue;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _monster.Health.HealthChanged += OnHealthChange;
        OnHealthChange(_monster.Health.CurrentHealth, _monster.Health.MaxHealth, 0);
    }

    private void OnDisable()
    {
        _monster.Health.HealthChanged -= OnHealthChange;
    }

    private void OnHealthChange(float currentHealth, float maxHealth, float healAmount)
    {
        _slider.maxValue = maxHealth;
        _currentValue = currentHealth;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(ChangeAnimation());

        if (healAmount <= 0)
            return;

        var heal = Instantiate(_floatingText, transform);
        heal.Init((int)healAmount);
    }

    private IEnumerator ChangeAnimation()
    {
        while (_slider.value != _currentValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _currentValue, _changeSpeed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}
