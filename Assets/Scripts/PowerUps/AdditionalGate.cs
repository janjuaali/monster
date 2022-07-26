using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalGate : MonoBehaviour
{
    [SerializeField] private ImproveGatesHandler _improveGatesHandler;

    public ImproveGatesHandler ImproveGatesHandler => _improveGatesHandler;
}
