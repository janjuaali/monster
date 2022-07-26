using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOpener : ShopButton
{
    [SerializeField] private MonsterCell _monsterCell;
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private bool _reward;

    private string _saveName = "MonsterUpgraderHandler";
    public bool Reward => _reward;

    public event Action Opened;

    private void Awake()
    {
        _saveName = $"{_saveName}{_monsterCell.Monster.Name}";
        LoadProgression(_saveName);
    }

    public override void Buy(float cost)
    {
        OpenCell();

        Opened?.Invoke();
        gameObject.SetActive(false);
    }

    public void OpenCell()
    {
        _monsterCell.Open();

        DisableRewardPanel();
    }

    public void DisableRewardPanel()
    {
        _rewardPanel.SetActive(false);
    }
}
