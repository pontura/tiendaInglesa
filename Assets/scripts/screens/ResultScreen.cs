using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MainScreen
{
    [SerializeField] GameObject guarda;
    [SerializeField] GameObject temp;
    [SerializeField] GameObject foods;
    [SerializeField] GameObject premios;

    public Text nameField;
    public Text cepaField;
    public Text paisField;
    public Text marcaField;
    public Text textField;

    public Text priceField;

    public Text guardaField;
    public Text tempField;

    public Image image;
    [SerializeField] Transform foodsContainer;
    [SerializeField] FoodIconUI foodIcon;


    [SerializeField] Transform premiosContainer;
    [SerializeField] PremioUI premioUI;

    public override void OnBack()
    {
        if(Game.Instance.screensManager.lastActiveScreen.type == types.LIST)
            Game.Instance.screensManager.Show(MainScreen.types.LIST);
        else
            Game.Instance.screensManager.Show(MainScreen.types.MAIN_MENU);
    }
    public override void OnHide()
    {
        image.sprite = null;
    }
    WinesData.Content active ;
    public override void OnShow()
    {
        base.OnShow();
        active = Data.Instance.winesData.active;

        priceField.text = "$" + active.price;

        nameField.text = active.name;
        cepaField.text = "<i>" + active.cepa + "</i>";
        string salto = "\n\n";
        paisField.text = active.pais;
        marcaField.text = active.brand;

        textField.text = active.text;
        textField.text += "\n";

        if (active.tiempo_guardia != null && active.tiempo_guardia != "")
        {
            guardaField.text = active.tiempo_guardia;
            guarda.SetActive(true);
        } else guarda.SetActive(false);

        if (active.temp != null && active.temp != "")
        {
            temp.SetActive(true);
            tempField.text = active.temp;
         } else temp.SetActive(false);

        StartCoroutine(Data.Instance.imagesLoader.C_LoadImage(active.id, 200, 200, OnLoaded, "large") );

        SetFoods();
        SetPremios();
    }
    void OnLoaded(Sprite sprite)
    {
        image.sprite = sprite;
        image.SetNativeSize();
    }
    void SetFoods()
    {
        Utils.RemoveAllChildsIn(foodsContainer);
        List<string> all = Data.Instance.foodsData.GetFoodsByTag(active.tags);
        int qty = all.Count;
        if (qty < 4)
        {
            List<string> allCepas = Data.Instance.foodsData.GetFoodByCepa(active.cepa);
            foreach (string s in allCepas)
            {
                if (qty < 4)
                    all.Add(s);
                qty++;
                print("Agrega por cepa: " + active.cepa + " : " + s);
            }
        }

        if (all != null && all.Count < 1)
            foods.SetActive(false);
        else
        {
            foreach (string s in all)
            {
                FoodIconUI fi = Instantiate(foodIcon, foodsContainer);
                fi.Init(s);
            }
            foods.SetActive(true);
        }
    }
    void SetPremios()
    {
        Utils.RemoveAllChildsIn(premiosContainer);
        premios.SetActive(false);

        if (active.p1 > 0)
            AddPremio("Descorchados",  active.p1);
        if (active.p2 > 0)
            AddPremio("Tim Atkin", active.p2);
        if (active.p3 > 0)
            AddPremio("James Suckling", active.p3);
    }
    void AddPremio(string name, int text)
    {
        PremioUI p = Instantiate(premioUI, premiosContainer);
        p.Init(name, text);
        premios.SetActive(true);
    }
}
