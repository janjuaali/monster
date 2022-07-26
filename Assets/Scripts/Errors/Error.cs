using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Error
{
    public static void CheckOnNull(object obj, string name)
    {
        if(obj == null)
            throw new NullReferenceException($"FindObjectOfType did not find {name}");
    }
}
