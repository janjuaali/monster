using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/RotatorOptions")]
public class RotatorOptions : ScriptableObject
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _resetSpeed;
    [SerializeField] private float _maxAngle;

    public float RotationSpeed => _rotationSpeed;
    public float ResetSpeed => _resetSpeed;
    public float MaxAngle => _maxAngle;
}
