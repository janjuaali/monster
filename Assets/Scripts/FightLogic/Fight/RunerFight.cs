using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunerFight: AttackBehavior
{
    private Monster _monsterOfPlayer;
    private Player _player;
    private Monster _enemyMonster;

    private bool _isDead;
    public override void Fight(Monster enemyMonster, Monster monsterOfPlayer)
    {
        _enemyMonster = enemyMonster;
        _player = monsterOfPlayer.transform.root.GetComponent<Player>();
        MonstersAnimatorHandler playerMonsterAnimatorHandler = monsterOfPlayer.transform.root.GetComponent<MonstersAnimatorHandler>();
        _monsterOfPlayer = monsterOfPlayer;

        bool isPlayerWin = _player.Might >= enemyMonster.Level;

        if (_isDead)
            return;

        enemyMonster.MonsterAnimator.TriggerAttackAnimation();
        monsterOfPlayer.MonsterAnimator.TriggerAttackAnimation();

        if (isPlayerWin)
        {
            monsterOfPlayer.SetTarget(enemyMonster);
            monsterOfPlayer.DealtDamage += Push;
            _monsterOfPlayer.DealDamage();
            _enemyMonster.Die(LostCouse.RunerEnemy);
        }
        else
        {
            _isDead = true;
            enemyMonster.DealtDamage += Die;
            enemyMonster.SetTarget(monsterOfPlayer);
        }
    }

    private void Push()
    {
        _player.RaiseMight(_enemyMonster.Level);
        _monsterOfPlayer.DealtDamage -= Push;
    }

    private void Die()
    {
        
        _player.Die(LostCouse.RunerEnemy);
        StartCoroutine(PushBackAnimation());
        _player.KillAllMonsters(LostCouse.RunerEnemy);
        _enemyMonster.DealtDamage -= Die;
    }

    private IEnumerator PushBackAnimation()
    {
        float elapsedTime = 0;

        Debug.Log(transform.name);

        while(elapsedTime < 0.2f)
        {
            elapsedTime += Time.deltaTime;

            _player.transform.position += -_player.transform.forward * 10f * Time.deltaTime;

            yield return null;
        }
    }
}
