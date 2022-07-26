using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(MonsterHandlerColliders))]
public class MonstersHandler : MonoBehaviour
{
    [SerializeField] private MonstersAnimatorHandler _monstersAnimatorHandler;

    private MonsterHandlerColliders _monsterHandlerColliders;
    private MonsterPlace[] _monsterPlaces;
    private const int AddLevelOnMerge = 10;
    private int _monstersMight;
    private int _counter = 0;
    public int MonsterCounter { get; private set; }
    public int MonsterMight => _monstersMight;

    public event Action<MonsterAnimator> MonsterAdded;
    public event Action<Monster> MonsterMerged;
    public event Action<int, int> MightChanged;
    public event Action<int> CurrencyPickedUp;

    private void Awake()
    {
        _monsterHandlerColliders = GetComponent<MonsterHandlerColliders>();

        _monsterPlaces = GetComponentsInChildren<MonsterPlace>();
        Error.CheckOnNull(_monsterPlaces[0], nameof(MonsterPlace));

        for (int i = 0; i < _monsterPlaces.Length; i++)
        {
            _monsterPlaces[i].Position = i;
        }
    }

    private void OnEnable()
    {
        foreach (var monsterPlace in _monsterPlaces)
        {
            monsterPlace.PlaceFree += SwapMonsterPlaces;
            monsterPlace.CurrencyPickedUp += PickUpCurrency;
        }
    }

    private void OnDisable()
    {
        foreach (var monsterPlace in _monsterPlaces)
        {
            monsterPlace.PlaceFree -= SwapMonsterPlaces;
            monsterPlace.CurrencyPickedUp -= PickUpCurrency;
        }
    }

    public void KillAllMonsters(LostCouse lostCouse)
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.Die(lostCouse);
        }
    }

    public bool TrySetMonsterToPlace(Monster monster, int level)
    {
        MonsterPlace place = _monsterPlaces.FirstOrDefault(place => place.IsTaken == false);
        MonsterPlace placeWithMonster = _monsterPlaces.FirstOrDefault(placeWithMonster => placeWithMonster.Monster != null && placeWithMonster.Monster.GetType() == monster.GetType());

        if (place != default || placeWithMonster != default)
        {
            if (CanMerge(monster, placeWithMonster))
            {

                Debug.Log("hi");
                ChangeMonstersMight(AddLevelOnMerge);

                MonsterMerged?.Invoke(placeWithMonster.Monster);

                return placeWithMonster.Monster.TryMerge(AddLevelOnMerge);
            }

            SetMonsterToPlace(monster, place, level);
            _monsterHandlerColliders.CreateBoxCollider(place);
            MonsterCounter++;

            return true;
        }

        return false;
    }

    public void TrySetMonsterToPlace(Monster monster, MonsterPlace place, int level, bool isNeededTeleportation = true)
    {
        _monsterHandlerColliders.CreateBoxCollider(place);

        if (isNeededTeleportation)
        {
            monster.transform.parent = null;
            monster.transform.SetParent(place.transform);
            monster.transform.localRotation = new Quaternion(0,1,0,0);
            monster.transform.localPosition = Vector3.zero;
            monster.transform.localScale = Vector3.one;
        }

        place.Take(monster, true);

        _monstersAnimatorHandler.AddAnimator(monster.MonsterAnimator);
        ChangeMonstersMight(level);

        MonsterCounter++;
    }

    public void DecreasseCounter(int level)
    {
        MonsterCounter--;
        ChangeMonstersMight(-level);
    }

    public void LevelUpAllMonster(int level)
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.LevelUp(level);
        }

        ChangeMonstersMight(level);
    }

    public void LevelDownAllMonster(int level)
    {
        var monsters = GetAllMonsters();

        foreach (var monster in monsters)
        {
            monster.LevelDown(level);
        }

        ChangeMonstersMight(-level);
    }

    public int GetMonsterForm(Monster monster)
    {
        var tempMonster = GetMonster(monster);

        if (tempMonster != null)
            return tempMonster.FormCounter;

        return 0;
    }

    public void PickUpCurrency(int amount)
    {
        CurrencyPickedUp?.Invoke(amount);
    }

    public IEnumerable<Monster> GetAllMonsters()
    {
        var monsters = from MonsterPlace monsterPlace in _monsterPlaces
                       where monsterPlace.Monster != null
                       select monsterPlace.Monster;

        return monsters;
    }

    public void TurnAllMonsterForward()
    {
        foreach (var monsterPlace in _monsterPlaces)
        {
            monsterPlace.TurnMonsterForward();
        }
    }

    private void SwapMonsterPlaces(MonsterPlace monsterPlace)
    {
        _monsterHandlerColliders.DisableCollider(monsterPlace);
        MonsterPlace currentPlace = _monsterPlaces.FirstOrDefault(place => place.Monster != null && place.Position > monsterPlace.Position);

        if (currentPlace != default)
        {
            _monsterHandlerColliders.DisableCollider(currentPlace);
            _monsterHandlerColliders.EnableCollider(monsterPlace);

            currentPlace.Monster.transform.SetParent(monsterPlace.transform, true);
            monsterPlace.Take(currentPlace.Monster);
            StartCoroutine(MoveTo(currentPlace.Monster, currentPlace));
            currentPlace.Free();
        }

    }

    private IEnumerator MoveTo(Monster monster, MonsterPlace monsterPlace)
    {
        float distance = Vector3.Distance(monster.transform.localPosition, Vector3.zero);
        float speed = distance / 0.5f;

        yield return new WaitForSeconds(0.5f);

        while (monster.transform.localPosition != Vector3.zero && monster.IsAllive)
        {
            monster.transform.localPosition = Vector3.MoveTowards(monster.transform.localPosition, Vector3.zero, speed * Time.deltaTime);

            yield return null;
        }
    }

    public void ChangeMonstersMight(int level)
    {
        _monstersMight+= level;

        if (_monstersMight <= 0)
            _monstersMight = 0;

        MightChanged?.Invoke(_monstersMight, level);
    }

    private void SetMonsterToPlace(Monster monsterType, MonsterPlace monsterPlace, int level)
    {
        var monster = Instantiate(monsterType, monsterPlace.transform);
        monster.transform.localPosition = Vector3.zero;
        monsterPlace.Take(monster);

        ChangeMonstersMight(level);
        _monstersAnimatorHandler.AddAnimator(monster.MonsterAnimator);
    }

    private bool CanMerge(Monster monster, MonsterPlace place)
    {       
        return place != default && place.Monster != null && place.Monster.GetType() == monster.GetType();
    }

    private Monster GetMonster(Monster monster)
    {
        var existMonster = _monsterPlaces.FirstOrDefault(monsterPlace => monsterPlace.Monster != null && monsterPlace.Monster.GetType() == monster.GetType());

        if (existMonster != default)
            return existMonster.Monster;

        return null;
    }
}
