using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour 
{
    [SerializeField] private AttackRange _attackRange;
    [SerializeField] private AttackRange _initialAttackRange;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private bool _DontSwitchRangeOnStart;

    private AttackRange _inititalRange;
    public AttackRange InitialRange => _initialAttackRange;
    public AttackRange AttackRange => _attackRange;

    private void Awake()
    {
        InitRunerSetting();
    }

    public void InitRunerSetting()
    {
        if (_DontSwitchRangeOnStart)
            return;

        _attackRange = AttackRange.Melee;
    }

    public void Perform(Monster target, float damage)
    {
        if(_attackRange == AttackRange.Melee)
            target.ApplyDamage(damage);

        if (_attackRange == AttackRange.Range)
        {
            var projectile = Instantiate(_projectile, _attackPoint.position, Quaternion.identity);
            projectile.Init(target, damage);
        }    
    }

    public void PerformMeleeAttack(Monster target, float damage)
    {
        target.ApplyDamage(damage);
    }

    public void SwitchRange()
    {
        if (_inititalRange == AttackRange.Melee)
            _attackRange = AttackRange.Range;
        else
            _attackRange = AttackRange.Melee;
    }

    public void SetInitialRange()
    {
        _attackRange = InitialRange;
    }
}

public enum AttackRange
{
    Melee,
    Range
}
