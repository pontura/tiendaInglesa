using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltersScreen : MonoBehaviour
{
    [SerializeField] Text title;
    public FilterDropDownUI filterDropDownUI;
    [SerializeField] Transform container;
    public List<FilterDropDownUI> dropDowns;
    string defaultString = "";
    WinesData winesData;
    [SerializeField] GameObject panel;
    ListScreen listScreen;

    private void Awake()
    {
        listScreen = GetComponent<ListScreen>();       
    }
    public void OnShow()
    {
        winesData = Data.Instance.winesData;
        panel.SetActive(true);
        Refresh();
    }
    public void OnHide()
    {
        panel.SetActive(false);
    }
    private void OnEnable()
    {
        Events.OnRemoveFilter += OnRemoveFilter;
    }
    private void OnDisable()
    {
        Events.OnRemoveFilter -= OnRemoveFilter;
    }
    void OnRemoveFilter(FiltersData.FilterData data)
    {
        Data.Instance.filtersData.RemoveFilter(data.name);
        Refresh();
        OnSelect();
    }
    public void Refresh()
    {
        Utils.RemoveAllChildsIn(container);
        foreach(FiltersData.FilterData fd in Data.Instance.filtersData.filters)
        {
            if (fd.applied == "")
            {
                FilterDropDownUI fdd = Instantiate(filterDropDownUI, container);
                fdd.Init(this,fd);
            }
        }
        if (winesData.content.Count == winesData.contentFiltered.Count)
            title.text = "Todos los vinos";
        else if (winesData.contentFiltered.Count == 1)
            title.text = "1 vino";
        else
            title.text = winesData.contentFiltered.Count + " vinos";
    }
    void AddToDropDown(Dropdown dropDown, List<string> arr)
    {
        dropDown.ClearOptions();
        dropDown.AddOptions(arr);
    }
    public void OnSelect()
    {
        OnHide();
        listScreen.OnShow();
    }
}
