using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterButtonUI : MonoBehaviour
{
    public FiltersData.FilterData data;
    [SerializeField] Text field;
    FiltersContainerUI ui;

    public void Init(FiltersContainerUI ui, FiltersData.FilterData data)
    {
        this.data = data;
        this.ui = ui;
        field.text = data.applied;
    }
    public void OnClicked()
    {
        ui.RemoveItem(this);
    }
}
