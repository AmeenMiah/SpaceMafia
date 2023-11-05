using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosHordeObjective : MonoBehaviour
{
    public Text Objective;
    void Start()
    {
        Objective.text = "Objective - Survive";
        Invoke("Remove", 3f);
    }

    void Remove()
    {
        Objective.text = "";
    }
}
