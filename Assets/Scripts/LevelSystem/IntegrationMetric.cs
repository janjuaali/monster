using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IntegrationMetric
{
    private const string SessionCountName = "sessionCount";
    private const string _regDay = "regDay";

    private string _profileId;
    private const string ProfileId = "ProfileId";
    private const int ProfileIdLength = 10;

    public int SessionCount;

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());

       
          }

    public void OnLevelStart(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);

           }

    public void OnLevelComplete(int levelComplitioTime, int levelIndex, int amountCollected, int amountInWallet)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelComplitioTime }, { "soft_lvl", amountCollected }, { "soft_all", amountInWallet } };

            }

    public void OnLevelFail(int levelFailTime, int levelIndex, string lostCouse)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime }, { "reason", lostCouse } };

             }

    public void OnRestartLevel(int levelIndex)
    {
        var levelProperty = CreateLevelProperty(levelIndex);

         }

    public void OnSoftCurrencySpend(string type, string name, int currencySpend)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "type", type }, { "name", name }, {"amount", currencySpend } };

           }

    public void OnAbiltyUsed(string name)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "name", name } };

          }

    
    

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(ProfileId))
        {
            _profileId = PlayerPrefs.GetString(ProfileId);
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(ProfileId, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        var random = new System.Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }

    private Dictionary<string, object> CreateLevelProperty( int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(SessionCountName))
        {
            count = PlayerPrefs.GetInt(SessionCountName);
            count++;
        }

        PlayerPrefs.SetInt(SessionCountName, count);
        SessionCount = count;

        return count;
    }
}
