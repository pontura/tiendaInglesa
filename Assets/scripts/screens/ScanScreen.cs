using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanScreen : MainScreen
{
    public ScanSimple scanSimple;
    float lastReceivedInput = 0;
    float timeDelay = 0.25f;
    string currentCode = "";
    [SerializeField] Text barCodeField;
    [SerializeField] Text feedbackField;

    void Update()
    {
        if (Input.inputString != "")
        {
            barCodeField.text += Input.inputString;
            feedbackField.text = "Llegó del lector!";
        }

    }

    private void OnEnable()
    {
        Events.OnScanDone += OnScanDone;
    }
    private void OnDisable()
    {
        Events.OnScanDone -= OnScanDone;
    }
    void OnScanDone(string value)
    {
        Data.Instance.winesData.SetActive(value);
        OnScanReady();
    }
    public override void OnShow() {
        scanSimple.gameObject.SetActive(true);
        Invoke("InitDelayed", 1);
    }
    void InitDelayed()
    {
        scanSimple.ClickStart();
    }
    public void OnScanReady()
    {
        Game.Instance.screensManager.Show(types.RESULT);
    }
    public void OnList()
    {
        Game.Instance.screensManager.Show(types.LIST);
    }

}
