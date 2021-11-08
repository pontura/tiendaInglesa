using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public MainScreen activeScreen;
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

    float lastBarcodeTimer = 0;
    void Update()
    {
        if (Input.inputString != "" && (lastBarcodeTimer == 0 || lastBarcodeTimer+1>Time.time))
        {
            lastBarcodeTimer = Time.time;
            string value = Input.inputString;
            value = value.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            Events.OnScanDone(value);
            OnScanReady(value);
            Debug.Log( "Llegó del lector: " + value);
        }
    }
    public void OnScanReady(string codebar)
    {
        Data.Instance.sommelierData.Reset();
        Data.Instance.winesData.SetActive(codebar);
        Game.Instance.screensManager.Show(MainScreen.types.RESULT);
    }
}
