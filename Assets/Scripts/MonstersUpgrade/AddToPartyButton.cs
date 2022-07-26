using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class AddToPartyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _noPlaceForMonster;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _leavePartyButton;

    private MonsterPlaceAccepter[] _monsterPlaceAccepters;
    private MonsterCell _monsterCell;

    private bool _hideButton;

    private void OnEnable()
    {
        _monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            monsterPlaceAccepter.Changed += UpdateView;
        }
    }

    private void OnDisable()
    {
        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            monsterPlaceAccepter.Changed -= UpdateView;
        }
    }

    public void Init(MonsterCell monsterCell)
    {
        _monsterCell = monsterCell;
        _monsterPlaceAccepters = FindObjectOfType<MonstersHandler>().GetComponentsInChildren<MonsterPlaceAccepter>();
        Error.CheckOnNull(_monsterPlaceAccepters, nameof(MonstersHandler));

        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            monsterPlaceAccepter.Changed += UpdateView;
        }

        UpdateView();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_hideButton)
            return;

        _monsterCell.TryPlaceMonster();
    }

    public void UpdateView()
    {
        _hideButton = true;

        foreach (var monsterPlaceAccepter in _monsterPlaceAccepters)
        {
            if (_monsterCell.IsMonsterUsed == false && monsterPlaceAccepter.CanAcquireMonster)
                _hideButton = false;
        }


        _text.gameObject.SetActive(!_hideButton);
        _noPlaceForMonster.gameObject.SetActive(_hideButton);
    }
}
