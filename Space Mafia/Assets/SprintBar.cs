using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxSprint(int sprintTime)
    {
        slider.maxValue = sprintTime;
        slider.value = sprintTime;

    }

    public void SetSprint(int sprintTime)
    {
        slider.value = sprintTime;
    }


}
