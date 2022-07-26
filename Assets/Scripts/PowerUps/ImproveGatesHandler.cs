using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ImproveGatesHandler : MonoBehaviour
{
    private MonsterList _monstersList;

    private Gate[] _gates;

    private void OnEnable()
    {
        _gates = GetComponentsInChildren<Gate>();

        foreach (var gate in _gates)
        {
            gate.GateActivated += DisableGates;
            gate.NeedAnotherMonster += ReplaceMonster;
        }
    }

    private void OnDisable()
    {
        foreach (var gate in _gates)
        {
            gate.GateActivated -= DisableGates;
            gate.NeedAnotherMonster -= ReplaceMonster;
        }
    }

    public void Init(MonsterList monsterList)
    {
        _monstersList = monsterList;
    }

    public void ResetMonster(Monster monster)
    {
        foreach (var gate in _gates)
        {
            if (gate.Monster.GetType() == monster.GetType())
            {
                gate.ResetMonster();
            }
        }

        _monstersList.AddMonster(monster);
    }

    public void DisableGates()
    {
        foreach (var gate in _gates)
        {
            gate.Disable();
            gate.GateActivated -= DisableGates;
        }
    }

    public void PlaceMonster()
    {
        Monster previousMonster = null;
        _monstersList.TryGetRandomMonster(out Monster monster);
        Monster newMonster = monster;

        foreach (var gate in _gates)
        {
            while (newMonster == previousMonster)
            {
                _monstersList.TryGetRandomMonster(out Monster tempMonster);
                newMonster = tempMonster;
            }

            gate.SetMonster(newMonster);
            previousMonster = newMonster;
        }
    }

    private void ReplaceMonster(Monster monsterToRemove, Gate gate)
    {
        _monstersList.RemoveMonster(monsterToRemove);

        List<Monster> monsters = new List<Monster>();

        foreach (var tempGate in _gates)
        {
            monsters.Add(tempGate.Monster);
        }

        Monster monsterToBeExcepted = monsters.FirstOrDefault(tempMonster => tempMonster.GetType() != monsterToRemove.GetType());

        if (_monstersList.TryGetRandomMonsterExcept(monsterToBeExcepted, out Monster newMonster))
            gate.ReplaceMonster(newMonster);
        else
            gate.PlacePowerUp(_monstersList.GetPowerUp());
    }
}
