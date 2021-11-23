using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataToText : MonoBehaviour
{
    public const string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdtJVqb3SRR-vL-mce4wZIEQiPAWPpx33b7wyJjd7wZPsH1sg/formResponse";

    private WWWForm form;

    public void CheckForWinesAntoDidntOverride()
    {
        List<WinesData.Content> newWines = new List<WinesData.Content>();
        WinesData winesData = GetComponent<WinesData>();
        foreach (WinesData.Content wineData in winesData.content)
        {
            if(!wineData.antoOverrided)
                newWines.Add(wineData);
        }
        foreach (WinesData.Content wineData in newWines)
        {
            print("____new wine id: " + wineData.id + " name: " + wineData.name);
        }
    }


    public void SaveWinesMaster()
    {
        WinesData winesData = GetComponent<WinesData>();
       // StartCoroutine(SaveWinesMasterC(winesData.content));
    }
    IEnumerator SaveWinesMasterC(List<WinesData.Content> content)
    {
        WinesData winesData = GetComponent<WinesData>();
        int id = 0;
        foreach (WinesData.Content wineData in content)
        {
           form = new WWWForm();

           AddToForm("entry.1971818089", wineData.id);
           AddToForm("entry.1400684166", wineData.name);
           AddToForm("entry.508827743", SetListToString(wineData.tags));
           AddToForm("entry.582080243", wineData.pais);
           AddToForm("entry.1582468151", wineData.brand);
           AddToForm("entry.420119645", wineData.tiempo_guardia);
           AddToForm("entry.520883067", wineData.cepa);
           AddToForm("entry.1275718940", wineData.text);
           AddToForm("entry.1390390408", wineData.temp);

            byte[] rawData = form.data;
            WWW www = new WWW(BASE_URL, rawData);
            yield return www;
            id++;
            Debug.Log(id + " - " + wineData.name);
        }  
    }
    string SetListToString(List<string> arr)
    {
        string a = "";
        int id = 0;
        foreach (string s in arr)
        {
            id++;
            a += s;
            if (id < arr.Count)
                a += ",";
        }
        return a;
    }
    void AddToForm(string entryID, string value)
    {
        if(value != null && value != "")
            form.AddField(entryID, value);
    }




    //private void Start()
    //{
    //    CreateLog();
    //}
    //void CreateLog()
    //{
    //    string timestamp = DateTime.Now.ToString("dd-mm-yyyy_hh-mm-ss");
    //    PlayerPrefs.SetString("timestamp", timestamp);
    //    string path = "/tienda/pontura/a.txt";
    //    // This text is added only once to the file.
    //    if (!File.Exists(path))
    //    {
    //        // Create a file to write to.
    //        using (StreamWriter sw = File.CreateText(path))
    //        {
    //            sw.WriteLine(DateTime.Now.ToString() + ": " + "App initialised");
    //        }
    //    }
    //    else
    //    {
    //        // This text is always added, making the file longer over time
    //        // if it is not deleted.
    //        using (StreamWriter sw = File.AppendText(path))
    //        {
    //            sw.WriteLine(DateTime.Now.ToString() + ": " + "App initialised");
    //        }
    //    }

    //    // Open the file to read from.
    //    using (StreamReader sr = File.OpenText(path))
    //    {
    //        string line = "";
    //        while ((line = sr.ReadLine()) != null)
    //        {
    //            Debug.Log(line);
    //        }
    //    }
    //    print("init");
    //}
}
