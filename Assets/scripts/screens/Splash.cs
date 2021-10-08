using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MainScreen
{
    public void OnStart()
    {
        Game.Instance.screensManager.Show(types.MAIN_MENU);
    }
}
