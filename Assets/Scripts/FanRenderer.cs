using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRenderer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material[] _materials;

    private void Awake()
    {
        int index = Random.Range(0, _materials.Length);
        _skinnedMeshRenderer.material = _materials[index];
    }
}
