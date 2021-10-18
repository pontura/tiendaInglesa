using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] Text nameField;
    [SerializeField] Text priceField;
    [SerializeField] Image image;
    WinesData.Content content;
    ListScreen manager;

    public void Init(ListScreen manager, WinesData.Content content)
    {
        this.manager = manager;
        this.content = content;
        nameField.text = "<b>" + content.name + "</b>";
        nameField.text += "\n" + content.pais;
        priceField.text = "$" + content.price;
        StartCoroutine( Data.Instance.imagesLoader.C_LoadImage(content.id, 60, 60, OnLoaded));
    }
    void OnLoaded(Sprite s)
    {
        if(isActiveAndEnabled)
            image.sprite = s;
        image.SetNativeSize();
    }
    public void OnClicked()
    {
        manager.OnSelect(content);
    }
}
