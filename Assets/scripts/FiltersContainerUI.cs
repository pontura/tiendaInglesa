using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiltersContainerUI : MonoBehaviour
{

    [SerializeField] GameObject filterButton;
    [SerializeField] FilterButtonUI button;
    [SerializeField] Transform container;

    private void OnEnable()
    {
        Events.OnAddFilter += OnAddFilter;
    }
    private void OnDisable()
    {
        Events.OnAddFilter -= OnAddFilter;
    }
    void OnAddFilter(FiltersData.FilterData filterData)
    {
        filterButton.transform.SetParent(transform);
        FilterButtonUI newButton = Instantiate(button, container);
        newButton.Init(this, filterData);
        filterButton.transform.SetParent(container);
    }
    public void Init(List<FiltersData.FilterData> all)
    {
        filterButton.transform.SetParent(transform);
        Utils.RemoveAllChildsIn(container);

        foreach (FiltersData.FilterData filterData in all)
            OnAddFilter(filterData);

        filterButton.transform.SetParent(container);
    }
    public void RemoveItem(FilterButtonUI thisButton)
    {
        Events.OnRemoveFilter(thisButton.data);
        Destroy(thisButton.gameObject);
    }
    public void Reset()
    {
        filterButton.transform.SetParent(transform);
        Utils.RemoveAllChildsIn(container);
        filterButton.transform.SetParent(container);
    }
}
