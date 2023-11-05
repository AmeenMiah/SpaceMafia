using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBestScore : MonoBehaviour
{
    public GameManager GM;
    public Text Score;
    // Start is called before the first frame update
    void Start()
    {
        GM.LoadFunction();
        Score.text = "Best Game" + System.Environment.NewLine + "Wave: " + GM.BestWaveNum + System.Environment.NewLine + "Score: " + GM.BestScore + System.Environment.NewLine + "Time: " + GM.BestTime[0] + ":" + GM.BestTime[1] + ":" + GM.BestTime[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
