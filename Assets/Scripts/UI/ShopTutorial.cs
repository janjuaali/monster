using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopTutorial : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject[] _tutorObjects;
    [SerializeField] private ShopTutorial _nextStep; 
    [SerializeField] private string _saveWord;
    [SerializeField] private bool _pauseTheGame;
    [SerializeField] private Button _button;
    [SerializeField] private Button _startLevelButton;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(_saveWord))
        {
            CloseTutorial();
        }
        else
        {
            if (_pauseTheGame)
                FreezeGame();
        }

        if(_button != null)
            _button.onClick.AddListener(UnFreezeGame);

        if(_startLevelButton != null)
            _button.onClick.AddListener(UnFreezeGame);

        PlayerPrefs.SetString(_saveWord, _saveWord);
    }

    public void FreezeGame()
    {
        if (PlayerPrefs.HasKey(_saveWord))
            return;

        Time.timeScale = 0;
        StartCoroutine(DelayedUnFreeze());
        PlayerPrefs.SetString(_saveWord, _saveWord);
    }

    private void UnFreezeGame()
    {
        Time.timeScale = 1;
        CloseTutorial();
        _button.onClick.RemoveListener(UnFreezeGame);

        if(_nextStep != null)
            OpenNextStep();
    }

    private IEnumerator DelayedUnFreeze()
    {
        float elapsedTime = 0;

        while(elapsedTime < 4)
        {
            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        UnFreezeGame();
    }

    private IEnumerator DelayedFreeze()
    {
        yield return new WaitForSeconds(2);


    }

    private void CloseTutorial()
    {
        foreach (var tutorObject in _tutorObjects)
        {
            tutorObject.SetActive(false);
        }
    }

    private void OpenNextStep()
    {
        _nextStep.gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CloseTutorial();
    }
}
