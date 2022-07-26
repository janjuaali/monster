using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameHandler : MonoBehaviour
{
    [SerializeField] private Image _usedImage;
    [SerializeField] private Image _notUsedImage;

    public void SwitchState(bool isUsed)
    {
        _usedImage.gameObject.SetActive(isUsed);
    }
}
