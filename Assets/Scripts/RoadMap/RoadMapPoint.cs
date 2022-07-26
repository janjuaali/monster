using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoadMapPoint : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private float _timeToFill;
    [SerializeField] private TMP_Text _levelNumber;

    public void Complete()
    {
        StartCoroutine(ChangeAnimation());
    }

    public void Init(float levelIndex)
    {
        levelIndex++;
        _levelNumber.text = $"{levelIndex}";
    }

    public void Fill()
    {
        _icon.fillAmount = 1f;
    }

    private IEnumerator ChangeAnimation()
    {
        float changeSpeed = 1f / _timeToFill;

        while (_icon.fillAmount < 1f)
        {
            _icon.fillAmount = Mathf.MoveTowards(_icon.fillAmount, 1f, changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
