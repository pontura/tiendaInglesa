using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScreen : MainScreen
{
    public override void OnBack()
    {
        Game.Instance.screensManager.Show(MainScreen.types.MAIN_MENU);
    }

}
