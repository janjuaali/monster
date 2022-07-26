using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/MonsterList")]
public class MonsterList : ScriptableObject
{
    [SerializeField] private List<Monster> _monsters;
    [SerializeField] private Interactable _powerUp;

    public IReadOnlyList<Monster> Monsters => _monsters;

    private List<Monster> _monsterPool = new List<Monster>();

    public void CreateMonsterPool()
    {
        if(_monsterPool.Count>0)
            _monsterPool.RemoveRange(0, _monsterPool.Count);

        foreach (var monster in _monsters)
        {
            _monsterPool.Add(monster);
        }
    }

    public void SetMonsterPool(IEnumerable<Monster> monsters)
    {
        if (_monsterPool.Count > 0)
            _monsterPool.RemoveRange(0, _monsterPool.Count);

        foreach (var monster in monsters)
        {
            _monsterPool.Add(monster);
        }
    }

    public bool TryAddToMonsterPool(List<Monster> monsters)
    {
        if (_monsterPool == null)
            return false;

        _monsterPool.AddRange(monsters);

        return true;
    }

    public Monster GetRandomMonster()
    {
        int index = UnityEngine.Random.Range(0, _monsters.Count);

        return _monsters[index];
    }

    public bool TryGetRandomMonster(out Monster monster)
    {
        if (_monsterPool.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, _monsterPool.Count);
            monster = _monsterPool[index];

            return true;
        }
        else
        {
            monster = null;
            return false;
        }
    }

    public Interactable GetPowerUp()
    {
        return _powerUp;
    }

    public bool TryGetRandomMonsterExcept(Monster monster, out Monster newMonster)
    {
        newMonster = _monsterPool.FirstOrDefault(tempMonster => tempMonster.GetType() != monster.GetType());

        return newMonster != default;
    }

    public void RemoveMonster(Monster monster)
    {
        var monsterToRemove = _monsterPool.FirstOrDefault(currentMonster => currentMonster.GetType() == monster.GetType());

        _monsterPool.Remove(monsterToRemove);
    }

    public void AddMonster(Monster monster)
    {
        _monsterPool.Add(monster);
    }

    public bool TryGetMonsterId(Monster monster, out int index)
    {
        index = 0;

        for (int i = 0; i < _monsters.Count; i++)
        {
            if (monster.GetType() == _monsters[i].GetType())
            {
                index = i;
                return true;
            }
        }

        return false;
    }
}
