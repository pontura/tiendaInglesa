using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaginatorUI : MonoBehaviour
{
    public GameObject panel;
    public Text field;
    public Button nextBtn;
    public Button prevButton;
    public int id;
    public int total;
    public int totalPags;
    public int size = 40;
    ListScreen listScreen;
    private void Awake()
    {
        listScreen = GetComponent<ListScreen>();
    }
    public void Init()
    {
        id = 1;
        total = Data.Instance.winesData.contentFiltered.Count;
        totalPags = (int)Mathf.Ceil((float)total / (float)size);
        SetValues();
        panel.SetActive(true);
    }
    void SetValues()
    {
        if (id == 1)
            prevButton.interactable = false;
        else
            prevButton.interactable = true;

        if (id == totalPags)
            nextBtn.interactable = false;
        else
            nextBtn.interactable = true;

        field.text = id + "/" + totalPags;
        listScreen.ShowAll();
    }
    public void Hide()
    {
        panel.SetActive(false);
    }
    public void Next()
    {
        id++;
        SetValues();
    }
    public void Prev()
    {
        id--;
        SetValues();
    }
    public Vector2 GetFromTo()
    {
        int init = size * (id - 1);
        Vector2 from_to = new Vector2(init, init+size);
        return from_to;
    }
}
