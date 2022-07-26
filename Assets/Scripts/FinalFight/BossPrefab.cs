using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPrefab : MonoBehaviour
{
    [SerializeField] private Monster[] _bosses;

    public IReadOnlyCollection<Monster> Bosses => _bosses;
}
