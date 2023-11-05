using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Percentage : MonoBehaviour
{
    public Slider slider;
    public Text CompletionText;
    public int Completion = 1;
    public GameManager GM;

    private void Start()
    {
        Completion = 0;
        slider.maxValue = 100;
       
        Invoke("CheckCompletion", 0.1f);
        Invoke("SetText", 0.5f);
    }

    void CheckCompletion()
    {
        if (GM.Level1Completed)
        {
            Completion += 10;
        }
        if (GM.Level2Completed)
        {
            Completion += 10;
        }
        if (GM.Level3Completed)
        {
            Completion += 10;
        }
        if (GM.Level4Completed)
        {
            Completion += 10;
        }

        if (GM.CollectibleLevel1)
        {
            Completion += 10;
        }
        if (GM.CollectibleLevel2)
        {
            Completion += 10;
        }
        if (GM.CollectibleLevel3)
        {
            Completion += 10;
        }
        if (GM.CollectibleLevel4)
        {
            Completion += 10;
        }
        if (GM.FinalBossDefeated)
        {
            Completion += 20;
        }    
    }

    void SetText()
    {
        CompletionText.text = Completion + "%";
        slider.value = Completion;
    }
}
