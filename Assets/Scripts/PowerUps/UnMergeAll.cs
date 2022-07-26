using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class UnMergeAll : Interactable
{
    public override void Use(MonstersHandler monstersHandler)
    {
        monstersHandler.LevelDownAllMonster(1);
        gameObject.SetActive(false);
    }
}
