using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SommelierButton : MonoBehaviour
{
    [SerializeField] Text field;
    SommelierScreen ui;
    SommelierData.RespuestasContent content;
    public void Init(SommelierScreen ui, SommelierData.RespuestasContent content)
    {
        this.content = content;
        this.ui = ui;
        field.text = content.text;
    }
    public void Clicked()
    {
        ui.OnAnswer(content);
    }
}
