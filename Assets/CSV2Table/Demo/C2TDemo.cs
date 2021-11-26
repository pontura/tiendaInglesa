using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class C2TDemo : MonoBehaviour 
{
    public string googleURL;
    public List<Row> rowList;

    [Serializable]
    public class Row
    {
        public string Row1;
        public string Row2;
        public string Row3;
        public string Row4;
        public string Row5;
        public string Row6;
        public string Row7;
        public string Row8;
        public string Row9;
        public string Row10;
        public string Row11;
        public string Row12;
        public string Row13;
        public string Row14;
        public string Row15;
        public string Row16;
        public string Row17;

    }

    public void Start()
	{
        StartCoroutine(GetData(googleURL));
    }
    IEnumerator GetData(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            
            Load(www.text);
        }
    }

   

    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(string csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.Row1 = grid[i][0];
            row.Row2 = grid[i][1];
            row.Row3 = grid[i][2];
            row.Row4 = grid[i][3];
            row.Row5 = grid[i][4];
            row.Row6 = grid[i][5];
            row.Row7 = grid[i][6];
            row.Row8 = grid[i][7];
            row.Row9 = grid[i][8];
            row.Row10 = grid[i][9];
            row.Row11 = grid[i][10];
            row.Row12 = grid[i][11];
            row.Row13 = grid[i][12];
            row.Row14 = grid[i][13];
            row.Row15 = grid[i][14];
            row.Row16 = grid[i][15];
            row.Row17 = grid[i][16];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

}
