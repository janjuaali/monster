using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class AbilityUpgradeShopView : ShopButton
{
    [SerializeField] private Ability _ability;
    [SerializeField] private TMP_Text _upgradeValue;
    [SerializeField] private TMP_Text _abilityName;
    [SerializeField] private float _value;
    [SerializeField] private string _abbreviation;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _lockedImage;
    [SerializeField] private int _unlockLevel;
    [SerializeField] private TMP_Text _levelNumber;

    private const string SaveName = "AbilityUpgradeShopView";

    private void Awake()
    {
        _ability.LoadStats();
        int currentLevel = 1 + SaveSystem.LoadLevelsProgression();

        LoadProgression(SaveName);

        UpdateInfo();
        _abilityName.text = $"{_ability.AbilityName}";
        _icon.sprite = _ability.Icon;

        if (currentLevel < _unlockLevel)
        {
            _lockedImage.gameObject.SetActive(true);
            _ability.gameObject.SetActive(false);
            _levelNumber.text = $"{_unlockLevel}";
        }
        else
        {
            _lockedImage.gameObject.SetActive(false);
        }
    }

    public override void Buy(float cost)
    {
        _ability.ValueHandler.Increase(_value);
    }

    protected override void UpdateInfo()
    {
        _upgradeValue.text = $"{_ability.ValueHandler.Value}{_abbreviation}";
        base.UpdateInfo();
    }
}
