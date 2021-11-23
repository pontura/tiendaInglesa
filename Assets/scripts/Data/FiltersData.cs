using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FiltersData : MonoBehaviour
{
    string[] price_from = new string[] { "400", "600" , "800", "1000", "1200"};
    string[] price_to = new string[] { "600", "800", "1000", "1200", "1400" };

    WinesData winesData;
    public List<FilterData> filters;

    [Serializable]
    public class FilterData
    {
        public string name;
        public string applied = "";
        public List<string> filters;
        public List<string> availableFilters;

        public List<string> GetAvailableFilters(string filterName)
        {
            List<string> arr = Data.Instance.filtersData.GetFilter(filterName).availableFilters;
            arr.Sort((a, b) => a.CompareTo(b));
            for (int a = 0; a<arr.Count; a++)
                arr[a] = Utils.SetFirstLetterToUpper(arr[a]);
            return arr;
        }

        public FilterData(string name)
        {
            this.name = name;
            filters = new List<string>();
            availableFilters = new List<string>();
        }
        public void ResetAvailables()
        {
            availableFilters.Clear();
            foreach (string f in filters)
                availableFilters.Add(f);
        }
    }
    private void Awake()
    {
        winesData = GetComponent<WinesData>();
    }
    public void Init()
    {
        AddPriceData(WinesData.DESDE, price_from);
        AddPriceData(WinesData.HASTA, price_to);
        Reset();
        filters.Add(new FilterData(WinesData.CEPAS));
        filters.Add(new FilterData(WinesData.BRANDS));
        filters.Add(new FilterData(WinesData.PAISES));
        filters.Add(new FilterData(WinesData.GUARDA));

    }
    public void ArrangeFilters()
    {
        GetFilter(WinesData.PAISES).filters.Sort((a, b) => a.CompareTo(b));
        GetFilter(WinesData.BRANDS).filters.Sort((a, b) => a.CompareTo(b));
        GetFilter(WinesData.CEPAS).filters.Sort((a, b) => a.CompareTo(b));
    }
    void AddPriceData(string name, string[] values)
    {
        FilterData fd = new FilterData(name);
        foreach(string s in values)
             fd.filters.Add(s);
        filters.Add(fd);
    }
    public void Reset()
    {
        foreach (FilterData fd in filters)
        {
            fd.availableFilters.Clear();
            foreach (string f in fd.filters)
                fd.availableFilters.Add(f);
            fd.applied = "";
        }
    }
    public FilterData GetFilter(string filterName)
    {
        foreach (FilterData fd in filters)
            if (fd.name == filterName)
                return fd;
        return null;
    }
    void ApplyFilters()
    {
        string _desde = filters[0].applied;
        string _hasta = filters[1].applied;

        int desde = 0;
        int hasta = 0;

        if (_desde != "")
            desde = int.Parse(_desde);
        if (_hasta != "")
            hasta = int.Parse(_hasta);

        foreach (FilterData fd in filters)
        {
            if(fd.name != WinesData.DESDE && fd.name != WinesData.HASTA)
            fd.availableFilters.Clear();
        }

        string cepas_appliedFilter = GetFilter(WinesData.CEPAS).applied;
        string paises_appliedFilter = GetFilter(WinesData.PAISES).applied;
        string brands_appliedFilter = GetFilter(WinesData.BRANDS).applied;
        string edad_appliedFilter = GetFilter(WinesData.GUARDA).applied;


        //print(cepas_appliedFilter + " " + paises_appliedFilter + " " + brands_appliedFilter + "count " + winesData.contentFiltered.Count);
        int i = winesData.contentFiltered.Count;
        while (i > 0)
        {
            i--;
            WinesData.Content thisContentFiltered = winesData.contentFiltered[i];

            if (FilteredByPrice(thisContentFiltered, desde, hasta))
                winesData.contentFiltered.Remove(thisContentFiltered);
            else if (
                  (cepas_appliedFilter != "" && thisContentFiltered.cepa != cepas_appliedFilter)
               || (brands_appliedFilter != "" && thisContentFiltered.brand != brands_appliedFilter)
               || (paises_appliedFilter != "" && thisContentFiltered.pais != paises_appliedFilter)
               || (edad_appliedFilter != "" && thisContentFiltered.tiempo_guardia != edad_appliedFilter)
            )
            {
                //print(thisContentFiltered.cepa + " cepa_app: " + cepas_appliedFilter + " pais: " + paises_appliedFilter + " brands:" + brands_appliedFilter);
                winesData.contentFiltered.Remove(thisContentFiltered);
            }
            else
                CheckForNewAvailableFilters(thisContentFiltered);
        }

        ArrangeFiltered();
        //print("winesData.contentFiltered.Count: " + winesData.contentFiltered.Count);

    }
    public void ArrangeFiltered()
    {
        //  print("________ArrangeFiltered");
        List<WinesData.Content> exclusives = new List<WinesData.Content>();
        foreach (WinesData.Content wdc in winesData.contentFiltered)
        {
            if (wdc.isExclusive == 0)
                exclusives.Add(wdc);
        }
        foreach (WinesData.Content wdc in exclusives)
        {
            winesData.contentFiltered.Remove(wdc);
        }
        winesData.contentFiltered.Sort((a, b) => a.price.CompareTo(b.price));
        exclusives.Sort((a, b) => a.price.CompareTo(b.price));
        foreach (WinesData.Content wdc in exclusives)
        {
            winesData.contentFiltered.Insert(0, wdc);
        }
    }
    bool FilteredByPrice(WinesData.Content content, int desde, int hasta)
    {
        //print(content.price + "Desde: " + desde + " hasta " + hasta);
        if (desde == 0 && hasta == 0)
            return false;
        if (desde > 0 && hasta > 0 && (content.price < desde || content.price > hasta))
            return true;
        if (desde > 0 && content.price < desde)
            return true;
        if (hasta > 0 && content.price > hasta)
            return true;
        return false;
    }
    void CheckForNewAvailableFilters(WinesData.Content c)
    {
        foreach (FilterData fd in filters)
        {
            if (fd.name == WinesData.CEPAS && c.cepa != "")
                AddAvailable(fd.availableFilters, c.cepa);
            else if (fd.name == WinesData.PAISES && c.pais != "")
                AddAvailable(fd.availableFilters, c.pais);
            else if (fd.name == WinesData.BRANDS && c.brand != "")
                AddAvailable(fd.availableFilters, c.brand);
            else if (fd.name == WinesData.GUARDA && c.tiempo_guardia != "")
                AddAvailable(fd.availableFilters, c.tiempo_guardia);
        }
    }
    void AddAvailable(List<string> arr, string value)
    {
        foreach (string s in arr)
            if (s == value)
                return;
        if (value != null && value != "" && value.Length > 1)
            arr.Add(value);
    }
    public void AddFilter(string filterName, string value)
    {
        if (filterName == WinesData.DESDE || filterName == WinesData.HASTA)
            ChangePrices(filterName, int.Parse(value));

        //print("AddFilter " + filterName + " value: " + value);
        FilterData fd = GetFilter(filterName);        
        fd.applied = value;
        winesData.ResetFilters();
        ApplyFilters();
        Events.OnAddFilter(fd);
    }
    public void RemoveFilter(string filterName)
    {
        if (filterName == WinesData.DESDE || filterName == WinesData.HASTA)
            ResetPrices();

        FilterData fd = GetFilter(filterName);
        fd.applied = "";
        fd.availableFilters.Clear();

        foreach (string s in fd.filters)
            if(s.Length>1)
                fd.availableFilters.Add(s);

        winesData.ResetFilters();
        ApplyFilters();
    }
    void ResetPrices()
    {
        GetFilter(WinesData.HASTA).ResetAvailables();
        GetFilter(WinesData.DESDE).ResetAvailables();
    }
    void ChangePrices(string filterName, int value)
    {
        FilterData fd;
        if(filterName == WinesData.DESDE)
            fd = GetFilter(WinesData.HASTA);
        else
            fd = GetFilter(WinesData.DESDE);
        fd.availableFilters.Clear();
        foreach(string s in fd.filters)
        {
            int a = int.Parse(s);
            if (a > value && filterName == WinesData.DESDE)
                fd.availableFilters.Add(s);
            else if (a < value && filterName == WinesData.HASTA)
                fd.availableFilters.Add(s);
        }
    }
    public bool HasFiltersApplied()
    {
        foreach(FilterData fd in filters)
        {
            if (fd.applied != null && fd.applied != "")
                return true;
        }
        return false;
    }
}
