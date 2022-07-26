using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishUiEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _finishFightUi;

    private ToFightTrigger _toFightTrigger;
    private WinnerDecider _winnerDecider;

    private void Awake()
    {
        _toFightTrigger = FindObjectOfType<ToFightTrigger>();
        _winnerDecider = FindObjectOfType<WinnerDecider>();
        _finishFightUi.SetActive(false);
    }

    private void OnEnable()
    {
        _toFightTrigger.PlayerEnteredFight += OnPlayerEneteredFight;
        _winnerDecider.Victory += Disable;
    }

    private void OnDisable()
    {
        _toFightTrigger.PlayerEnteredFight -= OnPlayerEneteredFight;
        _winnerDecider.Victory -= Disable;
    }

    public void Disable()
    {
        _finishFightUi.gameObject.SetActive(false);
    }

    private void OnPlayerEneteredFight()
    {
        _finishFightUi.gameObject.SetActive(true);
    }
}
