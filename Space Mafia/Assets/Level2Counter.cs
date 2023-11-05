using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Counter : MonoBehaviour
{
    // Start is called before the first frame update
    public static int EnemyCounter = 10;
    public GameObject Ghost;
    void Start()
    {
        EnemyCounter = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(EnemyCounter);
        if (EnemyCounter <= 0)
        {
            Ghost.SetActive(true);
        }
    }

    


}
