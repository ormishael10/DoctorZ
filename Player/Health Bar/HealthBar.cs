using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient grad;
    public Image fill;

    //set the current health we have on the player and update that on the slider
    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
        fill.color = grad.Evaluate(slider.normalizedValue); // update the color to the silder value
    }


    //set the max health that we want into the slider and update that
    public void SetMaxHealth(float currentHealth)
    {
        slider.maxValue = currentHealth;
        slider.value = currentHealth;

        fill.color = grad.Evaluate(1f);// green color cuz the grad is going from 0 to 1 like the slider
    }

}
