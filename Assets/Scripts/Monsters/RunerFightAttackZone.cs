using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunerFightAttackZone : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private RunerFight _runerFight;

    private bool _isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered)
            return;

        if(other.TryGetComponent(out Monster playerMonster))
        {
            _runerFight.Fight(_monster, playerMonster);
            _isTriggered = true;
        }
    }
}
