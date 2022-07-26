using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FanAnimator : MonoBehaviour
{
    private Animator _animator;
    private List<string> _animationTriggers = new List<string>
    {
        "Happy",
        "Tired",
        "Bored"
    };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        float delay = Random.Range(0, 5f);

        Invoke("StartAnimation", delay);
    }

    private void StartAnimation()
    {
        int index = Random.Range(0, _animationTriggers.Count);
        _animator.SetTrigger(_animationTriggers[index]);
    }
}
