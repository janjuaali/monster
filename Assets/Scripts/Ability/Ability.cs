using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField] private string _abilityName;
    [SerializeField] private float _minValue;
    [SerializeField] private Image _icon;
    [SerializeField] private bool _isInfinite;

    private ValueHandler _valueHandler;
    private ValueHandler _amountHandler;
    private float _elapsedTime = 1;
    private IntegrationMetric _integrationMetric = new IntegrationMetric();

    public Sprite Icon => _icon.sprite;
    public string AbilityName => _abilityName;
    public bool IsOnCooldown { get; private set; }
    public ValueHandler ValueHandler => _valueHandler;
    public ValueHandler AmountHandler => _amountHandler;
    public float ElapsedTime => _elapsedTime;

    private void Awake()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        if (_valueHandler != null)
            return;

        _valueHandler = new ValueHandler(_minValue, 100000, _abilityName);
        _amountHandler = new ValueHandler(0, 1000, _abilityName + 1);
        AmountHandler.LoadAmount();
        ValueHandler.LoadAmount();
    }

    public virtual void Cast() { }

    public void Use()
    {
        if (IsOnCooldown == false)
        {
            if (_amountHandler.TryDecrease(1) || _isInfinite)
            {
                StartCoroutine(Cooldown());
                Cast();
                OnAbilityCasted();
                _integrationMetric.OnAbiltyUsed(_abilityName);
            }
        }
    }

    protected virtual void OnAbilityCasted() { }
    private IEnumerator Cooldown()
    {
        float speed = 1 / _coolDown;
        IsOnCooldown = true;

        while (_elapsedTime > 0)
        {
            _elapsedTime = Mathf.MoveTowards(_elapsedTime, 0, speed * Time.deltaTime);

            yield return null;
        }

        _elapsedTime = 1;
        IsOnCooldown = false;
    }
}
