using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headshot : MonoBehaviour
{
    public Godfather GF;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TakeDamage(float amount)
    {

        switch (GF.rageMode)
        {
            case true:
                GF.health -= amount/2;
                break;
            case false:
                GF.health -= amount;
                break;
            default:
                GF.health -= amount/2;
                break;
        }

    }

}
