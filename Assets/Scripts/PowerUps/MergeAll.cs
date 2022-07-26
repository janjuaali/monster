using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MergeAll : Interactable
{
    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelUpAllMonster(1);
        gameObject.SetActive(false);
    }
}
