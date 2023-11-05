using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQDoor : MonoBehaviour
{
    public GameManager GM;
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < 8; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GM.Level3Complete();
        }
    }
}
