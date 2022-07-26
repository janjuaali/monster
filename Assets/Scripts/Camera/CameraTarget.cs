using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class CameraTarget : MonoBehaviour, IJumpable
{
    [SerializeField]private MovementSystem _movementSystem;
    [SerializeField]private MouseInput _mouseInput;

    private PlayerDeathHandler _playerDeathHandler;
    private JumpAnimation _jumpAnimation = new JumpAnimation();
    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        Error.CheckOnNull(_playerDeathHandler, nameof(PlayerDeathHandler));
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerLost += DisableControl;
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerLost -= DisableControl;
    }

    public void Jump(AnimationCurve animationCurve)
    {
        StartCoroutine(_jumpAnimation.Play(animationCurve, transform));
    }
    private void DisableControl(string lostCousse)
    {
        _movementSystem.enabled = false;
        _playerDeathHandler.PlayerLost -= DisableControl;
    }
}
