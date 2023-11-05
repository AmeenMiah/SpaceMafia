using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxHealth(float playerHealth)
    {
        slider.maxValue = playerHealth;
        slider.value = playerHealth;

    }
    

    public void SetHealth(float playerHealth)
    {
        slider.value = playerHealth;
    }
}
