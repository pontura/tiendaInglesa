using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SommelierData : DataLoader
{
    public List<Content> content;

    public bool loaded;

    [Serializable]
    public class Content
    {
        public string id;
        public string question;
        public List<RespuestasContent> respuestas;
    }
    [Serializable]
    public class RespuestasContent
    {
        public string text;
        public string titleID;
        public List<string> tags;
        public int minPrice;
        public int maxPrice;
    }
    private void Start()
    {
        LoadData(null);
    }

    public override void OnLoaded(List<SpreadsheetLoader.Line> d)
    {
        OnDataLoaded(content, d);
        loaded = true;
    }
    Content contentLine = null;
    void OnDataLoaded(List<Content> content, List<SpreadsheetLoader.Line> d)
    {
        int colID = 0;
        int rowID = 0;
        
        foreach (SpreadsheetLoader.Line line in d)
        {
            foreach (string value in line.data)
            {
                if (rowID >= 1)
                {
                    if (colID == 0)
                    {
                        if (value != "")
                        {
                            contentLine = new Content();
                            content.Add(contentLine);
                            contentLine.id = value;
                            contentLine.respuestas = new List<RespuestasContent>();
                        }
                    }
                    else if (value != "")
                        Add(colID, value);
                }
                colID++;
            }
            colID = 0;
            rowID++;
        }
    }
    RespuestasContent rContent;
    void Add( int colID, string value)
    {
        switch (colID)
        {
            case 1:
                contentLine.question = value;
                break;
            case 2:
                rContent = new RespuestasContent();
                rContent.text = value;
                contentLine.respuestas.Add(rContent);
                break;
            case 3:
                if (rContent != null)
                    rContent.titleID = value;
                break;
        }
    }
    public SommelierData.Content GetContent(string id)
    {
        foreach (Content c in content)
            if (c.id == id)
                return c;
        return null;
    }


}
