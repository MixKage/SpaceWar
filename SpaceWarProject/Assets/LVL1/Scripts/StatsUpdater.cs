using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
    // Update is called once per frame
    public Slider HpSlider;
    public GameObject DyeInfoRespawn;

    void Update()
    {
        if(InputManager.Instance._pl != null)
        {
            HpSlider.value = InputManager.Instance._pl.GetHpSliderValue();
            if (HpSlider.value >= 1)
            {
                DyeInfoRespawn.SetActive(true);
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
        DyeInfoRespawn.SetActive(false);
    }
}
