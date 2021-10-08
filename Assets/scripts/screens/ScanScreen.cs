using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanScreen : MainScreen
{
    public void OnScanReady()
    {
        Game.Instance.screensManager.Show(types.RESULT);
    }
    public void OnList()
    {
        Game.Instance.screensManager.Show(types.LIST);
    }

}
