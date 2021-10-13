using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] Text nameField;
    [SerializeField] Text cepaField;
    [SerializeField] Text tagsField;
    [SerializeField] Text priceField;
    [SerializeField] Image image;

    WinesData.Content content;
    ListScreen manager;

    public void Init(ListScreen manager, WinesData.Content content)
    {
        this.manager = manager;
        this.content = content;
        nameField.text = content.name;
        tagsField.text = content.pais;
        priceField.text = "$" + content.price;
    }
    public void OnClicked()
    {
        manager.OnSelect(content);
    }
}
