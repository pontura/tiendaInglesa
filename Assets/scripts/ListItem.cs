using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] Text nameField;
    [SerializeField] Text descField;
    [SerializeField] Text priceField;
    [SerializeField] Image image;
    [SerializeField] Image thumb;
    WinesData.Content content;
    ListScreen manager;


    public void Init(ListScreen manager, WinesData.Content content)
    {
        thumb.enabled = true;
        this.manager = manager;
        this.content = content;
        nameField.text = content.name;

        if (content.cepa != null && content.cepa.Length > 2)
            descField.text = "Cepa: " + content.cepa;
        if (content.brand != null && content.brand.Length > 2)
            descField.text += ". Bodega: " + content.brand + "\n";
        if (content.pais != null && content.pais.Length > 2)
            descField.text += "Pa�s: " + content.pais; //"\n" + 

        priceField.text = "$" + content.price;
        StartCoroutine( Data.Instance.imagesLoader.C_LoadImage(content.id, 60, 60, OnLoaded));
    }
    void OnLoaded(Sprite s)
    {
        if(isActiveAndEnabled)
            image.sprite = s;
        image.SetNativeSize();
        thumb.enabled = false;
    }
    public void OnClicked()
    {
        manager.OnSelect(content);
    }
}
