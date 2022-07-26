using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState: StateBehavior
{
    public override void Act(Monster self, Monster monster)
    {
        Vector3 direction = (monster.transform.position - self.transform.position).normalized;

        Rotate(self.transform, direction);

        self.transform.position += (self.transform.forward * self.Speed * Time.deltaTime);

    }

    public override void OnMonsterOutRange() => throw new NotImplementedException();

    private void Rotate(Transform self ,Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        self.rotation = lookRotation;
    }
}
