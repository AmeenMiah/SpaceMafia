using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPopUp : MonoBehaviour
{
    public bool CreditsUp = false;
    public GameObject Credits;
    public AudioManager Audio;
    // Start is called before the first frame update
    void Start()
    {
        Credits.SetActive(false);
        Audio = FindObjectOfType<AudioManager>();
    }

    public void CreditsPop()
    {
        switch (CreditsUp)
        {
            case true:
                Audio.Play("Success");
                Credits.SetActive(false);
                CreditsUp = false;
                break;
            case false:
                Audio.Play("Success");
                Credits.SetActive(true);
                CreditsUp = true;
                break;
            default:
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
