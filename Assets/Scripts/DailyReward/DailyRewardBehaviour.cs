using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DailyRewardBehaviour : MonoBehaviour
{
    public abstract void Acquire();
    public virtual void UpdateInfo() { }

    private void Expire()
    {
        gameObject.SetActive(false);
    }
}
