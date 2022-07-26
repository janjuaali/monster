using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Chest))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private MoveState _runerEnemyMover;
    [SerializeField] private int _level;
    [SerializeField] private RunerFight _runerFight;

    private Chest _chest;
    private bool _isFightOver;

    public int Level => _level;

    private void Awake()
    {
        _chest = GetComponent<Chest>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.TryGetComponent(out Player player) && _isFightOver == false)
        //{
        //    _runerFight.Fight(this, player, out bool IsPlayerWin);

        //    if (IsPlayerWin)
        //    {
        //        player.RaiseMight(_level);
        //        _runerEnemyMover.Disable();
        //        _chest.Push(-transform.forward, 50f);
        //    }
        //    else
        //    {
        //        _runerEnemyMover.Disable();
        //        player.Die();
        //    }

        //    _isFightOver = true;
        //}
    }
}
