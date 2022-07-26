using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMover : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    private SwipeZone _swipeZone;
    private float _targetXPosition;
    private Coroutine _coroutine;
    private Queue<Coroutine> _coroutines = new Queue<Coroutine>();
    public SwipeZone SwipeZone => _swipeZone;

    public bool IsInTransition { get; private set; }

    public void Init(SwipeZone swipeZone)
    {
        _swipeZone = swipeZone;
    }

    public void Move(float speed)
    {
        transform.localPosition += (Vector3.left * Time.deltaTime * speed);

        if (transform.localPosition.x <= _swipeZone.RightBorder.x)
            Teleport(_swipeZone.LeftOffset);

        if (transform.localPosition.x >= _swipeZone.LeftBorder.x)
            Teleport(_swipeZone.RightOffset);
    }

    public void TranslateLeft(float targetxPosition)
    {
        _targetXPosition = targetxPosition;

        IsInTransition = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveAnimation(IsLeftPositionReached));
    }

    public void TranslateRight(float targetxPosition)
    {
        _targetXPosition = targetxPosition;

        IsInTransition = true;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveAnimation(IsRightPositionReached));
    }

    private IEnumerator MoveAnimation(Func<bool> IsReached)
    {
        float xPosition = transform.localPosition.x;

        IsInTransition = true;

        while (IsReached())
        {
            xPosition = Mathf.MoveTowards(xPosition, _targetXPosition, 3.5f * Time.deltaTime);
            transform.localPosition = new Vector3(xPosition, transform.localPosition.y, transform.localPosition.z);

            yield return new WaitForEndOfFrame();
        }

        IsInTransition = false;
        _targetXPosition = 0;
    }

    private bool IsRightPositionReached()
    {
        return transform.localPosition.x > _targetXPosition;
    }

    private bool IsLeftPositionReached()
    {
        return transform.localPosition.x < _targetXPosition;
    }

    private void Teleport(float offset)
    {
        transform.localPosition = new Vector3(transform.localPosition.x + offset, transform.localPosition.y, transform.localPosition.z);
    }
}
