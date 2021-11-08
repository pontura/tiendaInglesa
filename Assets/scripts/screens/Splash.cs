using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MainScreen
{
    public void OnStart()
    {
        if (clicked) return;
        clicked = true;
        anim.Play("off");
        Invoke("Delayed", 1);
    }
    void Delayed()
    {
        clicked = false;
        Game.Instance.screensManager.Show(types.MAIN_MENU);
    }
}
