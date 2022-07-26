using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterShopButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CameraTransition _firstClickTransition;
    [SerializeField] private CameraTransition _secondClickTransition;

    [SerializeField] private GameObject _startLevel;

    private bool _switch;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_switch)
        {
            if(_firstClickTransition.TryTransit())
                _switch = !_switch;
        }
        else
        {
            if(_secondClickTransition.TryTransit())
                _switch = !_switch;
        }

        _startLevel.SetActive(!_switch);
    }
}
