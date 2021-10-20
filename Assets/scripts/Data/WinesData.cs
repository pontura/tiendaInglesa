using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinesData : DataLoader
{
    public string listadoURL;
    public string winesTexts;

    public List<Content> content;
    public List<Content> contentFiltered;

    public Content active;
    public static string CEPAS = "Cepa";
    public static string BRANDS = "Marca";
    public static string PAISES = "País";

    public static string DESDE = "$ Desde";
    public static string HASTA = "$ Hasta";
    public bool loaded;
    FiltersData filtersData;


    [Serializable]
    public class Content
    {
        public string id;
        public string codebar;
        public string name;
        public int price;
        public string brand;
        public List<string> tags;
        public string pais;
        public string cepa;
        public List<string> maridaje;
        public string tiempo_guardia;
        public string temp;
        public string text;

        public int p1;
        public int p2;
        public int p3;
        public string premios;
    }
    private void Awake()
    {
        filtersData = GetComponent<FiltersData>();
    }
    private void Start()
    {
        Data.Instance.filtersData.Init();

        LoadData(null);
    }
   
    public override void OnLoaded(List<SpreadsheetLoader.Line> d)
    {
        OnDataLoaded(content, d);
        LoadListadoData();
    }
    public void LoadListadoData()
    {
        Data.Instance.spreadsheetLoader.LoadFromTo(listadoURL, OnListadoLoaded);
    }
    void OnListadoLoaded(List<SpreadsheetLoader.Line> d) 
    {
        OnDataListadoLoaded(d);
        Data.Instance.spreadsheetLoader.LoadFromTo(winesTexts, OnWinesTextsLoaded);
    }
    void OnWinesTextsLoaded(List<SpreadsheetLoader.Line> d)
    {
        OnDataWinesTextsLoaded(d);
        ResetFilters();
        loaded = true;
    }
    public void ResetFilters()
    {
        if (contentFiltered.Count == content.Count) return;

        contentFiltered.Clear();
        foreach (Content c in content)
            contentFiltered.Add(c);
    }
    public override void Reset()
    {
        if (contentFiltered.Count == content.Count) return;
        Data.Instance.filtersData.Reset();
        contentFiltered.Clear();
        foreach (Content c in content)
            contentFiltered.Add(c);
    }
    void OnDataLoaded(List<Content> content, List<SpreadsheetLoader.Line> d)
    {
        int colID = 0;
        int rowID = 0;
        Content contentLine = null;
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
                            contentLine.tags = new List<string>();
                            contentLine.maridaje = new List<string>();
                        }
                    }
                    else if(value != "")
                        Add(contentLine, colID, value.ToLower());
                }
                colID++;
            }
            colID = 0;
            rowID++;
        }
    }
    void Add(Content contentLine, int colID, string value)
    {
        switch (colID)
        {
            case 1: contentLine.id = value; break;
            case 2: contentLine.name = value; break;
            case 3: contentLine.codebar = value; break;
            case 4:
                string r = value;
                string[] arrs = value.Split("."[0]);
                if (arrs.Length > 1)
                    r = arrs[0];
                contentLine.price = int.Parse(r);
                break;
            case 5: contentLine.brand = value; CheckForNewFilter(BRANDS, value);  break;
            case 9: contentLine.cepa = value.ToLower(); CheckForNewFilter(CEPAS, value.ToLower());break;
            case 12:
                string[] tagsArr = value.Split(","[0]);
                foreach (string s in tagsArr)
                    contentLine.tags.Add(s);
                break;
            case 13: contentLine.pais = value; CheckForNewFilter(PAISES, value.ToLower()); break;
            case 14: contentLine.cepa = value.ToLower(); CheckForNewFilter(CEPAS, value.ToLower()); break;
            case 15:
                string[] mArr = value.Split(","[0]);
                foreach (string s in mArr)
                    contentLine.maridaje.Add(s);
                break;
        }
    }






    void OnDataListadoLoaded(List<SpreadsheetLoader.Line> d)
    {
        int colID = 0;
        int rowID = 0;
        Content contentLine = null;
        foreach (SpreadsheetLoader.Line line in d)
        {
            foreach (string value in line.data)
            {
                if (rowID >= 1)
                {
                    if (value != "")
                    {
                        if (colID == 0)
                        {
                            contentLine = GetSpecificWine(value);
                        }
                        else if (contentLine != null) {
                            if (colID == 3)
                            {
                                contentLine.cepa = value.ToLower();
                                CheckForNewFilter(CEPAS, value.ToLower());
                            }
                            else if (colID == 4)
                            {
                                contentLine.tags.Add(value);
                            }
                            else if (colID == 5)
                            {
                                contentLine.pais = value;
                            }
                            else if (colID == 6)
                            {
                                contentLine.tiempo_guardia = value;
                            }
                            else if (colID == 7)
                            {
                                contentLine.temp = value;
                            }
                            else if (colID == 8)
                            {
                                contentLine.p1 = int.Parse( value);
                            }
                            else if (colID == 7)
                            {
                                contentLine.p2 = int.Parse(value);
                            }
                            else if (colID == 10)
                            {
                                contentLine.p3 = int.Parse(value);
                            }
                            else if (colID == 11)
                            {
                                contentLine.premios = value;
                            }
                        }
                    }
                }
                colID++;
            }
            colID = 0;
            rowID++;
        }
    }


    void OnDataWinesTextsLoaded(List<SpreadsheetLoader.Line> d)
    {
        int colID = 0;
        int rowID = 0;
        Content contentLine = null;
        foreach (SpreadsheetLoader.Line line in d)
        {
            foreach (string value in line.data)
            {
                if (rowID >= 1)
                {
                    if (value != "")
                    {
                        if (colID == 0)
                        {
                            contentLine = GetSpecificWine(value);
                        }
                        else if (contentLine != null)
                        {
                            if (colID == 2)
                            {
                                contentLine.text = value.ToLower();
                            }
                        }
                    }
                }
                colID++;
            }
            colID = 0;
            rowID++;
        }
    }




    public Content GetSpecificWine(string id)
    {
        foreach (Content c in content)
            if (c.id == id)
                return c;
        return null;
    }
    public void SetActive(string codebar)
    {
        foreach (Content c in content)
            if (c.codebar == codebar)
                active = c;
    }
    void CheckForNewFilter( string filterName, string name)
    {
        FiltersData.FilterData fd = filtersData.GetFilter(filterName);
        if (fd != null)
        {
            foreach (string filter in fd.filters)
                if (filter == name)
                    return;
        }
        fd.filters.Add(name);
        fd.availableFilters.Add(name);
    }
   
    public List<Content> GetFiltered(int from, int to)
    {
        List<Content> arr = new List<Content>();
        int id = 0;
        foreach(Content c in contentFiltered)
        {
            if (id >= from && id < to)
                arr.Add(c);
            id++;
        }
        return arr;
    }
}
