using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Interactable
{
    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelDownAllMonster(10);
    }

    public override void Use(Monster monster)
    {
        monster.MonsterAnimator.HitAnimation();
    }
}
