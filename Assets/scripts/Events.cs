using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action ResetApp = delegate { };
    public static System.Action<string> OnScanDone = delegate { };
    public static System.Action<FiltersData.FilterData> OnRemoveFilter = delegate { };
    public static System.Action<FiltersData.FilterData> OnAddFilter = delegate { };
    public static System.Action ResetSearch = delegate { };

}
   
