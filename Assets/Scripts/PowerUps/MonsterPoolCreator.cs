using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterPoolCreator : MonoBehaviour
{
    [SerializeField] private MonsterList _monsterList;

    private ImproveGatesHandler[] _improveGatesHandlers;
    private AdditionalGate[] _additionalGates;

    private int _firstAdditionalGateIndex = 4;
    private int _secondAdditionalGateIndex = 5;
    private void Awake()
    {
        _improveGatesHandlers = FindObjectsOfType<ImproveGatesHandler>();
        _additionalGates = FindObjectsOfType<AdditionalGate>();

        //_monsterList.CreateMonsterPool();

        //foreach (var improveGatesHandler in _improveGatesHandlers)
        //{
        //    improveGatesHandler.Init(_monsterList);
        //    improveGatesHandler.PlaceMonster();
        //}
    }

    public void Init()
    {
        var playerMonsters = FindObjectOfType<MonstersHandler>().GetAllMonsters();
        MonsterShop monsterShop = FindObjectOfType<MonsterShop>();

        _monsterList.SetMonsterPool(playerMonsters);

        if (monsterShop.TryGetMonstersForPool(out List<Monster> monsters))
            _monsterList.TryAddToMonsterPool(monsters);

        if (monsterShop.OpenedMonsterPlaces < _firstAdditionalGateIndex)
            _additionalGates[0].gameObject.SetActive(false);
        if(monsterShop.OpenedMonsterPlaces < _secondAdditionalGateIndex)
            _additionalGates[1].gameObject.SetActive(false);

        foreach (var improveGatesHandler in _improveGatesHandlers)
        {
            improveGatesHandler.Init(_monsterList);
            improveGatesHandler.PlaceMonster();
        }
    }

    public void ResetMonster(Monster monster)
    {
        foreach (var improveGatesHandler in _improveGatesHandlers)
        {
            improveGatesHandler.ResetMonster(monster);
        }
    }
}
