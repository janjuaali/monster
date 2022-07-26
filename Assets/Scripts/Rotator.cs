using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private RotatorOptions _rotatorOptions;

    private PlayerDeathHandler _playerDeathHandler;
    private Quaternion _forwardQuaternionRotation = new Quaternion(0, 0, 0, 1);
    private float xRotation;
    private float _threshold = 0.1f;
    private float _smoothMultiplier = 10;

    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerLost += Disable;
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerLost -= Disable;
    }

    private void Update()
    {
        float pointerX = Input.GetAxis("Mouse X") * _rotatorOptions.RotationSpeed * _smoothMultiplier * Time.deltaTime;

        SetRotation(pointerX);

        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(pointerX) >= _threshold)
                transform.localRotation = Quaternion.Euler(0, xRotation, 0f);
        }

        ResetRotation();
    }

    public void Rotate(float pointerX)
    {
        SetRotation(pointerX);
        transform.localRotation = Quaternion.Euler(0, xRotation, 0f);
    }

    public void SetRotation(float pointerX)
    {
        xRotation += pointerX;

        xRotation = Mathf.Clamp(xRotation, -_rotatorOptions.MaxAngle, _rotatorOptions.MaxAngle);
    }

    private void ResetRotation()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, _forwardQuaternionRotation, _rotatorOptions.ResetSpeed * Time.deltaTime);
        xRotation = transform.localEulerAngles.y;

        if (xRotation > 180)
            xRotation -= 360;
    }

    private void Disable(string lostCouse)
    {
        this.enabled = false;
    }
}
