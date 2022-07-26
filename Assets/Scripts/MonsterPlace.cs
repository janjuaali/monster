using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MonsterPlace : MonoBehaviour, IJumpable
{
    [HideInInspector] public int Position;

    private const int _playerLayer = 7;
    private bool _battleMode;
    private BoxCollider _boxCollider;
    private JumpAnimation _jumpAnimation = new JumpAnimation();
    public bool IsTaken { get; private set; }
    public Monster Monster { get; private set; }

    public event Action<MonsterPlace> PlaceFree;
    public event Action<int> CurrencyPickedUp;

    public void Take(Monster monster, bool isShopStage = false)
    {
        if(_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        Monster = monster;
        Monster.gameObject.layer = _playerLayer;

        if (isShopStage == false)
            Monster.LevelUp(1);


        Monster.Died += MonsterDied;
        _boxCollider.enabled = true;
        IsTaken = true;
    }

    public void MonsterDied()
    {
        FindObjectOfType<MonsterPoolCreator>().ResetMonster(Monster);
        Free();

       if(_battleMode == false)
            PlaceFree?.Invoke(this);
    }

    public void Free(bool isColliderDisabled = false)
    {
        Monster.Died -= MonsterDied;
        IsTaken = false;
        Monster = null;
        _boxCollider.enabled = isColliderDisabled;
    }

    public void TurnMonsterForward()
    {
        if (Monster != null)
            Monster.transform.localRotation = Quaternion.identity;
    }

    public void Jump(AnimationCurve animationCurve)
    {
        StartCoroutine(_jumpAnimation.Play(animationCurve, transform));
    }

    public void PickUpCurrency(int amount)
    {
        CurrencyPickedUp?.Invoke(amount);
    }

    public void BattleMode()
    {
        if (Monster != null)
            Monster.transform.localPosition = Vector3.zero;

        _battleMode = true;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void EnableCollider()
    {
        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        _boxCollider.enabled = true;
    }

    public void DeActivate()
    {
        if (_boxCollider != null)
            _boxCollider.enabled = false;

        gameObject.SetActive(false);
    }
}
