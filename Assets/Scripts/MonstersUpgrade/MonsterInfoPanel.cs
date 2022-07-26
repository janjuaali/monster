using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonsterInfoPanel : MonoBehaviour
{
    [SerializeField] private int _levelToShow;
    [SerializeField] private TMP_Text _lvlText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private Image _attackRangeImage;
    [SerializeField] private MonstersIcons _monstersIcons;
    [SerializeField] private MonsterLevelUpgrader _monsterLevelUpgrader;

    private StartLevelButton _startLevelButton;

    private Monster _monster;

    private void Awake()
    {
        _startLevelButton = FindObjectOfType<StartLevelButton>();
    }

    private void OnEnable()
    {
        _startLevelButton.RunStarted += Close;
    }

    private void OnDisable()
    {
        _startLevelButton.RunStarted -= Close;
    }

    public void Open(Monster monster)
    {
        float currentLevel = SaveSystem.LoadLevelsProgression();

        if (currentLevel < _levelToShow-1)
            return;

        _monster = monster;
        gameObject.SetActive(true);

        UpdateInfo();
        _monsterLevelUpgrader.Init();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void UpdateInfo()
    {
        _lvlText.text = $"{_monster.Level}";
        _nameText.text = $"{_monster.Name}";
        _healthText.text = $"{_monster.Health.MaxHealth}";
        _attackText.text = $"{_monster.Damage}";
        _attackRangeImage.sprite = _monstersIcons.GetAttackRangeIconSprite(_monster.Attack.InitialRange);
    }
}
