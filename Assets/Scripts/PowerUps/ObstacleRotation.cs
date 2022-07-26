using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleRotation : MonoBehaviour
{
    [SerializeField] private float _ySpeed;
    [SerializeField] private float _xSpeed;
    [SerializeField] private float _zSpeed;

    private void Update()
    {
        transform.Rotate(_xSpeed * Time.deltaTime, _ySpeed * Time.deltaTime, _zSpeed *Time.deltaTime);
    }
}
