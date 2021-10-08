using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    public void OnScan()
    {
        Game.Instance.screensManager.Show(types.SCAN);
    }
}
