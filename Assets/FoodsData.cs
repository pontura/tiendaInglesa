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
}
