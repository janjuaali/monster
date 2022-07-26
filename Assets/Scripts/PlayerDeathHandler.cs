using System;
using RunnerMovementSystem.Examples;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour, IDeathBehavior
{
    [SerializeField] private MonstersAnimatorHandler _animatorHandler;
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private UIHandler _uIHandler;

    public event Action<string> PlayerLost;

    private bool _isDead;
    public void Die(LostCouse lostCouse)
    {
        if (_isDead)
            return;

        PlayerLost?.Invoke(lostCouse.ToString());
        _mouseInput.enabled = false;
        _animatorHandler.SetDeathAnimation();
        _uIHandler.Disable();

        _isDead = true;
    }
}

public enum LostCouse
{
    Obstacle,
    Battle,
    RunerEnemy
}
