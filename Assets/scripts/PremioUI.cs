using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremioUI : MonoBehaviour
{
    [SerializeField] Text field;
    [SerializeField] Text fieldName;

    public void Init(string name, int text)
    {
        fieldName.text = name;
        field.text = text.ToString();
    }
}
