using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinesData : DataLoader
{
    public List<Content> content;
    public Content active;

    [Serializable]
    public class Content
    {
        public string codebar;
        public string name;
        public string cepa;
        public string text;
        public string image_url;
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
                            contentLine.codebar =  value;
                            content.Add(contentLine);
                        }
                    }
                    else if(value != "")
                        Add(contentLine, colID, value);
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
            case 2: contentLine.name = value; break;
            case 3: contentLine.cepa = value; break;
            case 4: contentLine.image_url = value; break;
            case 5: contentLine.text = value; break;
        }
    }
    public void SetActive(string codebar)
    {
        foreach (Content c in content)
            if (c.codebar == codebar)
                active = c;
    }
}
