using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MainScreen
{
    //public void OnScan()
    //{
    //    Game.Instance.screensManager.Show(types.SCAN);
    //}
    public override void OnShow()
    {
        base.OnShow();
        Data.Instance.winesData.Reset();
        Data.Instance.sommelierData.Reset();
        Events.ResetSearch();
    }
    public void OnFilters()
    {
        Game.Instance.screensManager.Show(MainScreen.types.LIST);
        Data.Instance.sommelierData.Reset();
    }
    public void OnSommelier()
    {
        Game.Instance.screensManager.Show(MainScreen.types.SOMMELIER);
        Data.Instance.sommelierData.Reset();
    }
}
