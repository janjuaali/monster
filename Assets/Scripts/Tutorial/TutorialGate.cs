using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGate : Interactable
{
    [SerializeField] private MonsterList _monsterList;
    [SerializeField] private GateIcon _gateIcon;

    private Monster _monster;
    private bool _isActivated;

    private const int _level = 10;

    private void Awake()
    {
        SetMonster();
    }
    public void SetMonster()
    {
        _monster = _monsterList.GetRandomMonster();
        _gateIcon.CreateIcon(_monster);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        if (_isActivated)
            return;

        _isActivated = monstersHandler.TrySetMonsterToPlace(_monster, _level);
    }
}
