using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class AbilitiyButtonView : Ability, IPointerClickHandler
{
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Image _cooldownImage;

    private void Start()
    {
        UpdateView();
    }

    private void OnEnable()
    {
        ValueHandler.ValueChanged += UpdateView;
        AmountHandler.ValueChanged += UpdateView;
        UpdateView();
    }

    private void OnDisable()
    {
        ValueHandler.ValueChanged -= UpdateView;
        AmountHandler.ValueChanged -= UpdateView;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Use();
        UpdateView();
    }

    protected override void OnAbilityCasted()
    {
        StartCoroutine(CooldownAnimation());
    }

    private void UpdateView()
    {
        _amount.text = $"{AmountHandler.Value}";

        string value = ValueHandler.Value.ToString();

        float decimalNumber = value.Split(',').Length;

        if (decimalNumber > 2)
            value = String.Format("{0:0.0}", ValueHandler.Value);

        _value.text = $"{value}";
    }

    private IEnumerator CooldownAnimation()
    {
        while (IsOnCooldown)
        {
            _cooldownImage.fillAmount = ElapsedTime;

            yield return null;
        }
    }
}
