using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;
    [SerializeField] private bool _InitialLevel;

    private WinnerDecider _winnderDecider;
    private PlayerDeathHandler _playerDeathHandler;
    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private float _timePassed;

    public int Counter { get; private set; }

    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        _winnderDecider = FindObjectOfType<WinnerDecider>();

        Counter = SaveSystem.LoadLevelsProgression();
    }

    private void Start()
    {
        _timePassed = Time.time;

        if (_InitialLevel == false)
            _integrationMetric.OnLevelStart(Counter);
    }

    private void OnEnable()
    {
        if (_playerDeathHandler != null)
            _playerDeathHandler.PlayerLost += OnLevelFailed;

        if (_winnderDecider != null)
            _winnderDecider.Victory += OnLevelCompleted;
    }

    private void OnDisable()
    {
        if (_playerDeathHandler != null)
            _playerDeathHandler.PlayerLost -= OnLevelFailed;

        if (_winnderDecider != null)
            _winnderDecider.Victory -= OnLevelCompleted;
    }

    public void LoadNextLevel()
    {
        if (Counter >= _levelList.SceneCount)
            _levelList.GetRandomScene(Counter).LoadSceneAsync();
        else
            _levelList.GetScene(Counter).LoadSceneAsync();
    }

    public void RestartLevel()
    {
        _integrationMetric.OnRestartLevel(Counter);

        var scene = _levelList.GetCurrentScene();

        Addressables.LoadSceneAsync(scene);
    }

    public void OnLevelCompleted()
    { 
        WalletView walletView = FindObjectOfType<WalletView>();
        int currencyCollected = (int)walletView.CurrencyCollected;
        int amount = (int)walletView.Amount;

        _integrationMetric.OnLevelComplete(GetTime(), Counter, currencyCollected, amount);

        Counter++;

        SaveSystem.SaveLevelsProgression(Counter);
    }

    private void OnLevelFailed(string lostCouse)
    {
        _integrationMetric.OnLevelFail(GetTime(), Counter, lostCouse);
    }

    private int GetTime()
    {
        return (int)(Time.time - _timePassed);
    }
}
