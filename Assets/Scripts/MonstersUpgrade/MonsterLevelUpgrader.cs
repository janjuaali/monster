using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MonsterLevelUpgrader : ShopButton
{
    [SerializeField] private MonsterPlaceAccepter _monstarAccepter;
    [SerializeField] private int _levelPerBuy;

    private MonstersHandler _monstersHandler;
    private bool _isInitialized;
    private Monster _previousMonster;
    private string SaveName => $"MonsterCellLevelHandler{_monstarAccepter.Monster.Name}";

    public void Init()
    {
        LoadProgression(SaveName);

        if(_previousMonster != null)
            _isInitialized = _previousMonster.GetType() == _monstarAccepter.Monster.GetType();

        if(_isInitialized == false && _monstarAccepter.Monster.LvlLoaded == false)
        {
            AddLvl(_monstarAccepter.Monster, (int)ValueHandler.LoadAmount() - 1);
            IncreaseMonstersMight((int)ValueHandler.LoadAmount() - 1);
            _previousMonster = _monstarAccepter.Monster;
            _monstarAccepter.Monster.LvlLoaded = true;
        }

        SetInactive();

        UpdateInfo();

        OnValueChanged();

        _isInitialized = true;
    }

    public override void Buy(float cost)
    {
        ValueHandler.Increase(_levelPerBuy);
        AddLvl(_monstarAccepter.Monster, _levelPerBuy);

        IncreaseMonstersMight(_levelPerBuy);

        SetInactive();
    }

    protected override void UpdateInfo()
    {
        _monstarAccepter.MonsterInfoPanel.UpdateInfo();
        base.UpdateInfo();
    }

    private void AddLvl(Monster monster, int level)
    {
        monster.LevelUp(level);
    }

    private void IncreaseMonstersMight(int value)
    {
        if (_monstersHandler == null)
            _monstersHandler = FindObjectOfType<MonstersHandler>();

        _monstersHandler.ChangeMonstersMight(value);
    }

    private void SetInactive()
    {
        if (_monstarAccepter.Monster.Level >= 10)
            SetInactive(true);
        else
            SetInactive(false);
    }
}
