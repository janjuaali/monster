using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitating : MonoBehaviour
{
    [Header("Y")]
    [SerializeField] private float _yDistance;
    [SerializeField] private float _ySpeed;
    [Header("X")]
    [SerializeField] private float _xDistance;
    [SerializeField] private float _xSpeed;
    [Header("Z")]
    [SerializeField] private float _zDistance;
    [SerializeField] private float _zSpeed;
    [SerializeField] private float _rotationSpeed;

    private float yPos = 0;
    private float xPos = 0;
    private float zPos = 0;

    private void Update()
    {
        yPos = CalculatePosition(_ySpeed, _yDistance);
        xPos = CalculatePosition(_xSpeed, _xDistance);
        zPos = CalculatePosition(_zSpeed, _zDistance);


        transform.localPosition = new Vector3(xPos, yPos, zPos);
        transform.Rotate(transform.up * _rotationSpeed * Time.deltaTime);
    }

    private float CalculatePosition(float speed, float distacne)
    {
        return distacne * Mathf.Sin(speed * Time.time);
    }
}
