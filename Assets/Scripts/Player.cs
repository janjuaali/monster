using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Examples;

public class Player : MonoBehaviour
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private PlayerDeathHandler _deathHandler;
    [SerializeField] private UIHandler _uIHandler;
    [SerializeField] private Collider _collider;
    [SerializeField] private MouseInput _mouseInput;

    private float _multiplier = 1f;
    private int _counter;
    private ValueHandler _currencyHandler = new ValueHandler(0, 100000, "Currency");

    public MouseInput MouseInput => _mouseInput;
    public int Might => _monstersHandler.MonsterMight;
    public ValueHandler CurrencyHandler => _currencyHandler;
    public UIHandler UiHandler => _uIHandler;

    private void Awake()
    {
        _currencyHandler.LoadAmount();
    }

    private void OnEnable()
    {
        _monstersHandler.CurrencyPickedUp += OnCurrencyPickedUp;
    }

    private void OnDisable()
    {
        _monstersHandler.CurrencyPickedUp -= OnCurrencyPickedUp;
    }

    public void SetMuliplier(float multiplier)
    {
        _multiplier = multiplier;
    }

    public void RaiseMight(int level)
    {
        _monstersHandler.LevelUpAllMonster(level);
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void Die(LostCouse lostCouse)
    {
        _deathHandler.Die(lostCouse);
    }

    public void KillAllMonsters(LostCouse lostCouse)
    {
        _monstersHandler.KillAllMonsters(lostCouse);
    }

    public void OnMonsterDie(LostCouse lostCouse)
    {
        _counter++;

        if (_counter >= _monstersHandler.MonsterCounter)
            Die(lostCouse);
    }

    private void OnCurrencyPickedUp(int amount)
    {
        float mulipliedAmount = amount * _multiplier;
        _currencyHandler.Increase(mulipliedAmount);
    }
}
