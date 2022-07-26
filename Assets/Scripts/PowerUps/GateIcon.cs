using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GateIcon : MonoBehaviour
{
    [SerializeField] private TMP_Text _monsterName;
    [SerializeField] private Image _rangeIcon;
    [SerializeField] private MonstersIcons _monstersIcons;

    private Monster _monsterIcon;
    private MonstersHandler _monstersHandler;
    private int _counter = 0;

    public event Action<Monster> NeedAnotherMonster;
    private void Awake()
    {
        _monstersHandler = FindObjectOfType<MonstersHandler>();
    }

    private void OnEnable()
    {
        _monstersHandler.MonsterMerged += UpdateIcon;
    }

    private void OnDisable()
    {
        _monstersHandler.MonsterMerged -= UpdateIcon;
    }

    public void CreateIcon(Monster monster)
    {
        _monsterIcon = Instantiate(monster);
        _monsterIcon.transform.position = transform.position;
        _monsterIcon.transform.rotation = transform.rotation;
        _monsterIcon.transform.SetParent(transform);
        _monsterIcon.Rigidbody.isKinematic = true;
        _monsterIcon.gameObject.layer = 0;
        _monsterIcon.GetComponent<Collider>().enabled = false;
        _monsterIcon.GetComponent<StateMachine>().enabled = false;
        _monsterIcon.GetComponentInChildren<Rotator>().enabled = false;
        _monsterIcon.MonsterAnimator.enabled = false;

        _monsterName.text = _monsterIcon.Name;

        _rangeIcon.enabled = true;
        _rangeIcon.sprite = _monstersIcons.GetAttackRangeIconSprite(_monsterIcon.Attack.InitialRange);
    }

    public void CreateIcon(Interactable powerUp)
    {
        Destroy(_monsterIcon.gameObject);
        var powerUpIcon = Instantiate(powerUp, transform, false);

        powerUpIcon.transform.localPosition = Vector3.up *1.5f;

        _monsterName.text = $"LevelUp";
    }

    public void ResetForm()
    {
        _counter = 0;
        _monsterIcon.FormsHandler.EnableFirstForm();
    }

    public void UpdateIcon(Monster monster)
    {
        if (monster.GetType() == _monsterIcon.GetType())
            if (_monsterIcon.TryMerge(0))
                _counter++;

        if (_counter >= 2)
        {
            _counter = 0;

            NeedAnotherMonster?.Invoke(monster);
        }
    }

    public void ReplaceIcon(Monster monster)
    {
        Destroy(_monsterIcon.gameObject);

        int formCounter = _monstersHandler.GetMonsterForm(monster);

        if (formCounter > 1)
            return;

        CreateIcon(monster);

        for (int i = 0; i < formCounter; i++)
        {
            UpdateIcon(monster);
        }
    }
}
