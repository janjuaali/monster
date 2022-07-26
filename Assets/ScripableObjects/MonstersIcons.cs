using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/MonstersIcon")]
public class MonstersIcons : ScriptableObject
{
    [SerializeField] private Sprite _rangeIcon;
    [SerializeField] private Sprite _meleeIcon;

    public Sprite GetAttackRangeIconSprite(AttackRange attackRange)
    {
        if (attackRange == AttackRange.Range)
            return _rangeIcon;

        return _meleeIcon;
    }
}
