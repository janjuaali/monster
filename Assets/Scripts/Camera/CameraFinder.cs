using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinder : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
