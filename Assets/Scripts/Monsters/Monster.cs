using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RunnerMovementSystem;

[RequireComponent(typeof(MonsterAnimator), typeof(MonsterDeathHandler), typeof(Rigidbody))]
[RequireComponent(typeof(Attack))]
public class Monster : MonoBehaviour, IMergeable
{
    [SerializeField] private Health _health = new Health();
    [SerializeField] private float _damagePerLevel;
    [SerializeField] private float _speed;
    [SerializeField] private int _level;
    [SerializeField] private float _damageScaler;
    [SerializeField] private Transform _pointForProjectile;
    [HideInInspector] public bool Protected;
    [HideInInspector] public bool LvlLoaded;

    public string Name;
    private int _maxLevel = 80;
    private float _damage;
    private ResizeAnimation _resizeAnimation;
    private Attack _attack;
    private StatScaler _statScaler = new StatScaler();

    public int FormCounter { get; private set; }
    public MovementSystem MovementSystem { get; private set; }
    public MonsterAnimator MonsterAnimator { get; private set; }
    public Monster Target { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public FormsHandler FormsHandler { get; private set; }
    public MonsterDeathHandler MonsterDeathHandler { get; private set; }
    public bool IsAllive { get; private set; }
    public Attack Attack => _attack;
    public Health Health => _health;
    public float Speed => _speed;
    public int Level => _level;
    public float Damage => _damage;
    public float DamagePerLevel => _damagePerLevel;
    public Transform PointForProjectile => _pointForProjectile;

    public event Action<int> LevelChanged;
    public event Action<float> Damaged;
    public event Action DealtDamage;
    public event Action Died;
    public event Action<int> HittedAnObstacle;

    private void Awake()
    {
        IsAllive = true;
        _attack = GetComponent<Attack>();

        Rigidbody = GetComponent<Rigidbody>();

        MonsterAnimator = GetComponent<MonsterAnimator>();
        _resizeAnimation = GetComponentInChildren<ResizeAnimation>();
        FormsHandler = GetComponentInChildren<FormsHandler>();
        MonsterDeathHandler = GetComponent<MonsterDeathHandler>();

        _resizeAnimation.SetMaxStep(_maxLevel);

        _damage = _statScaler.ScaleByLevel(_level, _damageScaler, _damagePerLevel);

        _health.Init(_level);
    }

    public void SetTarget(Monster monster)
    {
        Target = monster;
    }

    public void ApplyDamage(float damage)
    {
        if (Protected)
            return;

        Health.Decrease(damage);

        Damaged?.Invoke(damage);

        FormsHandler.CurrentForm.OnDamaged();

        if (Health.CurrentHealth <= 0)
            Die(LostCouse.Battle);
    }

    public void DealDamage()
    {
        DealtDamage?.Invoke();

        if (Target == null || Target.IsAllive == false)
            return;

        _attack.Perform(Target, _damage);
    }

    public void Die(LostCouse lostCouse)
    {
        if (IsAllive)
        {
            MonsterDeathHandler.Die(lostCouse);
            IsAllive = false;
            Died?.Invoke();
        }
    }

    public bool TryMerge(int level)
    {
        if (FormsHandler.TryEnableNextForm())
        {
            LevelUp(level, true);
            FormCounter++;

            return true;
        }

        return false;
    }

    public void LevelUp(int level, bool evo = false)
    {
        _level += level;
        _level = Mathf.Clamp(_level, 0, _maxLevel);
        _resizeAnimation?.PlayEnlargeAnimation(_level, evo);
        _damage = _statScaler.ScaleByLevel(_level, _damageScaler, _damagePerLevel);
        _health.IncreaseMaxHealth(_level);
        LevelChanged?.Invoke(_level);
    }

    public void ObstacleHitted(int level)
    {
        HittedAnObstacle?.Invoke(level);
        LevelDown(level);
    }

    public void LevelDown(int level)
    {
        _level -= level;
        _level = Mathf.Clamp(_level, 1, _maxLevel);

        LevelChanged?.Invoke(_level);
    }

    public void UnMerge(int level)
    {
        LevelDown(level);
        FormsHandler.EnablePreviousForm();
        LevelChanged?.Invoke(_level);
    }
}
