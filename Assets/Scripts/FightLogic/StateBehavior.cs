using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBehavior
{
    public abstract void Act(Monster self, Monster monster);
    public abstract void OnMonsterOutRange();
}
