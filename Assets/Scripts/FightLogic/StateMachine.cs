using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]
public class StateMachine: MonoBehaviour
{
    [SerializeField] private float _agroRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private AttackBehavior _attackBehavior;
    [SerializeField] private LayerMask _monsterLayerMask;

    private Monster _self;
    private Monster _target;
    private StateBehavior _currentBehavior;
    private StateBehavior _previousBehavior;
    private MoveState _moveState;
    private AttackState _attackState;
    private IdleState _idleState;

    public event Action<StateBehavior> StateChanged;

    private void Awake()
    {
        _moveState = new MoveState();
        _attackState = new AttackState(_attackBehavior);
        _idleState = new IdleState();
        _currentBehavior = _idleState;

        _self = GetComponent<Monster>();
    }

    private void Update()
    {
        if(TryFindTarget(_agroRadius))
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < _attackRadius)
            {
                SetBehavior(_attackState);

                if(_target.IsAllive)
                    _currentBehavior.Act(_self, _target);

                return;
            }

            SetBehavior(_moveState);
        }
        else
        {
            SetBehavior(_idleState);
        }

        _currentBehavior.Act(_self, _target);
    }

    public void SetBehavior(StateBehavior stateBehavior)
    {
        _currentBehavior = stateBehavior;

        if(_currentBehavior != _previousBehavior)
        {
            StateChanged?.Invoke(_currentBehavior);
            _previousBehavior = stateBehavior;
        }
    }

    private bool TryFindTarget(float range)
    {
        if (_target != null && _target.IsAllive)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) >= _agroRadius)
                return false;

            return true;
        }

        Collider[] monstersColliders = Physics.OverlapSphere(transform.position, range, _monsterLayerMask);

        _target = GetClosestTargetCollider(monstersColliders);

        return _target != null;
    }

    private Monster GetClosestTargetCollider(Collider[] monstersColliders)
    {
        Monster target = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var monsterCollider in monstersColliders)
        {
            if (monsterCollider.TryGetComponent(out Monster tempEnemy) && tempEnemy != _self && tempEnemy.IsAllive)
            {
                float dist = Vector3.Distance(tempEnemy.transform.position, currentPosition);

                if (dist < minDistance)
                {
                    target = tempEnemy;
                    minDistance = dist;
                }
            }
        }

        return target;
    }
}
