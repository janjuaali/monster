using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityUsageShopView : ShopButton
{
    [SerializeField] private Ability _ability;
    [SerializeField] private TMP_Text _upgradeValue;
    [SerializeField] private TMP_Text _abilityName;
    [SerializeField] private float _value;
    [SerializeField] private string _abbreviation;
    [SerializeField] private Image _icon;

    private const string SaveName = "AbilityUsageShopView";
    private void Awake()
    {
        LoadProgression(SaveName);
        _upgradeValue.text = $"+{_value}{_abbreviation}";
        _abilityName.text = $"{_ability.AbilityName}";
        _icon.sprite = _ability.Icon;
    }

    public override void Buy(float cost)
    {
        _ability.AmountHandler.Increase(_value);
    }
}
