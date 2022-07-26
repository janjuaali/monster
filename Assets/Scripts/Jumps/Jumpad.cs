using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;

    public AnimationCurve animationCurve => _animationCurve;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IJumpable jumpable))
        {
            jumpable.Jump(_animationCurve);
        }
    }
}
