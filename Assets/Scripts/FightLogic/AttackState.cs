using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBehavior
{
    private IAttackBehavior _attackBehavior;

    public AttackState(AttackBehavior attackBehavior)
    {
        _attackBehavior = attackBehavior;
    }

    public override void Act(Monster enemyMonster, Monster monsterOfPlayer)
    {
        _attackBehavior.Fight(enemyMonster, monsterOfPlayer );
    }

    public override void OnMonsterOutRange() => throw new System.NotImplementedException();
}
