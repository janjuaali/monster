using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyAmount;
    [SerializeField] private Image _image;
    [SerializeField] private FlyingPicture _flyingPicture;

    private Player _player;
    private int _amount;

    public float Amount => _player.CurrencyHandler.Value;
    public float CurrencyCollected { get; private set; }
    public Image Image => _image;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Error.CheckOnNull(_player, nameof(Player));
    }

    private void OnEnable()
    {
        _player.CurrencyHandler.ValueIncreased += OnAmountChange;
        _player.CurrencyHandler.ValueLoaded += OnAmountLoaded;
        _player.CurrencyHandler.ValueDecreased += ChangeViewText;
    }

    private void OnDisable()
    {
        _player.CurrencyHandler.ValueIncreased -= OnAmountChange;
        _player.CurrencyHandler.ValueLoaded -= OnAmountLoaded;
        _player.CurrencyHandler.ValueDecreased -= ChangeViewText;
    }

    public void ChangeViewText(float amount)
    {
        _amount += (int)amount;
        _currencyAmount.text = $"{_amount}";
    }

    private void OnAmountChange(float amount, float changeValue)
    {
        var flyingPicture = Instantiate(_flyingPicture, _image.transform);
        flyingPicture.transform.position = _player.UiHandler.Position;
        flyingPicture.Init(Vector3.zero, _image.sprite, _image.rectTransform.rect.width, this, changeValue, 1);
        CurrencyCollected += changeValue;
    }

    private void OnAmountLoaded(float amount)
    {
        ChangeViewText(amount);
        _player.CurrencyHandler.ValueLoaded -= OnAmountLoaded;
    }
}
