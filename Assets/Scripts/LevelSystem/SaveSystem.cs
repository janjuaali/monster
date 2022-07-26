using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private const string _levelKey = "LevelKey";
    private const int _firstLevel = 0;

    public static void SaveLevelsProgression(int index)
    {
        PlayerPrefs.SetInt(_levelKey, index);
    }

    public static int LoadLevelsProgression()
    {
        if (PlayerPrefs.HasKey(_levelKey))
            return PlayerPrefs.GetInt(_levelKey);

        return _firstLevel;
    }
}
