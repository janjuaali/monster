using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopsHandler : MonoBehaviour
{
    private ShopButton[] _shopButtons;

    private void Start()
    {
        _shopButtons = FindObjectsOfType<ShopButton>();
        Error.CheckOnNull(_shopButtons, nameof(ShopButton));
    }
}
