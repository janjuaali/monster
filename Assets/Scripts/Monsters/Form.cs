using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(RagdollHandler))]
public class Form : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _particlePoint;
    [ColorUsage(true, true)] [SerializeField] private  Color _damagedColor;

    private Animator _animator;
    private RagdollHandler _ragdollHandler;

    private float _initialSpecularSize;
    private Color _initialColor;

    public SkinnedMeshRenderer SkinnedMeshRenderer { get; private set; }
    public Animator FormAnimator => _animator;

    private Coroutine _coroutine;

    private void Awake()
    {
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _animator = GetComponent<Animator>();
        _ragdollHandler = GetComponent<RagdollHandler>();

        _initialSpecularSize = SkinnedMeshRenderer.materials[0].GetFloat("_FlatSpecularSize");

        if (_particleSystem != null)
            _particleSystem = Instantiate(_particleSystem, _particlePoint.position, _particlePoint.rotation);

        _initialColor = SkinnedMeshRenderer.materials[0].GetColor("_FlatSpecularColor");
    }

    public void EnableRagdoll()
    {
        _ragdollHandler.EnableRagdoll();
    }

    private void RangeAttack()
    {
        if (_monster == null)
            return;

        if (_monster.IsAllive)
        {
            _monster.DealDamage();

            if (_particleSystem != null)
                _particleSystem.Play();
        }
    }

    private void Hit()
    {
        if (_monster == null)
            return;

        if (_monster.IsAllive)
        {
            _monster.DealDamage();

            if (_particleSystem != null)
                _particleSystem.Play();
        }
    }

    public void OnDamaged()
    {
        SkinnedMeshRenderer.materials[0].SetFloat("_FlatSpecularSize", 0.78f);
        SkinnedMeshRenderer.materials[0].SetColor("_FlatSpecularColor", _damagedColor);

        if (_coroutine != null)
            StopCoroutine(MagicalMaterial());

        _coroutine =StartCoroutine(MagicalMaterial());
    }

    private IEnumerator MagicalMaterial()
    {
        yield return new WaitForSeconds(0.1f);

        SkinnedMeshRenderer.materials[0].SetFloat("_FlatSpecularSize", _initialSpecularSize);
        SkinnedMeshRenderer.materials[0].SetColor("_FlatSpecularColor", _initialColor);

        _coroutine = null;
    }

    private void Run() { }
}
