using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;

    public bool _dayEnded;

    public event Action DayEnded;

    private void Update()
    {
        float hours = 23 - DateTime.Now.Hour;
        float minutes = 60 - DateTime.Now.Minute;
        float second = 60 - DateTime.Now.Second;

        _timer.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, second);

        if (hours == 0 && minutes == 0 & second >= 0 && _dayEnded == false)
        {
            DayEnded?.Invoke();
            _dayEnded = true;
        }
    }
}
