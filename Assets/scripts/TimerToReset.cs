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
