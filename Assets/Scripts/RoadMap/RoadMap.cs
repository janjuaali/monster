using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.UI;
using System;

public class RoadMap : MonoBehaviour
{
    [SerializeField] private Image _pathImage;
    [SerializeField] private List<RoadMapPoint> _points;
    [SerializeField] private float _timeToFill;

    private int _firstLevelIndex;
    private int _currentLevelIndex;
    private int _remainder;

    private const float FillStep = 0.25f;

    private void Awake()
    {
        _pathImage.fillAmount = 0;
        _currentLevelIndex = SaveSystem.LoadLevelsProgression();

        _remainder = _currentLevelIndex % 5;

        if (_remainder >= 0)
            _firstLevelIndex = _currentLevelIndex - _remainder;

        Init();
    }

    public void Init()
    {
        int levelIndex = _firstLevelIndex;

        foreach (var point in _points)
        {
            point.Init(levelIndex);

            if (levelIndex < _currentLevelIndex)
            {
                point.Fill();

                FillSegment();
            }

            levelIndex++;
        }
    }

    public void FillCurrentPoint()
    {
        Init();

        _pathImage.fillAmount = _remainder * FillStep;

        _points[_remainder].Fill();

        StartCoroutine(FillPathAnimation());
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void FillSegment()
    {
        _pathImage.fillAmount += FillStep;
    }

    private IEnumerator FillPathAnimation()
    {
        var targetFill = _pathImage.fillAmount + FillStep;
        var speed = FillStep / _timeToFill;

        while(_pathImage.fillAmount < targetFill)
        {
            _pathImage.fillAmount = Mathf.MoveTowards(_pathImage.fillAmount, targetFill, speed * Time.deltaTime);

            yield return null;
        }
    }
}
