﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    const string PREFAB_PATH = "Data";
    static Data mInstance = null;
    public bool DEBUG;
    public string lastScene;
    public string newScene;

    public static Data Instance
    {
        get
        {
            return mInstance;
        }
    }
    public void LoadLevel(string aLevelName)
    {
        this.newScene = aLevelName;
         SceneManager.LoadScene(newScene);
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;

        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);

    }
}
