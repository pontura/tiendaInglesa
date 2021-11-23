using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodsData : MonoBehaviour
{
    public List<FoodData> all;

    [Serializable]
    public class FoodData
    {
        public string name;
        public string cepas;
    }
    public List<FoodData> GetAll()
    {
        return all;
    }
    public List<string> GetFoodsByTag(List<string> tags)
    {
        List<string> arr = new List<string>();
        foreach(string tag in tags)
        {
            if (IsFood(tag))
                arr.Add(tag);
        }
        return arr;
    }
    bool IsFood(string tag)
    {
        foreach (FoodData fd in all)
            if (fd.name == tag)
                return true;
        return false;
    }
    public List<string> GetFoodByCepa(string _cepa)
    {
        List<string> arr = new List<string>();
        if (_cepa == null || _cepa == "")
            return arr;
        foreach(FoodData fd in all)
        {
            string s = fd.cepas.Replace(" ", "").ToLower();
            s = fd.cepas.Replace("-", " ");
            string[] allArr = s.Split(","[0]);
            foreach (string cepa in allArr)
            {
               // print(cepa + " : " + _cepa);
                if (cepa == _cepa.ToLower())
                    arr.Add(fd.name);
            }
        }
        return arr;
    }
}
