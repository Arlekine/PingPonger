using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [EditorButton]
    private void Save()
    {
        var data = new Data();

        data.Coins = 100;
        data.Score = 20;

        string json = JsonUtility.ToJson(data);
        print(json);

        PlayerPrefs.SetString("Data", json);
    }

    [EditorButton]
    private void Load()
    {
        var json = PlayerPrefs.GetString("Data");
        var data = JsonUtility.FromJson<Data>(json);
    }

    [Serializable]
    public class Data
    {
        public int Coins;
        public int Score;
        public int CurrentLevel;
        public string PlayerName;
    }
}
