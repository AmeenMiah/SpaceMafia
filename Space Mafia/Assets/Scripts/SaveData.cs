using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool Level1Completed = false;
    public bool Level2Completed = false;
    public bool Level3Completed = false;
    public bool Level4Completed = false;
    public bool Level5Completed = false;
    public int BestWaveNum;
    public bool CollectibleLevel1 = false;
    public bool CollectibleLevel2 = false;
    public bool CollectibleLevel3 = false;
    public bool CollectibleLevel4 = false;
    public bool FinalBossDefeated = false;
    public string[] BestTime = { "00", "00", "00" };
    public int BestScore;


    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json)
    {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}
