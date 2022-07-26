using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextCreator : MonoBehaviour
{
    [SerializeField] private FloatingText _floatingText;
    [SerializeField] private Monster _monster;
    [SerializeField] private Transform _point;

    private void OnEnable()
    {
        _monster.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _monster.Damaged -= OnDamaged;
    }
    private void OnDamaged(float damage)
    {
        var text = Instantiate(_floatingText, _point);
        text.Init((int)-damage);
    }
}
