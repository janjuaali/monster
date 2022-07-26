using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public abstract class ShopButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _soldOut;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private float _costPerBuy;
    [SerializeField] private PurchaseType _purchaseType;
    [SerializeField] private PurchaseName _purchaseName;
    [SerializeField] private float _minValue;
    [SerializeField] private float _minCost;
    [SerializeField] private float _maxBuying = 100;
    [SerializeField] private Image _notEnoughMoney;

    protected Player Player;
    private const string BuyCounterSaveName = "BuyCounter";
    private bool _isInactive;
    protected IntegrationMetric _integrationMetric = new IntegrationMetric();

    protected ValueHandler BuyCounter { get; private set; }
    protected ValueHandler ValueHandler { get; private set; }
    protected ValueHandler CostHandler { get; private set; }

    private void OnEnable()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<Player>();
            Error.CheckOnNull(Player, nameof(Player));
        }

        Player.CurrencyHandler.ValueChanged += OnValueChanged;
        OnValueChanged();

    }

    private void OnDisable()
    {
        Player.CurrencyHandler.ValueChanged -= OnValueChanged;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInactive)
            return;

        if (Player == null)
            Player = FindObjectOfType<Player>();

        if (Player.CurrencyHandler.TryDecrease(CostHandler.Value))
        {
            BuyCounter.Increase(1);

            Buy(CostHandler.Value);

            CostHandler.Increase(_costPerBuy);

            UpdateInfo();

            _integrationMetric.OnSoftCurrencySpend(_purchaseType.ToString(), _purchaseName.ToString(), (int)CostHandler.Value);

            if (BuyCounter.Value >= _maxBuying -1)
                SetInactive();
        }
    }

    public abstract void Buy(float cost);

    public void SetInactive(bool isInactive = true)
    {
        _isInactive = isInactive;
        _soldOut.SetActive(isInactive);
    }

    protected virtual void UpdateInfo()
    {
        _label.text = $"x{CostHandler.Value}";
    }

    public void LoadProgression(string saveName)
    {
        BuyCounter = new ValueHandler(0, _maxBuying, $"{BuyCounterSaveName}{saveName}");
        BuyCounter.LoadAmount();

        ValueHandler = new ValueHandler(_minValue, 10000, saveName);
        ValueHandler.LoadAmount();

        CostHandler = new ValueHandler(_minCost, 10000, saveName + 1);
        CostHandler.LoadAmount();
        UpdateInfo();

        if (BuyCounter.Value >= _maxBuying-1)
            SetInactive();
    }

    protected void OnValueChanged()
    {
        if (CostHandler == null)
            return;

        bool isEnoughMoney = Player.CurrencyHandler.Value < CostHandler.Value;

        _notEnoughMoney.gameObject.SetActive(isEnoughMoney);
    }
}

public enum PurchaseType
{
    improvment
}

public enum PurchaseName
{
    LvlImprovment,
    MonsterLevelUpgrade,
    MonsterBuying,
    CurrencyMultiplier,
    HealUpgrade,
    MeteorUpgrade,
    ProtectionUpgrade
}
