using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTrapAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string _stepped = "Stepped";

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            _animator.SetTrigger(_stepped);
    }
}
