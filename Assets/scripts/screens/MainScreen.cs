using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public types type;
    public enum types
    {
        SPLASH,
        SCAN,
        FILTERS,
        LIST,
        RESULT,
        SOMMELIER,
        QUESTIONS,
        MAIN_MENU
    }
    public virtual void OnBack()
    {
        Game.Instance.screensManager.Back();
    }
    public virtual void OnShow() { }
    public virtual void OnHide() { }

    public void Init()
    {
        
    }
    public void Show()
    {
        gameObject.SetActive(true);
        OnShow();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        OnHide();
    }

}
