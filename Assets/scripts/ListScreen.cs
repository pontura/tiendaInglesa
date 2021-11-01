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
    [SerializeField] Scrollbar scrollBar;
    [SerializeField] FiltersContainerUI filtersContainer;
    public GameObject filtersButton;
    [SerializeField] bool loaded;

    private void Awake()
    {
        automaticOpenFilters = true;
        filtersScreen = GetComponent<FiltersScreen>();
        paginatorUI = GetComponent<PaginatorUI>();
        Events.ResetSearch += ResetSearch;
    }
    private void OnDestroy()
    {
        Events.ResetSearch -= ResetSearch;
    }
    public void ResetSearch()
    {
        loaded = false;
    }
    public override void OnBack()
    {
        Game.Instance.screensManager.Show(types.MAIN_MENU);
        automaticOpenFilters = true;
        filtersContainer.Reset();
    }
    public override void OnShow()
    {
        base.OnShow();
        if (loaded) return;

        if (Data.Instance.sommelierData.activeSommelierList)
        {
            OpenSommelierList();
            return;
        }

        filtersButton.SetActive(true);
        filtersContainer.gameObject.SetActive(true);

        if (automaticOpenFilters)
            OpenFilters();
        else
        {
            filtersButton.SetActive(true);
            paginatorUI.Init();
        }
        automaticOpenFilters = false;    
    }
    void OpenSommelierList()
    {
        Data.Instance.sommelierData.FilterBySommelier();
        automaticOpenFilters = false;
        ShowResults(new Vector2(0, 20));
        OpenFilters();
        paginatorUI.Hide();
        filtersButton.SetActive(false);
        filtersScreen.title.text = "Te sugerimos estos vinos";
        filtersContainer.gameObject.SetActive(false);
    }
    public void ShowAll()
    {
        Vector2 from_to = paginatorUI.GetFromTo();
        ShowResults(from_to);
    } 
    void ShowResults(Vector2 from_to)
    {
        print("ShowResults");
        Utils.RemoveAllChildsIn(container);
        List<WinesData.Content> arr;
        if (Data.Instance.sommelierData.activeSommelierList)
        {
            arr = Data.Instance.winesData.GetSommelierExlusives();
            Add(arr);
        }
        arr = Data.Instance.winesData.GetFiltered((int)from_to.x, (int)from_to.y);
        Add(arr);
        scrollBar.value = 1;
        loaded = true;
    }
    void Add(List<WinesData.Content> arr)
    {
        foreach (WinesData.Content c in arr)
        {
            ListItem li = Instantiate(listItem, container);
            li.Init(this, c);
        }
    }
    public void OnSelect(WinesData.Content content)
    {
        Data.Instance.winesData.SetActive(content.codebar);
        Game.Instance.screensManager.Show(types.RESULT);
    }
    public void OpenFilters()
    {
        filtersScreen.OnShow();
        filtersButton.SetActive(false);
    }
}
