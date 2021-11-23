using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltersScreen : MonoBehaviour
{
    public Text title;
    public Text alertField;
    public FilterDropDownUI filterDropDownUI;
    [SerializeField] Transform container;
    public List<FilterDropDownUI> dropDowns;
    string defaultString = "";
    WinesData winesData;
    [SerializeField] GameObject panel;
    ListScreen listScreen;
    public bool changed;
    public Button button;
    PaginatorUI paginator;

    private void Awake()
    {
        alertField.gameObject.SetActive(false);
        listScreen = GetComponent<ListScreen>();
        paginator = GetComponent<PaginatorUI>();
    }
    public void OnShow()
    {
        winesData = Data.Instance.winesData;
        panel.SetActive(true);
        Refresh();
        changed = false;
        SetButton();
        paginator.Hide();
    }
    void SetButton()
    {
        if (Data.Instance.filtersData.HasFiltersApplied())
            button.interactable = true;
        else
            button.interactable = false;
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
        print("OnRemoveFilter " + data.name);
        Data.Instance.filtersData.RemoveFilter(data.name);
        Refresh();
        OnSelect();
        changed = true;
        SetButton();
    }
    public void Refresh()
    {
        Utils.RemoveAllChildsIn(container);
        int total = 0;
        foreach (FiltersData.FilterData fd in Data.Instance.filtersData.filters)
        {
         //   print(fd.applied + fd.name + " count: " + fd.availableFilters.Count);

            if (fd.applied == "" && fd.availableFilters.Count>1)
            {
                FilterDropDownUI fdd = Instantiate(filterDropDownUI, container);
                fdd.Init(this,fd);
                total++;
            }
        }
        if (total == 0)
            alertField.gameObject.SetActive(true);
        else 
            alertField.gameObject.SetActive(false);

        if (winesData.content.Count == winesData.contentFiltered.Count)
            title.text = "Todos los vinos";
        else if (winesData.contentFiltered.Count == 1)
            title.text = "1 vino";
        else
            title.text = winesData.contentFiltered.Count + " vinos";

        SetButton();
    }
    public void OnChange()
    {
        changed = true;
    }
    public void OnSelect()
    {
        OnHide();
        listScreen.filtersButton.SetActive(true);
        // listScreen.filtersButton.SetActive(true);
        if (changed)
        {
            listScreen.ResetSearch();
            listScreen.OnShow();
        }
    }
}