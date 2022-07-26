using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunnerEnemyLevelPresenter : LevelPresenter
{
    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
        Show(_enemy.Level);
    }
}
