using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinesData : DataLoader
{
    public List<Content> content;
    public List<Content> contentFiltered;
    public List<string> brands;
    public List<string> paises;
    public List<string> cepas;
    public Content active;

    [Serializable]
    public class Content
    {
        public string id;
        public string codebar;
        public string name;
        public string pais;
        public string cepa;
        public string brand;
        public string[] tags;
        public int price;
    }

    private void Start()
    {
        LoadData(null);
    }
    public override void OnLoaded(List<SpreadsheetLoader.Line> d)
    {
        OnDataLoaded(content, d);
        // base.OnLoaded(d);
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
                //print("row: " + rowID + "  colID: " + colID + "  value: " + value);
                if (rowID >= 1)
                {
                    if (colID == 0)
                    {
                        if (value != "")
                        {
                            contentLine = new Content();
                            content.Add(contentLine);
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
            case 5: contentLine.brand = value; CheckForNewFilter(brands, value);  break;
            case 9: contentLine.cepa = value; CheckForNewFilter(cepas, value); break;
            case 12:
                string[] arr = value.Split(","[0]);
                if (arr.Length > 1)
                    contentLine.tags = arr;
                break;
            case 13: contentLine.pais = value; CheckForNewFilter(paises, value); break;
            case 14: contentLine.cepa = value; CheckForNewFilter(cepas, value); break;
        }
    }
    public void SetActive(string codebar)
    {
        foreach (Content c in content)
            if (c.codebar == codebar)
                active = c;
    }
    void CheckForNewFilter(List<string> arr, string name)
    {
        foreach (string s in arr)
            if (s == name)
                return;
        arr.Add(name);
    }
    public void Filter(string cepa, string brand, string paises)
    {
        contentFiltered.Clear();
        foreach (Content c in content)
        {
            if (c.cepa == cepa && c.pais == paises && c.brand == brand)
                contentFiltered.Add(c);
        }

    }
}
