using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public GameManager GM;
    public static string baseObjective = "Find the Rino - 3rd Space Mafia Commander";
    public Text ObjectiveText;

    // Start is called before the first frame update
    void Start()
    {

            ObjectiveText.text = "Objective: " + baseObjective;
            Invoke("Basic", 3f);


    }

    void Basic()
    {
        ObjectiveText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
