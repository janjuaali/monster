using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterPersistence
{
    [SerializeField] private MonsterList _monsterList;

    private const string _monsterSaveName = "monsterPersitence";

    public bool HaveSavedMonster => PlayerPrefs.HasKey(_monsterSaveName + 0);

    public void Save(Monster monster, int position)
    {
        if (_monsterList.TryGetMonsterId(monster, out int index))
        { 
            PlayerPrefs.SetInt(_monsterSaveName + position, index);
        }
    }

    public bool TryLoad(int position, out Monster monster)
    {
        monster = null;

        if (PlayerPrefs.HasKey(_monsterSaveName + position))
        {
            int index = PlayerPrefs.GetInt(_monsterSaveName + position);

            monster = _monsterList.Monsters[index];
            return true;
        }

        return false;
    }

    public void DeleteSave(int position)
    {
        PlayerPrefs.DeleteKey(_monsterSaveName + position);
    }
}
