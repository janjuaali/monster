using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathHandler : MonoBehaviour, IDeathBehavior
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private MonsterAnimator _monsterAnimator;

    private UIHandler _uIHandler;
    private Player _player;

    private void Awake()
    {
        _uIHandler = GetComponent<UIHandler>();
    }

    public void Die(LostCouse lostCouse)
    {
        _stateMachine.enabled = false;
        _monsterAnimator.DieAnimation();
        gameObject.layer = 9;
        _uIHandler.Disable();

        _player = transform.root.GetComponent<Player>();

        if (_player != null)
            _player.OnMonsterDie(lostCouse);
    }
}
