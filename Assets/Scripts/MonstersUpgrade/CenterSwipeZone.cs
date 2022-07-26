using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CenterSwipeZone : MonoBehaviour
{
    [SerializeField] private SwipeZone _swipeZone;

    private float _speedDivider = 2;

    public SwipeZone SwipeZone => _swipeZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SwipeMover swipeMover))
        {
            if(SwipeZone.Interacting == false)
                _swipeZone.SlowDown(_speedDivider, swipeMover);
        }

        if(other.TryGetComponent(out MonsterCell monsterCell))
        {
            monsterCell.SetCentral(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MonsterCell monsterCell))
        {
            monsterCell.SetCentral(false);
        }
    }
}
