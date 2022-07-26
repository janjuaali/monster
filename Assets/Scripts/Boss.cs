using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]
public class Boss : MonoBehaviour
{
    public Monster Monster {get; private set;}

    private void Awake()
    {
        Monster = GetComponent<Monster>();
    }
}
