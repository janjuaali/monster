using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyMultiplierReward : DailyRewardBehaviour
{
    [SerializeField] private float _multiplier;
    [SerializeField] private TMP_Text _amountText;

    public override void Acquire()
    {
        FindObjectOfType<CurrencyMultilplierButton>().InreaseMultiplier(_multiplier);
    }

    public override void UpdateInfo()
    {
        _amountText.text = $"x{_multiplier}";
    }
}
