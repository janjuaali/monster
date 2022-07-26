using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGateSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialGates;
    [SerializeField] private ImproveGatesHandler _improveGatesHandler;
    [SerializeField] private string _saveName;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_saveName))
        {
            _improveGatesHandler.gameObject.SetActive(true);
            _tutorialGates.gameObject.SetActive(false);
        }
        else
        {
            _tutorialGates.gameObject.SetActive(true);
            _tutorialGates.gameObject.SetActive(true);
            _improveGatesHandler.gameObject.SetActive(false);
        }

        PlayerPrefs.SetString(_saveName, _saveName);
    }
}
