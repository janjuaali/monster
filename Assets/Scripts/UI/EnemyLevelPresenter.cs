using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLevelPresenter : LevelPresenter
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Color _weakColor;
    [SerializeField] private Image _background;

    private MonstersHandler _monstersHandler;
    private Color _inititalColor = Color.red;
    private void Awake()
    {
        _inititalColor = _background.color;
        _monstersHandler = FindObjectOfType<MonstersHandler>();
        Error.CheckOnNull(_monstersHandler, nameof(MonstersHandler));
    }
    private void Start()
    {
        Show(_monster.Level);
    }

    private void OnEnable()
    {
        _monstersHandler.MightChanged += ChangeColor;
    }

    private void OnDisable()
    {
        _monstersHandler.MightChanged -= ChangeColor;
    }

    private void ChangeColor(int might, int diffrence)
    {
        if (might < _monster.Level)
            _background.color = _inititalColor;
        else
            _background.color = _weakColor;

    }
}
