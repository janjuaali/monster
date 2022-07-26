using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAll : AbilitiyButtonView
{
    [SerializeField] private ParticleSystem _particleSystemPrefab;

    private IEnumerable<Monster> _monsters;

    public override void Cast()
    {
        Debug.Log(Time.time);

        if(_monsters == null)
            _monsters = FindObjectOfType<MonstersHandler>().GetAllMonsters();

        foreach (var monster in _monsters)
        {
            monster.Health.Increase(ValueHandler.Value);
            Vector3 position = monster.transform.position + new Vector3(0, 1, 0);
            PlayParticle(position);
        }
    }

    private void PlayParticle(Vector3 position)
    {
        Instantiate(_particleSystemPrefab, position, _particleSystemPrefab.transform.rotation);
    }
}
