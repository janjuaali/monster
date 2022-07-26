using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHandler
{
    private float _value;
    private float _maxValue;
    private float _minValue;

    private string _saveName;

    public float Value => _value;
    public float MaxValue => _maxValue;

    public event Action<float, float> ValueIncreased;
    public event Action<float> ValueDecreased;
    public event Action<float> ValueLoaded;
    public event Action ValueChanged;

    public ValueHandler(float minValue, float maxValue, string SaveName)
    {
        _saveName = SaveName;
        _minValue = minValue;
        _maxValue = maxValue;
        _value = _minValue;
    }

    public void Increase(float value, bool triggerEvent = true)
    {
        ChangeAmount(value);

        if(triggerEvent)
            ValueIncreased?.Invoke(_value, value);
    }

    public void SetValue(float value)
    {
        _value = value;
        ChangeAmount(0);
    }

    public bool TryDecrease(float value)
    {
        if (IsEnough(value))
        {
            Decrease(value);
            return true;
        }

        return false;  
    }

    public bool IsEnough(float value)
    {
        return _value >= value;
    }

    private void Decrease(float value)
    {
        ChangeAmount(-value);
        ValueDecreased?.Invoke(-value);
    }

    public void ChangeAmount(float value)
    {
        _value += value;

        _value = Mathf.Clamp(_value, _minValue, _maxValue);

        Save();
        ValueChanged?.Invoke();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(_saveName, _value);
    }

    public float LoadAmount()
    {
        _value = _minValue;

        if (PlayerPrefs.HasKey(_saveName))
            _value =  PlayerPrefs.GetFloat(_saveName);

        ValueLoaded?.Invoke(_value);
        return _value;
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(_saveName);
    }
}
