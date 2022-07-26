using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _particlePoint;

    private void Awake()
    {
        _particleSystem = Instantiate(_particleSystem, _particlePoint, false);
    }

    private void OnEnable()
    {
        _monster.DealtDamage += OnDamaged;
    }

    private void OnDisable()
    {
        _monster.DealtDamage -= OnDamaged;
    }

    private void OnDamaged()
    {
        _particleSystem.Play();
    }
}
