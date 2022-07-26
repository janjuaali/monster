using System;
using UnityEngine;

public class Gate : Interactable
{
    [SerializeField] private GateIcon _gateIcon;
    [SerializeField] private MeshRenderer _meshRenderer;

    public Monster Monster { get; private set; }
    private bool _isActivated;
    private const int _level = 10;

    public event Action GateActivated;
    public event Action<Monster, Gate> NeedAnotherMonster;

    private void OnEnable()
    {
        _gateIcon.NeedAnotherMonster += OnNeedAnotherMonster;
    }

    private void OnDisable()
    {
        _gateIcon.NeedAnotherMonster -= OnNeedAnotherMonster;
    }

    public void SetMonster(Monster monster)
    {
        Monster = monster;
        _gateIcon.CreateIcon(Monster);
    }

    public void ResetMonster()
    {
        _gateIcon.ResetForm();
    }

    public void PlacePowerUp(Interactable powerUp)
    {
        _gateIcon.CreateIcon(powerUp);
    }

    public void ReplaceMonster(Monster monster)
    {
        Monster = monster;

        _gateIcon.ReplaceIcon(Monster);
    }

    public void OnNeedAnotherMonster(Monster monster)
    {
        NeedAnotherMonster?.Invoke(monster, this);
    }

    public override void Use(MonstersHandler monstersHandler)
    {
        if (_isActivated)
            return;

        Monster.FormsHandler.GetComponent<Rotator>().enabled = true;
        _isActivated = monstersHandler.TrySetMonsterToPlace(Monster, _level);

        if (_isActivated)
            GateActivated?.Invoke();
    }

    public void Disable()
    {
        _isActivated = true;

        foreach (var material in _meshRenderer.materials)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
        }
    }
}
