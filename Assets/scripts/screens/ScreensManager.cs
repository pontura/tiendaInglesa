using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public MainScreen activeScreen;
    public MainScreen lastActiveScreen;
    [SerializeField] MainScreen.types initialScreenType;
    [SerializeField] MainScreen[] screens;
    public TimerToReset timerToReset;

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
                timerToReset.SetScreen(ms.type);
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
    float lastTimeScanned = 0;
    public string codebarReaded;
    float initScanningTime = 0;
    float timer = 0;
    bool scanning = false;
    string lastStringAdded = "";

    void Update()
    {
        if (lastTimeScanned != 0 && lastTimeScanned + 2 > Time.time)
            return;
        if (Input.inputString != "" )
        {
            if (initScanningTime == 0)
            {
                scanning = true;
                StartScanning();
            }
            if (!scanning)
                return;
            else if (Time.time > initScanningTime + 0.9f)
            {
                DoneScanning();
            }
            else
            {
                //if (lastStringAdded == Input.inputString)
                //    return;
                lastStringAdded = Input.inputString;
                codebarReaded += lastStringAdded;
                if(codebarReaded.Contains("\n"))
                    DoneScanning();
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) 
            OnScanReady("780433ss0006717");
#endif
    }
    void StartScanning()
    {
        initScanningTime = Time.time;
    }
    void DoneScanning()
    {
        lastStringAdded = "";
        lastTimeScanned = Time.time;
        scanning = false;
        initScanningTime = 0;

        codebarReaded = codebarReaded.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

        Events.OnScanDone(codebarReaded);
        OnScanReady(codebarReaded);
        Debug.Log("Llegó del lector: " + codebarReaded);
        codebarReaded = "";
    }
    public void OnScanReady(string codebar)
    {
        codebarReaded = codebar;
        Data.Instance.sommelierData.Reset();
        Data.Instance.winesData.SetActive(codebar);
        if(Data.Instance.winesData.active == null)
            Game.Instance.screensManager.Show(MainScreen.types.ERROR);
        else
            Game.Instance.screensManager.Show(MainScreen.types.RESULT);
    }
}
