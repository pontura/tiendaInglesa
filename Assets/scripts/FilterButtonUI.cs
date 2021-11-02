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
        string prefix = "";

        if (data.name == "$ Desde")
            prefix = "Desde $";
        else if (data.name == "$ Hasta")
            prefix = "Hasta $";

            field.text = prefix + " " + data.applied;
    }
    public void OnClicked()
    {
        ui.RemoveItem(this);
    }
}
