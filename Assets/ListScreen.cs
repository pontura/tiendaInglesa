using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListScreen : MainScreen
{
    public ListItem listItem;
    public Transform container;

    public override void OnShow()
    {
        base.OnShow();
        foreach(WinesData.Content c in Data.Instance.winesData.contentFiltered)
        {
            ListItem li = Instantiate(listItem, container);
            li.Init(this, c);
        }
    }
    public void OnSelect(WinesData.Content content)
    {

    }
}
