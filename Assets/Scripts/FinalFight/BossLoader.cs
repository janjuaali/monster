using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLoader : MonoBehaviour
{
    [SerializeField] private BossPrefab[] _bossPrefabs;
    [SerializeField] private BossPrefab _normalBosses;

    private WinnerDecider _winnerDecider;
    private BossPrefab _currentBossPrefab;
    private const string SaveName = "BossLoader";

    private void Awake()
    {
        int levelIndex = SaveSystem.LoadLevelsProgression();
        
        if (levelIndex >= 14 && LevelsList.BossLevelFilter(levelIndex) == false)
        {
            _normalBosses.gameObject.SetActive(false);
            _currentBossPrefab = Instantiate(_bossPrefabs[GetBossPrefabIndex()], transform.position, transform.rotation);
        }
        else
        {
            _currentBossPrefab = _normalBosses;
        }
    }

    private void OnEnable()
    {
        foreach (var boss in _currentBossPrefab.Bosses)
        {
            boss.Died += OnMonsterDied;
        }
    }

    private void OnDisable()
    {
        foreach (var boss in _currentBossPrefab.Bosses)
        {
            boss.Died -= OnMonsterDied;
        }
    }

    public bool TryGetBossRewardValue(out int value)
    {
        value = 0;

        if (_currentBossPrefab == null)
            return false;

        value = _currentBossPrefab.Bosses.Count * 20;

        return true;
    }

    private int GetBossPrefabIndex()
    {
        int index = 0;
        int previousIndex = -1;

        if (PlayerPrefs.HasKey(SaveName))
            previousIndex = PlayerPrefs.GetInt(SaveName);

        if (_bossPrefabs.Length > 1)
        {
            do
            {
                index = Random.Range(0, _bossPrefabs.Length);
            } while (index == previousIndex);
        }

        PlayerPrefs.SetInt(SaveName, index);

        return index;
    }

    private void OnMonsterDied()
    {
        if (_winnerDecider == null)
            _winnerDecider = FindObjectOfType<WinnerDecider>();

        _winnerDecider.OnMonsterDied(_currentBossPrefab.Bosses.Count);
    }
}
