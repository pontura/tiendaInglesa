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
    public static string TAGS = "Tags";
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
                int numericValue = 0;
                int.TryParse(r, out numericValue);
                if(numericValue>0)
                    contentLine.price = int.Parse(r);
                break;
            case 5: contentLine.brand = value.ToLower(); CheckForNewFilter(BRANDS, value.ToLower());  break;
            case 9:
                string v = value.ToLower();
                contentLine.cepa = v;
                CheckForNewFilter(CEPAS, v); break;
            case 11: contentLine.text = value; break;
            //case 12:
            //    string[] tagsArr = value.Split(","[0]);
            //    foreach (string s in tagsArr)
            //        contentLine.tags.Add(s);
            //    break;
            //case 13: contentLine.pais = value; CheckForNewFilter(PAISES, value.ToLower()); break;
            //case 14: contentLine.cepa = value.ToLower(); CheckForNewFilter(CEPAS, value.ToLower()); break;
            //case 15:
            //    string[] mArr = value.Split(","[0]);
            //    foreach (string s in mArr)
            //        contentLine.maridaje.Add(s);
            //    break;
        }
    }






    void OnDataListadoLoaded(List<SpreadsheetLoader.Line> d)
    {
        print("OnDataListadoLoaded");
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
                            if (contentLine == null)
                                print(rowID + " no hay vino: " + value);
                        }
                        else if (contentLine != null) {
                            if (colID == 1)
                            {
                                contentLine.name = value;
                            }
                            if (colID == 2)
                            {
                                contentLine.brand = value.ToLower(); CheckForNewFilter(BRANDS, contentLine.brand);
                            }
                            if (colID == 3)
                            {
                                contentLine.cepa = value.ToLower();
                                print(rowID + " " + contentLine.name + " cepa: " + contentLine.cepa);
                                CheckForNewFilter(CEPAS, contentLine.cepa);
                            }
                            else if (colID == 4)
                            {
                                string v = value.Replace(" ", "").ToLower();
                                string[] arr = v.Split(","[0]);
                                foreach(string s in arr)
                                    contentLine.tags.Add(s);
                            }
                            else if (colID == 5)
                            {
                                string v = value.Replace(" ", "").ToLower();
                                contentLine.pais = v;
                            }
                            else if (colID == 6)
                            {
                                string v = value.ToLower();
                                if (v == "1 a 3 años")
                                    contentLine.tags.Add("joven");
                                else if (v == "4 a 6 años" || v == "Mas de 6 años")
                                    contentLine.tags.Add("guarda");
                                contentLine.tiempo_guardia = v;
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
                            else if (colID == 8)
                            {
                                contentLine.p3 = int.Parse(value);
                            }
                            else if (colID == 9)
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
    public List<Content> GetSommelierExlusives()
    {
        List<Content> arr = new List<Content>();
        foreach(string s in Data.Instance.sommelierData.GetAllExlusives())
            arr.Add(GetSpecificWine(s));
        return arr;
    }
    public string debug_cepa;
    public void ApplySommelierFilter(string name, List<string> values, bool onlyIfHasAll = false)
    {
        List<Content> toRemove = new List<Content>();
        foreach (Content c in contentFiltered)
        {
            bool matched = false;
            if (name == WinesData.CEPAS)
            {
                foreach (string value in values)
                {
                    debug_cepa = value;
                  //  print(c.cepa.ToLower() + "_" + value.ToLower());
                    if (c.cepa.ToLower() == value.ToLower())
                        matched = true;                                 
                }
                if(!matched)
                    toRemove.Add(c);
            }
            else if (name == WinesData.PAISES)
            {
                foreach (string value in values)
                {
                    if (c.pais.ToLower() == value.ToLower())
                        matched = true;
                }
                if (!matched)
                    toRemove.Add(c);
            }
            else if (name == WinesData.TAGS)
            {
                if (!HasAllTags(values, c.tags))
                    toRemove.Add(c);
            }
        }
        foreach (Content c in toRemove)
            contentFiltered.Remove(c);
    }
    bool HasAllTags(List<string> values, List<string> tags)
    {
        foreach (string value in values)
        {
            if (!HasTag(tags, value))
                return false;
        }
        return true;
    }
    bool HasTag(List<string> tags, string value)
    {
        foreach (string tag in tags)
        {
            if (tag.ToLower().Equals(value.ToLower()))
            {
                return true;
            }
        }
        return false;
    }
}
