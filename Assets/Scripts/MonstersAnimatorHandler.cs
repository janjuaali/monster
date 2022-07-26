using UnityEngine;
using RunnerMovementSystem.Examples;
using System.Collections.Generic;

public class MonstersAnimatorHandler : MonoBehaviour
{
    [SerializeField] private MouseInput _mouseInput;

    private WinnerDecider _winnerDecider;
    private bool _isRunnig;

    private List<MonsterAnimator> _monstersAnimator = new List<MonsterAnimator>();

    private void Awake()
    {
        _winnerDecider = FindObjectOfType<WinnerDecider>();
        Error.CheckOnNull(_winnerDecider, nameof(WinnerDecider));
    }
    private void OnEnable()
    {
        _mouseInput.RunBegan += SetRunAnimation;
        _winnerDecider.Victory += OnVictory;
    }

    private void OnDisable()
    {
        _mouseInput.RunBegan -= SetRunAnimation;
        _winnerDecider.Victory -= OnVictory;
    }

    public void AddAnimator(MonsterAnimator monsterAnimator)
    {
        _monstersAnimator.Add(monsterAnimator);

        if (_isRunnig)
            monsterAnimator.RunAnimation();
    }

    public void RemoveAnimator(MonsterAnimator monsterAnimator)
    {
        if (_monstersAnimator.Contains(monsterAnimator))
            _monstersAnimator.Remove(monsterAnimator);
    }

    public void SetDeathAnimation()
    {
        foreach (var monsterAnimator in _monstersAnimator)
        {
            monsterAnimator.DieAnimation();
        }
    }

    public void OnVictory()
    {
        foreach (var monsterAnimator in _monstersAnimator)
        {
            monsterAnimator.LookAtPlayer();
        }
    }

    public void TriggerAttackAnimation()
    {
        foreach (var monsterAnimator in _monstersAnimator)
        {
            monsterAnimator.TriggerAttackAnimation();
        }
    }

    private void SetRunAnimation()
    {
        foreach (var monsterAnimator in _monstersAnimator)
        {
            monsterAnimator.RunAnimation();
        }

        _isRunnig = true;
        _mouseInput.RunBegan -= SetRunAnimation;
    }
}
