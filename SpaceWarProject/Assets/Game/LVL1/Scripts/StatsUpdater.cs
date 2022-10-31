using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
    // Update is called once per frame
    public Slider HpSlider;
    public GameObject DeathScreen;

    private void FixedUpdate()
    {
        if(InputManager.Instance._pl != null)
        {
            HpSlider.value = InputManager.Instance._pl.GetHpSliderValue();
            
            //Код всрат
            //Привязка слайдеру вызывает подлагивание моей нейрвной системы
            if (HpSlider.value >= 1)
            {
                DeathScreen.SetActive(true);
            }
            else
            {
                DeathScreen.SetActive(false);
            }
        }
        else
        {
            HpSlider.value = 1;
        }
    }

    public void ReturnPlayerInGame()
    {
        InputManager.Instance._pl.ReturnPlayerInGame();
    }
}
