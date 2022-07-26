using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DailyRewardPresenter : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DailyRewardBehaviour _dailyRewardBehaviour;
    [SerializeField] private Image _claimedImage;
    [SerializeField] private Image _ClosedImage;

    private int _day;
    private string _saveName = "DailyRewardClaimed";

    public bool IsClaimed { get; private set; }
    public bool IsOpen { get; private set; }

    public void Init(int day)
    {
        _dailyRewardBehaviour.UpdateInfo();
        _day = day;

        _saveName = _saveName + day;

        if (PlayerPrefs.HasKey(_saveName))
        {
            _claimedImage.gameObject.SetActive(true);
            IsClaimed = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeClaimed())
        {
            _dailyRewardBehaviour.Acquire();
            Claim();
            IsClaimed = true;
        }
    }

    public void Open()
    {
        _ClosedImage.gameObject.SetActive(false);
        IsOpen = true;
    }

    public void Unclaim()
    {
        PlayerPrefs.DeleteKey(_saveName);
        IsClaimed = false;
    }

    private void Claim()
    {
        _claimedImage.gameObject.SetActive(true);
        PlayerPrefs.SetString(_saveName, _saveName);
    }

    private bool CanBeClaimed()
    {
        return IsClaimed == false && IsOpen;
    }
}
