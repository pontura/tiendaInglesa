using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SommelierData : DataLoader
{
    public List<Content> content;
    public List<RespuestasContent> allActive;

    public bool loaded;
    public bool activeSommelierList;

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
        public int minPrice;
        public int maxPrice;
        public string[] tags;
        public string[] paises;
        public string[] exclusivos;
        public string[] cepas;
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
            case 4:
                if (rContent != null)
                {
                    string v = value;
                    string[] arr = value.Split("-"[0]);
                    foreach (string s in arr)
                        SetPrice(rContent, s);
                }
                break;
            case 5:
                if (rContent != null)
                {
                    string v = value;
                    rContent.tags = value.Split(","[0]);
                }
                break;
            case 6:
                if (rContent != null)
                {
                    string v = value;
                    rContent.exclusivos = value.Split(","[0]);
                }
                break;
            case 7:
                if (rContent != null)
                {
                    string v = value;
                    rContent.tags = value.Split(","[0]);
                }
                break;
            case 8:
                if (rContent != null)
                {
                    string v = value;
                    rContent.cepas = value.Split(","[0]);
                }
                break;
        }
    }
    void SetPrice(RespuestasContent rc, string value)
    {
        if (value.Contains(">"))
            rc.maxPrice = int.Parse(value.Replace(">", ""));
        else
            rc.minPrice = int.Parse(value.Replace("<", ""));
    }
    public SommelierData.Content GetContent(string id)
    {
        foreach (Content c in content)
            if (c.id == id)
                return c;
        return null;
    }
    public void SetActiveRespuesta(RespuestasContent rc, List<string> others)
    {
        allActive.Clear();
        others.RemoveAt(others.Count-1); // borra el ultimo porque no tiene ID:
       
        foreach (string s in others)
        {
            print("____Va a filtar: " + s);
            allActive.Add(GetRespuestaContentFor(s));
        }
        allActive.Add(rc);
        activeSommelierList = true;
    }
    public override void Reset()
    {
        allActive.Clear();
        base.Reset();
        activeSommelierList = false;
    }
    RespuestasContent GetRespuestaContentFor(string s)
    {
        foreach(Content c in content)
        {
            foreach (RespuestasContent rc in c.respuestas)
            {
                if (c.id == s)
                    return rc;
            }
        }
        return null;
    }
    public List<string> GetAllExlusives()
    {
        List<string> all = new List<string>();
        foreach (RespuestasContent rc in allActive)
        {
            if (rc.exclusivos != null && rc.exclusivos.Length > 0)
            {
                foreach (string s in rc.exclusivos)
                    all.Add(s);
            }
        }
        return all;
    }

}
