using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMergeable
{
    public bool TryMerge(int level);
}
