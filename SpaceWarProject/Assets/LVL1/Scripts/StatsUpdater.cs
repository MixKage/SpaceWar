using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
    // Update is called once per frame
    public Slider HpSlider;

    void Update()
    {
        if(InputManager.Instance._pl != null)
        {
            HpSlider.value = InputManager.Instance._pl.GetHpSliderValue();
        }
        else
        {
            HpSlider.value = 1;
        }
    }
}
