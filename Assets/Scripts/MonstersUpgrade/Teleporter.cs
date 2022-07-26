using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform _pointTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SwipeMover swipeMover))
            swipeMover.transform.localPosition = _pointTo.localPosition;
    }
}
