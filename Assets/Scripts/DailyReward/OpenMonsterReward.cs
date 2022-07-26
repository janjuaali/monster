using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMonsterReward : DailyRewardBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private CurrencyReward _currencyReward;
    [SerializeField] private GameObject _congrats;

    public override void Acquire()
    {
        if (FindObjectOfType<MonsterShop>().TryOpenCellWithMonster(_monster) == false)
            _currencyReward.Acquire();
        else
            _congrats.SetActive(true);
    }
}
