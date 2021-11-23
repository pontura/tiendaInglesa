using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorScreen : MainScreen
{
    public Text field;

    public override void OnShow()
    {
        base.OnShow();
        field.text = "Lamentablemente no encontramos este producto: (" + Game.Instance.screensManager.codebarReaded + ")";
    }
    public override void OnBack()
    {
        Game.Instance.screensManager.Show(MainScreen.types.MAIN_MENU);
    }
}
