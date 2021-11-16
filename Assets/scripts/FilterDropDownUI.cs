using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterDropDownUI : MonoBehaviour
{
    [SerializeField] Text title;
    FiltersScreen ui;
    FiltersData.FilterData filterData;
    Dropdown dropDown;

    public void Init(FiltersScreen ui, FiltersData.FilterData filterData)
    {
        dropDown = GetComponent<Dropdown>();
        this.filterData = filterData;
        this.ui = ui;
        title.text = "" ;// filterData.name;
        dropDown.AddOptions(new List<string>() { filterData.name });
        dropDown.AddOptions(filterData.GetAvailableFilters(filterData.name));
        dropDown.onValueChanged.AddListener(delegate {
            OnChanged();
        });
    }
    public void OnChanged()// hack para que refresque el status (raro)
    {
        ui.OnChange();
        Invoke("Delayed", 0.1f); 
        dropDown.onValueChanged.RemoveAllListeners();
    }
    void Delayed()
    {
        if (dropDown.value == 0) return;
        string value = dropDown.options[dropDown.value].text;        
        Data.Instance.filtersData.AddFilter(filterData.name, value.ToLower());
        ui.Refresh();
    }
}
