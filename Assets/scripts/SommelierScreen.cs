using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SommelierScreen : MainScreen
{
    [SerializeField] SommelierButton button;
    [SerializeField] CascadeButtons cascade;
    [SerializeField] Text field;

    [SerializeField] Scrollbar scrollBar;

    public List<string> historial;
    public List<string> texts;


    public int id;

    public override void OnShow()
    {
        id = 0;
        base.OnShow();
        historial.Clear();
        texts.Clear();
        SetOn("inicial");
        Events.ResetSearch();
    }
    public override void OnBack()
    {        
        if (id <= 0)
            base.OnBack();
        else
        {
            texts.Remove(texts[texts.Count - 1]);

            historial.Remove(historial[historial.Count - 1]);
            string lastToLoad = historial[historial.Count - 1];
            historial.Remove(lastToLoad);
            id--;         
            SetOn(lastToLoad);
        }
    }
    void SetOn(string questionID)
    {
        cascade.Init();
        historial.Add( questionID );
        SommelierData.Content content = Data.Instance.sommelierData.GetContent(questionID);
        field.text = content.question;
        foreach (SommelierData.RespuestasContent c in content.respuestas)
        {
            SommelierButton sb = Instantiate(button,cascade.transform);
            cascade.Add(sb.gameObject);
            sb.Init(this, c);

        }
        scrollBar.value = 1;
    }
    public void OnAnswer(SommelierData.RespuestasContent content)
    {
        texts.Add(content.text);
        if (content.titleID == null || content.titleID == "")
        {
            Data.Instance.sommelierData.SetActiveRespuesta(content, historial, texts);
            Game.Instance.screensManager.Show(types.LIST);
        }
        else
        {
            id++;
            SetOn(content.titleID);
        }
    }
}
