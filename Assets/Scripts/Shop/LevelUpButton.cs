using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelUpButton : ShopButton
{
    [SerializeField] private int _levelsPerBuy;
    [SerializeField] private TMP_Text _levels;

    private const string SaveName = "LevelUpButton";
    private MonstersHandler _monstersHandler;
    private void Awake()
    {
        LoadProgression(SaveName);
        UpdateInfo();
        _monstersHandler = FindObjectOfType<MonstersHandler>();

        _monstersHandler.LevelUpAllMonster((int)ValueHandler.Value-1);
    }

    public override void Buy(float cost)
    {
        ValueHandler.Increase(_levelsPerBuy);
        _monstersHandler.LevelUpAllMonster(_levelsPerBuy);
    }

    protected override void UpdateInfo()
    {
        _levels.text = $"Lv.{ValueHandler.Value}";
        base.UpdateInfo();
    }
}
