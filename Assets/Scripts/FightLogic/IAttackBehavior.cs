using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBehavior
{
    public void Fight(Monster self, Monster monster);
}
