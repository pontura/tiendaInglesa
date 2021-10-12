﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string url;
    System.Action OnReady;

    public void LoadData(System.Action OnReady)
    {
        this.OnReady = OnReady;
        print("Load " + url);
        Data.Instance.spreadsheetLoader.LoadFromTo(url, OnLoaded);
    }
    public virtual void OnLoaded(List<SpreadsheetLoader.Line> d) {
        OnReady();
    }
    public virtual void Reset() { }

}
