using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _protection;
    [SerializeField] private ParticleSystem _heal;

    public void SetProtection(float time)
    {
        StartCoroutine(Timer(_protection, time));
    }

    public void Heal()
    {
        _heal.Play();
    }

    private IEnumerator Timer(ParticleSystem particleSystem, float time)
    {
        particleSystem.Play();
        float elapsedTime = time;

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;

            yield return null;
        }

        particleSystem.Stop();
    }
}
