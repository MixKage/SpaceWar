using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class SetActiveButton : MonoBehaviour
{
    // Код обеспечивает смену активных кнопок по нажатию на Tab
    // Так же настроил базовую реализацию юнити, но меня она не устроила
    public List<GameObject> uiElementsWindow1 = new List<GameObject>();
    public List<GameObject> uIElementsWindow2 = new List<GameObject>();
    private bool _isFirstMenu = true;
    private int _indexTab = 0;
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        // Если прожат Tab
        if (_isFirstMenu)
        {
            if (_indexTab > uiElementsWindow1.Count - 1)
            {
                _indexTab = 0;
            }
            EventSystem.current.SetSelectedGameObject(
                uiElementsWindow1[_indexTab]);
        }
        else
        {
            if (_indexTab > uIElementsWindow2.Count - 1)
            {
                _indexTab = 0;
            }
            EventSystem.current.SetSelectedGameObject(
                uIElementsWindow2[_indexTab]);
        }
        _indexTab++;
    }

    public void ChangeTabWindow(bool isFirstMenu)
    {
        _isFirstMenu = isFirstMenu;
        _indexTab = 0;
    }
}
