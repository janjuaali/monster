using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] private bool _isFinisher;
    [SerializeField] private bool _isRagdolDeath;
    [SerializeField] private FormsHandler _formsHandler;
    [SerializeField] private StateMachine _stateMachine;

    private Animator _animator => _formsHandler.CurrentFormAnimator;

    private UIHandler _uIHandler;

    private const string Run = "Run";
    private const string Die = "Die";
    private string _fightAttack = "FightAttack";
    private string _attack = "Attack";
    private const string Idle = "Idle";
    private const string Victory = "Victory";
    private const string Hit = "TakeHit";
    private const string Placed = "Placed";

    private bool _isDead;

    private void Awake()
    {
        _uIHandler = GetComponent<UIHandler>();
    }
    private void Start()
    {
        if (_isFinisher)
            ToFightTransition();
    }

    private void OnEnable()
    {
        _formsHandler.FormChanged += RunAnimation;
        _stateMachine.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _formsHandler.FormChanged -= RunAnimation;
        _stateMachine.StateChanged -= OnStateChanged;
    }

    public void MonsterPlaced()
    {
        _animator.SetTrigger(Placed);
    }

    public void TriggerAttackAnimation()
    {
        _animator.SetTrigger(_attack);
    }

    public void RunAnimation()
    {
        _animator.SetTrigger(Run);
    }

    public void DieAnimation()
    {
        if(_isDead == false)
        {
            _animator.SetBool(Die, true);
            _animator.SetLayerWeight(1, 0);
        }

        if (_isRagdolDeath)
        {
            _animator.enabled = false;
            _formsHandler.CurrentForm.EnableRagdoll();
        }

        _isDead = true;
    }

    public void IdleAnimation()
    {
        _animator.SetTrigger(Idle);
    }

    public void HitAnimation()
    {
        _animator.SetTrigger(Hit);
    }

    public void TriggerVictory()
    {
        if (_isDead)
            return;

        _uIHandler.SwitchState();

        VictoryAnimation();
        _animator.ResetTrigger(Run);
    }

    public void VictoryAnimation(bool isOneTime = false)
    {
        _animator.SetTrigger(Victory);

        if(isOneTime)
            _animator.SetTrigger(Placed);
    }

    public void LookAtPlayer()
    {
        _stateMachine.enabled = false;

        Vector3 lookDirection = (Camera.main.transform.position - _formsHandler.CurrentForm.transform.position).normalized;
        lookDirection.x = 0;
        lookDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, transform.up);
        StartCoroutine(LookAnimation(rotation));
    }

    private IEnumerator LookAnimation(Quaternion rotation)
    {
        while(_formsHandler.CurrentForm.transform.rotation != rotation)
        {
            _formsHandler.CurrentForm.transform.rotation = Quaternion.Lerp(_formsHandler.CurrentForm.transform.rotation, rotation, 20 * Time.deltaTime);

            yield return null;
        }
    }

    public void ToFightTransition()
    {
        _attack = _fightAttack;
        _animator.SetLayerWeight(1, 0);
    }

    private IEnumerator ResetTrigger(string name)
    {
        yield return new WaitForSeconds(0.2f);

        _animator.ResetTrigger(name);
    }

    private IEnumerator ResetWeight()
    {
        yield return new WaitForSeconds(0.01f);

        _animator.SetLayerWeight(1,0);
    }

    private void OnStateChanged(StateBehavior stateBehavior)
    {
        if (_isDead)
            return;

        if (stateBehavior is MoveState)
        {
            _animator.ResetTrigger(Idle);
            _animator.ResetTrigger(_attack);
            RunAnimation();
        }
        else if(stateBehavior is AttackState)
        {
            _animator.ResetTrigger(Run);
            TriggerAttackAnimation();
        }
        else if (stateBehavior is IdleState)
        {
            IdleAnimation();
        }
    }
}
