using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private RoadMap _roadMap;
    [SerializeField] private GameObject[] _screensToClose;

    private MonsterAnimator[] _monsterAnimators;
    private PlayerDeathHandler _playerDeathHandler;
    private int _counter;

    public event Action Victory;

    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        Error.CheckOnNull(_playerDeathHandler, nameof(PlayerDeathHandler));

        if (_winScreen.activeInHierarchy)
            _winScreen.SetActive(false);
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerLost += OnPlayerLost;
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerLost -= OnPlayerLost;
    }

    public void OnMonsterDied(int bossCount)
    {
        _counter++;

        if (_counter >= bossCount)
            SetVictory();
    }

    private void SetVictory()
    {
        StartCoroutine(DelayedEnable());

        SetVictoryAnimation();

        RewardPlayer();

        Victory?.Invoke();
    }

    private void RewardPlayer()
    {
        Player player = FindObjectOfType<Player>();

        BossLoader bossLoader = FindObjectOfType<BossLoader>();

        bossLoader.TryGetBossRewardValue(out int value);

        player.CurrencyHandler.Increase(value, false);
    }

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(1f);

        _winScreen.SetActive(true);
        //_roadMap.gameObject.SetActive(true);
        //_roadMap.FillCurrentPoint();

        foreach (var screen in _screensToClose)
        {
            screen.SetActive(false);
        }
    }

    public void OnPlayerLost(string lostCouse)
    {
        SetVictoryAnimation();

        foreach (var screen in _screensToClose)
        {
            screen.SetActive(false);
        }
    }

    private void SetVictoryAnimation()
    {
        _monsterAnimators = FindObjectsOfType<MonsterAnimator>();

        foreach (var monsterAnimator in _monsterAnimators)
        {
            monsterAnimator.TriggerVictory();
        }
    }
}
