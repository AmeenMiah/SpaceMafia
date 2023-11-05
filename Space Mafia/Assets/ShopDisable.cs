using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShopButton;
    public GameObject Shop;
    void Start()
    {
        ShopButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.WaveisOn == true)
        {
            ShopButton.SetActive(false);
            Shop.SetActive(false);
        }
        else
        {
            ShopButton.SetActive(true);
        }
    }
}
