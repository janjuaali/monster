using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTransitionHandler : MonoBehaviour
{
    private WinnerDecider _winnerDecider;
    private CameraTransition _cameraTransition;

    private void Awake()
    {
        _winnerDecider = FindObjectOfType<WinnerDecider>();
        Error.CheckOnNull(_winnerDecider, nameof(WinnerDecider));

        _cameraTransition = GetComponent<CameraTransition>();
    }

    private void OnEnable()
    {
        _winnerDecider.Victory += StartTransition;
    }

    private void OnDisable()
    {
        _winnerDecider.Victory -= StartTransition;
    }

    private void StartTransition()
    {
        _cameraTransition.TryTransit();
    }
}
