using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HammerAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _delay;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _delay = Random.Range(0, 3);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _animator.enabled = true;        
    }
}
