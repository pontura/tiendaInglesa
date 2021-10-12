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
    }
}
