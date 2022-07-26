using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour, IAttackBehavior
{
    public virtual void Fight(Monster self, Monster monster) { }
}
