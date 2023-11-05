using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Objective : MonoBehaviour
{
    public Text Objective;
    void Start()
    {
        Objective.text = "Objective - Find the Key";
        Invoke("Remove", 3f);
    }

    public void Key()
    {
        Objective.text = "You got a key! Head to the Space Mafia HQ";
        Invoke("Remove", 3f);
    }

    void Remove()
    {
        Objective.text = "";
    }
}
