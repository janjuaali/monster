using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MonsterPlace))]
public class MonsterPlaceAccepter : MonoBehaviour, IMonsterHolder
{
    [SerializeField] private MonstersHandler _monstersHandler;
    [SerializeField] private GameObject _freeEffect;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private MonsterInfoPanel _monsterInfoPanel;
    [SerializeField] private ParticleSystem _acquireParticle;

    private MonsterPlace _monsterPlace;
    private Monster _monster;

    public Rotator _rotator { get; private set; }
    public Monster Monster => _monster;
    public bool IsFree => _monster == null;
    public bool CanAcquireMonster => IsFree && _boxCollider.enabled;
    public MonsterInfoPanel MonsterInfoPanel => _monsterInfoPanel;
    public bool IsOpened { get; private set; }

    public event Action Changed;

    public bool TryAcquireMonster(Monster monster)
    {
        bool isPlaceFree = _monsterPlace.Monster == null && IsOpened;

        if (isPlaceFree)
        {
            _monster = monster;
            _monstersHandler.TrySetMonsterToPlace(_monster, _monsterPlace, _monster.Level);


            monster.transform.localScale = Vector3.one;
            _rotator = monster.GetComponent<Rotator>();
            DisableRotator();
            Changed?.Invoke();

            Instantiate(_acquireParticle, transform);

            _monsterInfoPanel?.Open(monster);
        }

        return isPlaceFree;
    }

    public bool TryGrab(out Monster monster, bool instaShrink = false)
    {
        bool _isMonsterSetted = _monster != null;
        monster = null;

        if (_isMonsterSetted)
        {
            _monsterPlace.Free(true);
            _monstersHandler.DecreasseCounter(_monster.Level);
            monster = _monster;
            LightUp();
            _monster = null;

            _monsterInfoPanel?.Close();
            Changed?.Invoke();
        }

        return _isMonsterSetted;
    }

    public void EnableRotator()
    {
        if(_rotator != null)
            _rotator.enabled = true;
    }

    public void Open()
    {
        _monsterPlace.EnableCollider();
        IsOpened = true;

        LightUp();

        Changed?.Invoke();
    }

    public void Hide()
    {
        _monsterPlace.DeActivate();
    }

    public void Activate()
    {
        if (_monsterPlace == null)
            _monsterPlace = GetComponent<MonsterPlace>();

        gameObject.SetActive(true);
    }

    public void LightUp()
    {
        _freeEffect.SetActive(true);
    }

    private void DisableRotator()
    {
        _rotator = _monster.GetComponentInChildren<Rotator>();
        _rotator.enabled = false;
    }

    public void LightDown()
    {
        _freeEffect.SetActive(false);
    }

    public bool TryReturnMonster()
    {
        if (_monster == null)
            return false;

        MonsterCell[] monsterCells = FindObjectsOfType<MonsterCell>();

        var place = monsterCells.FirstOrDefault(place => place.Monster.GetType() == _monster.GetType());

        if (place == default)
            return false;

        if (TryGrab(out Monster monster))
        {
            if (place.IsOpened)
            {
                monster.GetComponent<Mover>().MoveTo(monster, place, -transform.forward * 10f);

                return true;
            }
        }

        return false;
    }
}
