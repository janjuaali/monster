using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/EnlargeOptions")]
public class EnlargeOptions : ScriptableObject
{
    [SerializeField] private float _time;
    [SerializeField] private float _scaleCoefficient;
    [SerializeField] private float _maxScale;

    public float Time => _time;
    public float ScaleCoefficient => _scaleCoefficient;
    public float MaxScale => _maxScale;
}
