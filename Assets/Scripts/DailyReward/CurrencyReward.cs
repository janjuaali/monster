using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyReward : DailyRewardBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private FlyingPicture _flyingPicture;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _amountText;

    private WalletView _walletView;

    public int Amount => _amount;

    public override void Acquire()
    {
        _walletView = FindObjectOfType<WalletView>();
        Player player = FindObjectOfType<Player>();
        player.CurrencyHandler.Increase(_amount, false);

        int amount = 0;

        if (_amount < 200)
        {
            while (amount < _amount)
            {
                var flyingPicture = Instantiate(_flyingPicture, _walletView.Image.transform);
                flyingPicture.transform.position = transform.position;
                flyingPicture.SetSprite(_image.sprite);
                flyingPicture.DelayedInit(Vector3.zero, _image.sprite, 75, _image.rectTransform.rect.width, _walletView, 1, 1f);
                amount++;
            }
        }
        else
        {
            {
                while (amount < _amount)
                {
                    var flyingPicture = Instantiate(_flyingPicture, _walletView.Image.transform);
                    flyingPicture.transform.position = transform.position;
                    flyingPicture.SetSprite(_image.sprite);
                    flyingPicture.DelayedInit(Vector3.zero, _image.sprite, 75, _image.rectTransform.rect.width, _walletView, 10, 1f);
                    amount += 20;
                }
            }
        }

    }

    public override void UpdateInfo()
    {
        _amountText.text = $"x{_amount}";
    }
}
