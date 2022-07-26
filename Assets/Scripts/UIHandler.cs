using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceObject;
    [SerializeField] private Transform _CurrencySpawnPoint;

    public Vector3 Position => Camera.main.WorldToScreenPoint(_CurrencySpawnPoint.position);

    public void SwitchState()
    {
        _interfaceObject.gameObject.SetActive(!_interfaceObject.gameObject.activeInHierarchy);
    }

    public void Enable()
    {
        _interfaceObject.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _interfaceObject.gameObject.SetActive(false);
    }
}
