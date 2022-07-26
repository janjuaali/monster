using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUIView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyUI;
    [SerializeField] private TMP_Text _bossReward;
    [SerializeField] private WalletView _walletView;

    private int _bossValue;

    private void OnEnable()
    {
        FindObjectOfType<BossLoader>().TryGetBossRewardValue(out _bossValue);

        StartCoroutine(Animation(_currencyUI, (int)_walletView.CurrencyCollected));

        StartCoroutine(Animation(_bossReward, _bossValue));
    }

    private IEnumerator Animation(TMP_Text text, int value)
    {
        float textValue = 0;
        float valuePerTick = 0;
        text.text = $"{(int)textValue}";

        if(value>0)
            valuePerTick = ((float)value * 0.02f);

        while (textValue< value)
        {
            yield return null;

            textValue += valuePerTick;

            textValue = Mathf.Clamp(textValue, 0, value);
            text.text = $"x{(int)textValue}";
        }
    }
}
