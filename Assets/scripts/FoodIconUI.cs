using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIconUI : MonoBehaviour
{
    [SerializeField] Text field;
    [SerializeField] Image image;

    public void Init(string name)
    {
        field.text = name.Replace("-", " ");
        string v = name.Replace("ñ", "n");        
        image.sprite = Resources.Load<Sprite>("foods/" + v);
        image.SetNativeSize();
    }
}
