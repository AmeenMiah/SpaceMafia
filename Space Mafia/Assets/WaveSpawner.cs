using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public static int WaveNum = 0;
    public static bool WaveisOn = false;
    public static bool EndlessMode = true;
    public int[] Timer = { 0, 0, 0 };
    public string[] TimerDisplay = { "00", "00", "00" };
    public float milliseconds = 0;
    public static int Score = 0;
    public AudioManager Audio;
    public GameManager GM;
    public bool PlayerDecided = false;
    //Bosses
    public GameObject RinoBoss;
    public GameObject GhostBoss;
    public GameObject TimeBoss;
    public GameObject GodfatherBoss;
    public GameObject ChaosReaper;
    //Basic Enemies
    public GameObject EasyEnemy;
    public GameObject MediumEnemy;
    public GameObject HardEnemy;
    public GameObject MythicalEnemy;
    public GameObject PrimordialEnemy;
    //Spawn Locations
    public Transform SpawnLoc1;
    public Transform SpawnLoc2;
    public Transform SpawnLoc3;
    public Transform SpawnLoc4;
    public Transform SpawnLoc5;
    //Texts
    public Text WaveText;
    public Text BossWaveText;
    public Text ScoreText;
    public Text TimerText;
    public Text WaveCooldownTimerText;
    public GameObject SkipToWave50;
    //Overlays
    public GameObject DecideIfEndless;
    // Start is called before the first frame update
    void Start()
    {
        WaveNum = 0;
        WaveisOn = false;
        if (WaveisOn == false && GM.CollectibleLevel1 == true && GM.CollectibleLevel2 == true && GM.CollectibleLevel3 == true && GM.CollectibleLevel4 == true)
        {
            DecideIfEndless.SetActive(true);
            Time.timeScale = 0f;
            PauseMenu.GameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            if (GM.BestWaveNum > 49)
            {
                SkipToWave50.SetActive(true);
            }
            else
            {
                SkipToWave50.SetActive(false);
            }    
        }
        else
        {
            Time.timeScale = 1f;
            DecideIfEndless.SetActive(false);
            EndlessMode = true;
            PlayerDecided = true;
        }
        StartCoroutine(BeforeFirstWave());
        //Spawn1 = new Vector3(SpawnObject1.transform.position.x, SpawnObject1.transform.position.y, SpawnObject1.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDecided == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (EnemyWaveCounter.AmountofEnemies < 1 && WaveisOn == true)
        {
            WaveisOn = false;
            StartCoroutine(AfterWaveTimer());
        }
        milliseconds += Time.deltaTime;
        if (milliseconds > 1)
        {
            Timer[2] += 1;
            milliseconds = 0;
        }
        //Seconds to Minutes
        if (Timer[2] > 59)
        {
            Timer[1] += 1;
            Timer[2] = 0;
        }
        //Minutes to Hours
        if (Timer[1] > 59)
        {
            Timer[0] += 1;
            Timer[1] = 0;
        }
        
        if (Timer[2] > 99)
        {
            Timer[2] = 99;
        }

        if (Timer[0] < 10)
        {
            TimerDisplay[0] = "0" + Timer[0];
        }
        else
        {
            TimerDisplay[0] = Timer[0].ToString();
        }

        if (Timer[1] < 10)
        {
            TimerDisplay[1] = "0" + Timer[1];
        }
        else
        {
            TimerDisplay[1] = Timer[1].ToString();
        }
        if (Timer[2] < 10)
        {
            TimerDisplay[2] = "0" + Timer[2];
        }
        else
        {
            TimerDisplay[2] = Timer[2].ToString();
        }
        TimerText.text = "Timer: " + TimerDisplay[0] + ":" + TimerDisplay[1] + ":" + TimerDisplay[2];
        WaveText.text = "Wave: " + WaveNum;
        ScoreText.text = "Score: " + Score;
    }

    IEnumerator AfterWaveTimer()
    {
        int TimeBeforeNextWave = 10;
        for (int i = 0; TimeBeforeNextWave > -1; i++)
        {
            WaveCooldownTimerText.text = TimeBeforeNextWave.ToString();
            TimeBeforeNextWave -= 1;
            yield return new WaitForSeconds(1f);
        }
        NewWave();
    }

    IEnumerator BeforeFirstWave()
    {
        int TimeBeforeNextWave = 10;
        Score = 0;
        WaveCooldownTimerText.text = TimeBeforeNextWave.ToString();
        for (int i = 0; TimeBeforeNextWave > -1; i++)
        {
            WaveCooldownTimerText.text = TimeBeforeNextWave.ToString();
            TimeBeforeNextWave -= 1;
            yield return new WaitForSeconds(1f);
        }
        NewWave();
    }

    void NewWave()
    {
        WaveNum += 1;
        int SpawnWhichEnemy;
        int SpawnWhichLocation;
        Transform SpawnLocation;

        //Wave 1
        if (WaveNum == 1)
        {
            Instantiate(EasyEnemy, SpawnLoc1);
            Debug.Log("Hi");
            Invoke("TurnWaveOn", 1f);
        }

        //Wave 2 to 4 - Spawn Basic Enemies
        if (WaveNum > 1 && WaveNum < 6)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(1, 2);

                switch (SpawnWhichEnemy)
                {
                    case 1:
                        Instantiate(EasyEnemy, SpawnLocation);
                        break;
                    case 2:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(EasyEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);

        }

        if (WaveNum == 50 && EndlessMode == true)
        {
            ChaosReaper.SetActive(true);
            Invoke("ChaosReaperOn", 1f);
            return;
        }
        //Wave 10 - Spawn RinoBoss 
        if (WaveNum == 10)
        {
            GameObject R1 = Instantiate(RinoBoss, SpawnLoc1);
            R1.SetActive(true);
            Invoke("BossWaveOn", 1f);
        }

        //Wave 20 - Spawn GhostBoss 
        if (WaveNum == 20)
        {
            
            GameObject G1 = Instantiate(GhostBoss, SpawnLoc1);
            G1.SetActive(true);
            Invoke("BossWaveOn", 1f);
        }

        //Wave 30 - Spawn Time Boss 
        if (WaveNum == 30)
        {
            GameObject T1 = Instantiate(TimeBoss, SpawnLoc1);
            T1.SetActive(true);
            Invoke("BossWaveOn", 1f);
        }

        //Wave 40 - Spawn Godfather Boss 
        if (WaveNum == 40)
        {
            GameObject GF1 = Instantiate(GodfatherBoss, SpawnLoc1);
            GF1.SetActive(true);
            Invoke("BossWaveOn", 1f);
        }

        if (WaveNum % 10 == 0 && WaveNum > 49)
        {
            Debug.Log("Success");
            SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

            switch (SpawnWhichLocation)
            {
                case 1:
                    SpawnLocation = SpawnLoc1;
                    break;
                case 2:
                    SpawnLocation = SpawnLoc2;
                    break;
                case 3:
                    SpawnLocation = SpawnLoc3;
                    break;
                case 4:
                    SpawnLocation = SpawnLoc4;
                    break;
                case 5:
                    SpawnLocation = SpawnLoc5;
                    break;
                default:
                    SpawnLocation = SpawnLoc1;
                    break;

            }

            //Randomly decide which enemy to spawn
            SpawnWhichEnemy = UnityEngine.Random.Range(1, 4);

            switch (SpawnWhichEnemy)
            {
                case 1:
                    GameObject R1 = Instantiate(RinoBoss, SpawnLoc1);
                    R1.SetActive(true);
                    break;
                case 2:
                    GameObject G1 = Instantiate(GhostBoss, SpawnLoc1);
                    G1.SetActive(true);
                    break;
                case 3:
                    GameObject T1 = Instantiate(TimeBoss, SpawnLoc1);
                    T1.SetActive(true);
                    break;
                case 4:
                    GameObject GF1 = Instantiate(GodfatherBoss, SpawnLoc1);
                    GF1.SetActive(true);
                    break;
                default:
                    GameObject GF2 = Instantiate(GodfatherBoss, SpawnLoc1);
                    GF2.SetActive(true);
                    break;
            }

            Invoke("BossWaveOn", 1f);
            return;
        }

        //Wave 6 to 9 - Spawn Basic Enemies
        if (WaveNum > 5 && WaveNum < 10)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(1, 3);

                switch (SpawnWhichEnemy)
                {
                    case 1:
                        Instantiate(EasyEnemy, SpawnLocation);
                        break;
                    case 2:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                    case 3:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);



        }
        //Wave 11 - 19 
        if (WaveNum > 10 && WaveNum < 20)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(2, 3);

                switch (SpawnWhichEnemy)
                {
                    case 2:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                    case 3:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);
        }
        //Wave 20 - 29 
        if (WaveNum > 20 && WaveNum < 30)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(2, 4);

                switch (SpawnWhichEnemy)
                {
                    case 2:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                    case 3:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                    case 4:
                        Instantiate(MythicalEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(MediumEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);
        }
        //Wave 30 - 39 
        if (WaveNum > 30 && WaveNum < 40)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(3, 4);

                switch (SpawnWhichEnemy)
                {
                    case 3:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                    case 4:
                        Instantiate(MythicalEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);
        }
        //Wave 40 - 49 
        if (WaveNum > 40 && WaveNum < 50)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(3, 5);

                switch (SpawnWhichEnemy)
                {
                    case 3:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                    case 4:
                        Instantiate(MythicalEnemy, SpawnLocation);
                        break;
                    case 5:
                        Instantiate(PrimordialEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(HardEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);
        }
        //Wave 50 - 99
        if (WaveNum > 50 && WaveNum < 100)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                //Randomly decide which enemy to spawn
                SpawnWhichEnemy = UnityEngine.Random.Range(4, 5);

                switch (SpawnWhichEnemy)
                {
                    case 4:
                        Instantiate(MythicalEnemy, SpawnLocation);
                        break;
                    case 5:
                        Instantiate(PrimordialEnemy, SpawnLocation);
                        break;
                    default:
                        Instantiate(MythicalEnemy, SpawnLocation);
                        break;
                }
            }
            Invoke("TurnWaveOn", 1f);
        }
        //Wave 100+
        if (WaveNum > 100)
        {
            for (int i = 0; i < WaveNum; i++)
            {
                //Randomly decide the enemy spawn location
                SpawnWhichLocation = UnityEngine.Random.Range(1, 5);

                switch (SpawnWhichLocation)
                {
                    case 1:
                        SpawnLocation = SpawnLoc1;
                        break;
                    case 2:
                        SpawnLocation = SpawnLoc2;
                        break;
                    case 3:
                        SpawnLocation = SpawnLoc3;
                        break;
                    case 4:
                        SpawnLocation = SpawnLoc4;
                        break;
                    case 5:
                        SpawnLocation = SpawnLoc5;
                        break;
                    default:
                        SpawnLocation = SpawnLoc1;
                        break;

                }

                Instantiate(PrimordialEnemy, SpawnLocation);
                
            }
            Invoke("TurnWaveOn", 1f);
        }
    }

    void TurnWaveOn()
    {
        WaveisOn = true;
        BossWaveText.text = "Wave: " + WaveNum;
        Audio.Play("WaveStart");
        Invoke("TurnOffText", 2f);
    }

    void BossWaveOn()
    {
        WaveisOn = true;
        BossWaveText.text = "Boss Wave!";
        Audio.Play("WaveStart");
        Invoke("TurnOffText", 2f);
    }

    void ChaosReaperOn()
    {
        WaveisOn = true;
        BossWaveText.text = "The Final Fight";
        Audio.Play("WaveStart");
        Invoke("TurnOffText", 2f);
    }

    void TurnOffText()
    {
        BossWaveText.text = "";
    }

    public void EndlessButton()
    {
        PlayerDecided = true;
        DecideIfEndless.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        EndlessMode = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void NonEndlessButton()
    {
        PlayerDecided = true;
        DecideIfEndless.SetActive(false);
        Time.timeScale = 1f;
        EndlessMode = true;
        PauseMenu.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NonEndlessButtonSkip()
    {
        PlayerDecided = true;
        DecideIfEndless.SetActive(false);
        Time.timeScale = 1f;
        EndlessMode = true;
        PauseMenu.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.None;
        WaveNum = 49;
        Score = 3000;
    }
}
