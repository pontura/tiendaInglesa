using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public void OnScan()
    {
        Game.Instance.screensManager.Show(types.SCAN);
    }
    public override void OnShow()
    {
        base.OnShow();
        Data.Instance.winesData.Reset();
    }
}
