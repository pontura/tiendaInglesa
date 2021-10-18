using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiltersContainerUI : MonoBehaviour
{
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
        FilterButtonUI newButton = Instantiate(button, container);
        newButton.Init(this, filterData);
    }
    public void Init(List<FiltersData.FilterData> all)
    {
        Utils.RemoveAllChildsIn(container);
        foreach (FiltersData.FilterData filterData in all)
            OnAddFilter(filterData);
    }
    public void RemoveItem(FilterButtonUI thisButton)
    {
        Events.OnRemoveFilter(thisButton.data);
        Destroy(thisButton.gameObject);
    }
    public void Reset()
    {
        Utils.RemoveAllChildsIn(container);
    }
}
