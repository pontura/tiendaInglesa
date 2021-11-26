using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string url;
    System.Action OnReady;
    public types type;
    public enum types
    {
        TSV,
        CSV
    }

    public void LoadData(System.Action OnReady)
    {
        this.OnReady = OnReady;
        print("Load " +this + url + type);
        Data.Instance.spreadsheetLoader.LoadFromTo(url, OnLoaded, type);
    }
    public virtual void OnLoaded(List<SpreadsheetLoader.Line> d) {
        OnReady();
    }
    public virtual void Reset() { }

}
