using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToReset : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float time_to_reset;

    void Start()
    {
        Loop();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            timer = 0;
    }
    public void SetScreen(MainScreen.types type)
    {
        timer = 0;
        switch (type)
        {
            case MainScreen.types.ERROR:
                time_to_reset = 6;
                break;
            case MainScreen.types.RESULT:
                time_to_reset = 70;
                break;
            case MainScreen.types.MAIN_MENU:
                time_to_reset = 15;
                break;
            case MainScreen.types.SPLASH:
                time_to_reset = 10000;
                break;
            default:
                time_to_reset = 40;
                break;
        }
    }
    void Loop()
    {
        timer++;
        Invoke("Loop", 1);
        if(timer > time_to_reset)
        {
            timer = 0;
            if(Game.Instance.screensManager.activeScreen.type  !=  MainScreen.types.SPLASH)
                Game.Instance.screensManager.Show(MainScreen.types.SPLASH);
        }
    }
}
