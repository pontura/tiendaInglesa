using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SommelierData : DataLoader
{
    public List<Content> content;
    public List<RespuestasContent> allActive;
    public AllFilters allFiltersActive; // los allActive pero sumados

    public bool loaded;
    public bool activeSommelierList;

    [Serializable]
    public class AllFilters
    {
        public int desde;
        public int hasta;
        public List<string> tags;
        public List<string> paises;
        public List<string> exclusivos;
        public List<string> cepas;
        public List<string> edad;
    }
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
                    string v = value.Replace(" ", "");
                    string[] arr = v.Split("-"[0]);
                    foreach (string s in arr)
                        SetPrice(rContent, s);
                }
                break;
            case 5:
                if (rContent != null)
                {
                    string v = value.Replace(" ", "").ToLower();
                    rContent.tags = v.Split(","[0]);
                }
                break;
            case 6:
                if (rContent != null)
                {
                    string v = value.Replace(" ", "").ToLower();
                    rContent.exclusivos = v.Split(","[0]);
                }
                break;
            case 7:
                if (rContent != null)
                {
                    string v = value.Replace(" ", "").ToLower();
                    rContent.paises = v.Split(","[0]);
                }
                break;
            case 8:
                if (rContent != null)
                {
                    value = value.Replace(", ", ",").ToLower();
                    string v = value;
                    rContent.cepas = v.Split(","[0]);
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
    public void SetActiveRespuesta(RespuestasContent rc, List<string> ids, List<string> texts)
    {
      //  print("________SetActiveRespuesta");
        allActive.Clear();
       // ids.RemoveAt(ids.Count-1); // borra el ultimo porque no tiene ID:
        int _id = 0;
        foreach (string id in ids)
        {
            string text = texts[_id];
            allActive.Add(GetRespuestaContentFor(id, text));
            _id++;
        }
        //allActive.Add(rc);
        activeSommelierList = true;
    }
    public override void Reset()
    {
        allActive.Clear();
        base.Reset();
        activeSommelierList = false;
    }
    RespuestasContent GetRespuestaContentFor(string id, string text)
    {
      // print("____Va a filtar id: " + id + " text: " + text);
        foreach (Content c in content)
        {
            if (c.id == id)
            {
                foreach (RespuestasContent rc in c.respuestas)
                {
                    if (rc.text == text)
                        return rc;
                }
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
    public void FilterBySommelier()
    {
        allFiltersActive = new AllFilters();
        allFiltersActive.cepas = new List<string>();
        allFiltersActive.paises = new List<string>();
        allFiltersActive.tags = new List<string>();
        allFiltersActive.exclusivos = new List<string>();
        allFiltersActive.edad = new List<string>();

        foreach (SommelierData.RespuestasContent active in allActive)
        {
            if (active.minPrice > 0)
                allFiltersActive.hasta = active.minPrice;
            if (active.maxPrice > 0)
                allFiltersActive.desde = active.maxPrice;
            if (active.cepas != null)
            {
                foreach(string s in active.cepas)
                    allFiltersActive.cepas.Add(s);
            }
            if (active.paises != null)
            {
                foreach (string s in active.paises)
                    allFiltersActive.paises.Add(s);
            }
            if (active.tags != null)
            {
                foreach (string s in active.tags)
                {
                    if(s == "joven" || s == "guarda")
                        allFiltersActive.edad.Add(s);
                    else
                        allFiltersActive.tags.Add(s);
                }
            }
            if (active.exclusivos != null)
            {
                foreach (string s in active.exclusivos)
                    allFiltersActive.exclusivos.Add(s);
            }
        }
       // print("1 ______" + Data.Instance.winesData.contentFiltered.Count);
        if (allFiltersActive.hasta > 0)
            Data.Instance.filtersData.AddFilter(WinesData.HASTA, allFiltersActive.hasta.ToString());
        if (allFiltersActive.desde > 0)
            Data.Instance.filtersData.AddFilter(WinesData.DESDE, allFiltersActive.desde.ToString());

      //  print("2 PRECIO ______" + Data.Instance.winesData.contentFiltered.Count);
        if (allFiltersActive.paises.Count > 0)
            Data.Instance.winesData.ApplySommelierFilter(WinesData.PAISES, allFiltersActive.paises);

       // print("3 PAISES______" + Data.Instance.winesData.contentFiltered.Count);
        if (allFiltersActive.cepas.Count > 0)
            Data.Instance.winesData.ApplySommelierFilter(WinesData.CEPAS, allFiltersActive.cepas);

      //  print("4 CEPAS______" + Data.Instance.winesData.contentFiltered.Count);
        if (allFiltersActive.tags.Count > 0)
            Data.Instance.winesData.ApplySommelierFilter(WinesData.TAGS, allFiltersActive.tags, true);

       // print("5 TAGS ______" + Data.Instance.winesData.contentFiltered.Count);

        if (allFiltersActive.edad.Count > 0)
            Data.Instance.winesData.ApplySommelierFilter(WinesData.EDAD, allFiltersActive.edad, true);

       // print("6 edad______" + Data.Instance.winesData.contentFiltered.Count);

    }

}
