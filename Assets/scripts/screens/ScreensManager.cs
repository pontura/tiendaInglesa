using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    [SerializeField] MainScreen activeScreen;
    public MainScreen lastActiveScreen;
    [SerializeField] MainScreen.types initialScreenType;
    [SerializeField] MainScreen[] screens;

    public void Init()
    {
        foreach (MainScreen ms in screens)
        {
            ms.Init();
            ms.Hide();
        }
    }
    public void Show(MainScreen.types type)
    {
        lastActiveScreen = activeScreen;
        foreach (MainScreen ms in screens)
        {
            if (ms.type == type)
            {
                activeScreen = ms;
                ms.Show();
            }
            else
                ms.Hide();
        }
            
    }
    public void Back()
    {
        if(lastActiveScreen != activeScreen)
        Show(lastActiveScreen.type);
    }
}
