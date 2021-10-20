using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListScreen : MainScreen
{
    public ListItem listItem;
    public Transform container;
    PaginatorUI paginatorUI;
    public FiltersScreen filtersScreen;
    public bool automaticOpenFilters;
    [SerializeField] Text filterButtonText;
    bool filtersOpen;
    [SerializeField] Scrollbar scrollBar;
    [SerializeField] FiltersContainerUI filtersContainer;

    private void Awake()
    {
        automaticOpenFilters = true;
        filtersScreen = GetComponent<FiltersScreen>();
        paginatorUI = GetComponent<PaginatorUI>();
    }
    public override void OnBack()
    {
        Game.Instance.screensManager.Show(types.MAIN_MENU);
        automaticOpenFilters = true;
        filtersContainer.Reset();
    }
    public override void OnShow()
    {
        if (automaticOpenFilters)
            OpenFilters();
        else
        {
            filtersOpen = false;
            SetFilterText();
        }
        automaticOpenFilters = false;
        base.OnShow();
        paginatorUI.Init();       
    }
    public void ShowAll()
    {
        Utils.RemoveAllChildsIn(container);
        Vector2 from_to = paginatorUI.GetFromTo();
        print("filtra: " + from_to);
        List<WinesData.Content> arr = Data.Instance.winesData.GetFiltered((int)from_to.x, (int)from_to.y);
        foreach (WinesData.Content c in arr)
        {
            ListItem li = Instantiate(listItem, container);
            li.Init(this, c);
        }
        scrollBar.value = 1;
    }
    public void OnSelect(WinesData.Content content)
    {
        Data.Instance.winesData.SetActive(content.codebar);
        Game.Instance.screensManager.Show(types.RESULT);
    }
    public void OpenFilters()
    {
        if (filtersOpen)
        {
            filtersOpen = false;
            filtersScreen.OnHide();
        }
        else
        {
            filtersOpen = true;
            filtersScreen.OnShow();
        }
        SetFilterText();
    }
    void SetFilterText()
    {
        if (filtersOpen)
            filterButtonText.text = "X Filtros";
        else
            filterButtonText.text = "Filtros";
    }
}
