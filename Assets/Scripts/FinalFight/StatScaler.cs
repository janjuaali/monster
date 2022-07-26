using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatScaler
{
    public float ScaleByLevel(float level, float scaleMultiplier, float valuePerLevel)
    {
        int levelIndex = SaveSystem.LoadLevelsProgression();

        float additionalValue = 0;

        if (levelIndex > 14)
            additionalValue = (levelIndex - 13) * scaleMultiplier;

        return level * (valuePerLevel + additionalValue);
    }
}
