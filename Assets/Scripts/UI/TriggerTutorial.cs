using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : Interactable
{
    [SerializeField] private GameObject[] _tutorials;
    [SerializeField] private bool _open;
    [SerializeField] private ShopTutorial _shopTutorial;
    [SerializeField] private float _delay;

    public override void Use(MonstersHandler monstersHandler)
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        foreach (var tutrial in _tutorials)
        {
            tutrial.SetActive(_open);
        }

        if (_shopTutorial != null)
            _shopTutorial.FreezeGame();
    }
}
