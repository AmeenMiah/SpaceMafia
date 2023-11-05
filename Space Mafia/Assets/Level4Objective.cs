using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Objective : MonoBehaviour
{
    public Text Objective;
    void Start()
    {
        Objective.text = "Objective - Defeat...";
        Invoke("Remove", 3f);
    }

    void Remove()
    {
        Objective.text = "";
    }
}
