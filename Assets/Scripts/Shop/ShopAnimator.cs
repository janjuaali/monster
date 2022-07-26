using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Open = "Open";
    private const string Close = "Close";
    private const string HideShopIcon = "HideIcon";

    public void OpenAnimation()
    {
        _animator.SetTrigger(Open);
    }

    public void CloseAnimation()
    {
        _animator.SetTrigger(Close);
    }

    public void HideIcon()
    {
        _animator.SetTrigger(HideShopIcon);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
