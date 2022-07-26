using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectAll : AbilitiyButtonView
{
    [SerializeField] private ParticleSystem _particleSystem;

    private IEnumerable<Monster> _monsters;
    private List<ParticleSystem> _tempParticles = new List<ParticleSystem>();
    public override void Cast()
    {
        if(_monsters == null)
            _monsters = FindObjectOfType<MonstersHandler>().GetAllMonsters();

        Debug.Log(ValueHandler.Value);
        StartCoroutine(ProtectionTime(ValueHandler.Value));
    }

    private IEnumerator ProtectionTime(float time)
    {
        Debug.Log(Time.time);
        foreach (var monster in _monsters)
        {
            var particle = Instantiate(_particleSystem, monster.transform);
            particle.transform.localPosition += new Vector3(0, 1, 0);
            _tempParticles.Add(particle);

            monster.Protected = true;
        }

        yield return new WaitForSeconds(time - _particleSystem.main.duration);

        foreach (var tempParticle in _tempParticles)
        {
            tempParticle.Stop();
        }

        Debug.Log(Time.time);

        foreach (var monster in _monsters)
        {
            monster.Protected = false;
        }


        _tempParticles.RemoveRange(0,_tempParticles.Count);
    }
}
