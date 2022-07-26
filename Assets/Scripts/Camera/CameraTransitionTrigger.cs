using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CameraTransition), typeof(BoxCollider))]
public class CameraTransitionTrigger : MonoBehaviour
{
    private CameraTransition _cameraTransition;
    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _cameraTransition = GetComponent<CameraTransition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MonstersHandler monstersHandler))
        {
            _cameraTransition.TryTransit();
            _boxCollider.enabled = false;
        }
    }
}
