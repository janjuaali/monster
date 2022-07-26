using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] protected TMP_Text Level;

    public void Show(int level)
    {
        Level.text = $"Lv.{level}";
    }
}
