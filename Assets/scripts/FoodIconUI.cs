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
        field.text = name;
        string v = name.Replace("�", "n");
        image.sprite = Resources.Load<Sprite>("foods/" + v);
        image.SetNativeSize();
    }
}
