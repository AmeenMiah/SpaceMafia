using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenBuffEnable : MonoBehaviour
{
    public Text GoldenBuffText;
    public Text GoldenBuffCostText;
    public GameObject GoldenBuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.EndlessMode == true && WaveSpawner.WaveNum > 45)
        {
            GoldenBuff.SetActive(true);
            
            GoldenBuffCostText.gameObject.SetActive(true);
            GoldenBuffText.gameObject.SetActive(true);
        }
        else
        {
            GoldenBuff.SetActive(false);
            GoldenBuffCostText.gameObject.SetActive(false);
            GoldenBuffText.gameObject.SetActive(false);
        }
    }
}
