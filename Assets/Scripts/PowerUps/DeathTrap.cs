using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrap : Interactable
{
    [SerializeField] private ParticleSystem _particleSystem;

    public override void Use(Monster monster)
    {
        var monstersHandler = FindObjectOfType<MonstersHandler>();

        int level = monstersHandler.MonsterMight / monstersHandler.MonsterCounter;
        monstersHandler.LevelDownAllMonster(level);

        monster.Die(LostCouse.Obstacle);
        monster.transform.parent = null;

        Instantiate(_particleSystem, monster.PointForProjectile.position, Quaternion.identity);
    }
}
