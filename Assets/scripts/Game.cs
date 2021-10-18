using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    static Game mInstance = null;
    public ScreensManager screensManager;
    public static Game Instance {  get {  return mInstance;  } }

    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    private void Start()
    {
        screensManager.Init();
        CheckAllDataLoaded();
    }
    void CheckAllDataLoaded()
    {
        if (Data.Instance.winesData.loaded)
            Init();
        else Invoke("CheckAllDataLoaded", 0.5f);
    }
    private void Init()
    {
        screensManager.Show(MainScreen.types.SPLASH);
    }
}
