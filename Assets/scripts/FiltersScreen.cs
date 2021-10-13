using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltersScreen : MainScreen
{
    public Dropdown cepas;
    public Dropdown paises;
    public Dropdown brands;
    string defaultString = "Selecciona";

    public void Start()
    {
        AddToDropDown(cepas, Data.Instance.winesData.cepas);
        AddToDropDown(paises, Data.Instance.winesData.paises);
        AddToDropDown(brands, Data.Instance.winesData.brands);
    }
    void AddToDropDown(Dropdown dropDown, List<string> arr)
    {
        dropDown.AddOptions(arr);
    }
    public void OnSelect()
    {
        string cepa = cepas.options[cepas.value].text;
        if (cepa == defaultString)  cepa = "";

        string pais = paises.options[paises.value].text;
        if (pais == defaultString) pais = "";

        string brand = brands.options[brands.value].text;
        if (brand == defaultString) brand = "";

        Data.Instance.winesData.Filter(cepa, brand, pais);

        Game.Instance.screensManager.Show(MainScreen.types.LIST);
    }
}
