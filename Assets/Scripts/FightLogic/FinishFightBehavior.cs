using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFightBehavior : AttackBehavior
{
    private Coroutine _coroutine;
    private const float _delay = 0.5f;

    public override void Fight(Monster self, Monster monster)
    {
        self.SetTarget(monster);

        Vector3 lookDirection = (monster.transform.position - self.transform.position).normalized;

        self.transform.rotation = Quaternion.LookRotation(lookDirection, self.transform.up);

        if(_coroutine == null)
            _coroutine = StartCoroutine(AttackDelay(self));
    }

    private IEnumerator AttackDelay(Monster self)
    {
        self.MonsterAnimator.TriggerAttackAnimation();

        yield return new WaitForSeconds(_delay);

        _coroutine = null;
    }
}
