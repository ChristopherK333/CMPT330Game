using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void updateHealthBar(float currentValue)
    {
        // sets the value of the slider to the fixed health value
        slider.value = currentValue;
    }
}
