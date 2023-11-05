using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public GameManager GM;
    // Start is called before the first frame update
    void Awake()
    {
        GM = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        if (GM.CollectibleLevel1 == true && SceneManager.GetActiveScene().buildIndex == 2)
        {
            Destroy(gameObject);
            return;
        }
        else if (GM.CollectibleLevel2 == true && SceneManager.GetActiveScene().buildIndex == 4)
        {
            Destroy(gameObject);
            return;
        }
        else if(GM.CollectibleLevel3 == true && SceneManager.GetActiveScene().buildIndex == 6)
        {
            Destroy(gameObject);
            return;
        }
        else if (GM.CollectibleLevel4 == true && SceneManager.GetActiveScene().buildIndex == 7)
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
