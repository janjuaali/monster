using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Interactable
{
    [SerializeField] private int _amount;
    [SerializeField] private ParticleSystem _particleSystem;

    public override void Use(MonsterPlace monsterPlace)
    {
        monsterPlace.PickUpCurrency(_amount);
        var particles = Instantiate(_particleSystem, transform);
        particles.transform.parent = null;
        Destroy(gameObject);
    }
}
