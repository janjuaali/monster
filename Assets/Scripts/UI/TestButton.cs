using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Monster _monster;
    [SerializeField] private MonstersHandler _monstersHandler;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_monstersHandler.TrySetMonsterToPlace(_monster,1))
            Debug.Log("bonjour");
    }
}
