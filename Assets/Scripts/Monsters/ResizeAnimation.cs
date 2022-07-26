using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResizeAnimation : MonoBehaviour
{
    [SerializeField] private EnlargeOptions _enlargeOptions;

    private Vector3 _initialScale;
    private int _step;
    private Coroutine _coroutine;
    private int _maxStep;

    public int Step => _step;
    private float _scalePerStep => (_enlargeOptions.MaxScale - _initialScale.x)/ _maxStep;
    private float _additionalScale => _step * _scalePerStep;
    private Vector3 _nexSteptScale => new Vector3(_initialScale.x + _additionalScale, _initialScale.y + _additionalScale, _initialScale.z + _additionalScale);
    private Vector3 _enlargeScale => _nexSteptScale * _enlargeOptions.ScaleCoefficient;

    public event Action<int, int> StepChanged;

    private void Start()
    {
        _initialScale = transform.localScale;
        StepChanged?.Invoke(_step, _maxStep);
    }

    public void Reset()
    {
        _step = 0;
        StepChanged?.Invoke(_step, _maxStep);
    }

    public void SetMaxStep(int maxLevel)
    {
        _maxStep = maxLevel;
    }

    public void PlayEnlargeAnimation(int count, bool evo = false)
    {
        int multiplier = 1;

        if (_step < _maxStep)
        {
            _step+= count;

            StepChanged?.Invoke(_step, _maxStep);
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (evo)
            multiplier = 3;

        _coroutine = StartCoroutine(Enlarge(multiplier));
    }

    public void ShrinkAnimation(int stepCount)
    {
        if (_step > 0)
        {
            _step -= stepCount;

            StepChanged?.Invoke(_step, _maxStep);
        }
    }

    private IEnumerator Enlarge(int multilpier)
    {
        var targetScale = Vector3.one + (_enlargeScale - Vector3.one) * multilpier;

        float changeSpeed = Mathf.Abs(transform.localScale.x - targetScale.x / _enlargeOptions.Time);

        while (transform.localScale.x < targetScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, changeSpeed * Time.deltaTime);

            yield return null;
        }

        changeSpeed = Mathf.Abs(transform.localScale.x - _nexSteptScale.x / _enlargeOptions.Time);

        while (transform.localScale.x > _nexSteptScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _nexSteptScale, changeSpeed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }



    private IEnumerator Shrink()
    {
        float changeSpeed = Mathf.Abs(transform.localScale.x - _nexSteptScale.x / _enlargeOptions.Time);

        while (transform.localScale.x > _nexSteptScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _nexSteptScale, changeSpeed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}
