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
    public void Init()
    {
        
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void OnFilters()
    {
        Game.Instance.screensManager.Show(MainScreen.types.FILTERS);
    }
    public void OnSommelier()
    {
        Game.Instance.screensManager.Show(MainScreen.types.SOMMELIER);
    }
}
