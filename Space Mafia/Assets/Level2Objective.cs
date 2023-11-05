using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Objective : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Objective;
    void Start()
    {
        Objective.text = "Objective - Kill 10 Enemies";
        Invoke("Remove", 3f);
    }

    void Remove()
    {
        Objective.text = "";
    }
}
