using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MainScreen
{
    public Text nameField;
    public Text cepaField;
    public Text textField;
    public Text priceField;
    public Image image;

    public override void OnBack()
    {
        if(Game.Instance.screensManager.lastActiveScreen.type == types.LIST)
            Game.Instance.screensManager.Show(MainScreen.types.LIST);
        else
            Game.Instance.screensManager.Show(MainScreen.types.MAIN_MENU);
    }
    public override void OnHide()
    {
        image.sprite = null;
    }
    public override void OnShow()
    {
        base.OnShow();
        WinesData.Content active = Data.Instance.winesData.active;

        priceField.text = "$" + active.price;

        nameField.text = active.name;
        cepaField.text = active.cepa;

        textField.text += active.brand;
        textField.text += "\n<b>País</b>\n" + active.pais;
        if (active.p1>0)
            textField.text += "\n<b>Puntaje Descorchados:</b>\n" + active.p1;
        if (active.p2 > 0)
            textField.text += "\n<b>Puntaje Tim Atkin:</b>\n" + active.p2;
        if (active.p3 > 0)
            textField.text += "\n<b>Puntaje James Suckling:</b>\n" + active.p3;

        if (active.premios != null && active.premios != "")
            textField.text += "\n<b>Premios:</b>\n" + active.premios;

        textField.text += "\n" + active.text;
        StartCoroutine(Data.Instance.imagesLoader.C_LoadImage(active.id, 200, 200, OnLoaded, "large") );
    }
    void OnLoaded(Sprite sprite)
    {
        image.sprite = sprite;
        image.SetNativeSize();
    }
}
