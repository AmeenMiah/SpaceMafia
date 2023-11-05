using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterSet : MonoBehaviour
{
    public Text O;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        O.text = "Enemies Left to Kill: " + Level2Counter.EnemyCounter;
    }
}
