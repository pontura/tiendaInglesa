using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SommelierScreen : MainScreen
{
    [SerializeField] SommelierButton button;
    [SerializeField] Transform container;
    [SerializeField] Text field;
    public List<string> historial;
    public int id;

    public override void OnShow()
    {
        base.OnShow();
        historial.Clear();
        SetOn("inicial");
    }
    public override void OnBack()
    {        
        if (id <= 0)
            base.OnBack();
        else
        {
            id--;
            SetOn(historial[id]);
            historial.RemoveAt(historial.Count - 1);
        }
    }
    void SetOn(string questionID)
    {
        historial.Add( questionID );
        Utils.RemoveAllChildsIn(container);
        SommelierData.Content content = Data.Instance.sommelierData.GetContent(questionID);
        field.text = content.question;
        foreach (SommelierData.RespuestasContent c in content.respuestas)
        {
            SommelierButton sb = Instantiate(button, container);
            sb.Init(this, c);
        }
    }
    public void OnAnswer(SommelierData.RespuestasContent content)
    {
        if (content.titleID == null || content.titleID == "")
        {
            Game.Instance.screensManager.Show(types.LIST);
        }
        else
        {
            id++;
            SetOn(content.titleID);
        }
    }
}
