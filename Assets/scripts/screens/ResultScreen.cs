using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MainScreen
{
    public Text nameField;
    public Text cepaField;
    public Text textField;
    public Image image;

    public override void OnBack()
    {
        Game.Instance.screensManager.Show(MainScreen.types.MAIN_MENU);
    }
    public override void OnShow()
    {
        base.OnShow();
        WinesData.Content active = Data.Instance.winesData.active;
        nameField.text = active.name;
        cepaField.text = active.cepa;
        textField.text = active.text;
    }

}
