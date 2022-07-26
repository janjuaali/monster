using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBehavior
{
    public override void Act(Monster self, Monster monster) { }
    public override void OnMonsterOutRange() => throw new System.NotImplementedException();
}
