using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    bool gameHasEnded = false;
    public float reloadTime = 2f;
    public GameObject completeLevelUI;
    public GameObject Player;
    public bool Level1Completed = false;
    public bool Level2Completed = false;
    public bool Level3Completed = false;
    public bool Level4Completed = false;
    public bool Level5Completed = false;
    public bool CollectibleLevel1 = false;
    public bool CollectibleLevel2 = false;
    public bool CollectibleLevel3 = false;
    public bool CollectibleLevel4 = false;
    public int BestWaveNum;
    public bool FinalBossDefeated = false;
    public WaveSpawner WaveManager;
    public GameObject DeathParticle;
    public GameObject ChaosHordeGameOverOverlay;
    public string[] BestTime = { "00", "00", "00" };
    public int BestScore;


    public Text Objective;

    private void Awake()
    {
        LoadFunction();
        if (ChaosHordeGameOverOverlay != null)
        {
            ChaosHordeGameOverOverlay.SetActive(false);
        }

    }
    
    private static void SaveJsonData(GameManager a_GameManager)
    {
        SaveData sd = new SaveData();
        a_GameManager.PopulateSaveData(sd);
        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save Successful");
        }

    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.BestScore = BestScore;
        a_SaveData.BestTime = BestTime;
        a_SaveData.BestWaveNum = BestWaveNum;
        if (Level1Completed)
        {
            a_SaveData.Level1Completed = Level1Completed;
        }
        if (Level2Completed)
        {
            a_SaveData.Level2Completed = Level2Completed;
        }
        if (Level3Completed)
        {
            a_SaveData.Level3Completed = Level3Completed;
        }
        if (Level4Completed)
        {
            a_SaveData.Level4Completed = Level4Completed;
        }
        if(CollectibleLevel1)
        {
            a_SaveData.CollectibleLevel1 = CollectibleLevel1;
        }
        if (CollectibleLevel2)
        {
            a_SaveData.CollectibleLevel2 = CollectibleLevel2;
        }
        if (CollectibleLevel3)
        {
            a_SaveData.CollectibleLevel3 = CollectibleLevel3;
        }
        if (CollectibleLevel4)
        {
            a_SaveData.CollectibleLevel4 = CollectibleLevel4;
        }
        if (FinalBossDefeated)
        {
            a_SaveData.FinalBossDefeated = FinalBossDefeated;
        }

    }

    private static void LoadJsonData(GameManager a_GameManager)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            a_GameManager.LoadFromSaveData(sd);
            Debug.Log("load Successful");
        }

    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        BestScore = a_SaveData.BestScore;
        BestTime = a_SaveData.BestTime;
        BestWaveNum = a_SaveData.BestWaveNum;
        Level1Completed = a_SaveData.Level1Completed;
        Level2Completed = a_SaveData.Level2Completed;
        Level3Completed = a_SaveData.Level3Completed;
        Level4Completed = a_SaveData.Level4Completed;
        CollectibleLevel1 = a_SaveData.CollectibleLevel1;
        CollectibleLevel2 = a_SaveData.CollectibleLevel2;
        CollectibleLevel3 = a_SaveData.CollectibleLevel3;
        CollectibleLevel4 = a_SaveData.CollectibleLevel4;
        FinalBossDefeated = a_SaveData.FinalBossDefeated;
    }

    public void FinalBossComplete()
    {
        FinalBossDefeated = true;
        SaveJsonData(this);
        Invoke("LoadNextLevel", 1f);
    }

    public void Level1Complete()
    {
        Level1Completed = true;
        SaveJsonData(this);
        Invoke("LoadNextLevel", 1f);

        StartCoroutine(Text1());
    }

    public void Level2Complete()
    {
        Level2Completed = true;
        SaveJsonData(this);
        Cursor.lockState = CursorLockMode.None;
        Invoke("LoadNextLevel", 1f);

    }

    public void Level3Complete()
    {
        Level3Completed = true;
        SaveJsonData(this);
        Invoke("LoadNextLevel", 1f);
    }

    public void Level4Complete()
    {
        Level4Completed = true;
        SaveJsonData(this);
        Invoke("LoadNextLevel", 1f);

    }

    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
            Invoke("Restart", reloadTime);
        }
        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadFunction()
    {
        LoadJsonData(this);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Text1()
    {
        Objective.text = "Kill 9 Enemies";
        yield return new WaitForSeconds(2f);
        Objective.text = "";
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            Level1Completed = data.Level1Completed;
            Level2Completed = data.Level2Completed;
            Level3Completed = data.Level3Completed;
            Level4Completed = data.Level4Completed;
            Level5Completed = data.Level5Completed;
        }



    }

    public void EnemyDeath(Transform enemytransform)
    {
        Vector3 enemyPosition = new Vector3 (enemytransform.position.x, enemytransform.position.y, enemytransform.position.z);
        GameObject Death = Instantiate(DeathParticle, enemyPosition, enemytransform.rotation);
        Destroy(Death, 1f);
    }

    public void ChaosHordeGameOver()
    {

        Time.timeScale = 0f;
        ChaosHordeGameOverOverlay.SetActive(true);
        PauseMenu.GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        if (WaveSpawner.WaveNum > BestWaveNum)
        {
            BestWaveNum = WaveSpawner.WaveNum;
            BestScore = WaveSpawner.Score;
            BestTime = WaveManager.TimerDisplay;
            SaveJsonData(this);

        }

        if (WaveSpawner.WaveNum == BestWaveNum)
        {
            if (WaveSpawner.Score > BestScore)
            {
                BestScore = WaveSpawner.Score;
                BestTime = WaveManager.TimerDisplay;
                SaveJsonData(this);
            }
        }





    }

    public void ChaosHordeRestrat()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Time.timeScale = 1f;
            ChaosHordeGameOverOverlay.SetActive(false);
            Debug.Log("Game Over");
            Restart2();
            Invoke("Restart2", reloadTime);
        }
        void Restart2()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChaosHordeLoadMenu()
    {
        Time.timeScale = 1f;
        ChaosHordeGameOverOverlay.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }

    
}
