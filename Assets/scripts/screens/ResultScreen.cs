using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MainScreen
{
    [SerializeField] GameObject guarda_temp;
    [SerializeField] GameObject guarda;
    [SerializeField] GameObject temp;
    [SerializeField] GameObject foods;
    [SerializeField] GameObject premios;

    public Text nameField;
    public Text cepaField;
    public Text paisField;
    public Text marcaField;

    public GameObject title_cepaField;
    public GameObject title_paisField;
    public GameObject title_brandField;

    public Text textField;

    public Text priceField;

    public Text guardaField;
    public Text tempField;

    public Image image;
    [SerializeField] Transform foodsContainer;
    [SerializeField] FoodIconUI foodIcon;


    [SerializeField] Transform premiosContainer;
    [SerializeField] PremioUI premioUI;
    [SerializeField] GameObject bottle;

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
        bottle.SetActive(true);
        base.OnShow();
        active = Data.Instance.winesData.active;

        priceField.text = "$" + active.price;

        nameField.text = active.name.ToUpper();
        if (active.cepa == null || active.cepa.Length < 2)
            title_cepaField.gameObject.SetActive(false);
        else
        {
            title_cepaField.gameObject.SetActive(true);
            cepaField.text = Utils.SetFirstLetterToUpper(active.cepa);
        }
        string salto = "\n\n";

        if (active.pais == null || active.pais.Length < 2)
            title_paisField.gameObject.SetActive(false);
        else
        {
            title_paisField.gameObject.SetActive(true);
            paisField.text = Utils.SetFirstLetterToUpper(active.pais);
        }

        if (active.brand == null || active.brand.Length < 2)
            title_brandField.gameObject.SetActive(false);
        else
        {
            title_brandField.gameObject.SetActive(true);
            marcaField.text = Utils.SetFirstLetterToUpper(active.brand);
        }

        textField.text = "\n" + active.text;
        textField.text += "\n";

        bool show_guarda_temp = false;
        if (active.tiempo_guardia != null && active.tiempo_guardia != "")
        {
            guardaField.text = active.tiempo_guardia;
            guarda.SetActive(true);
            show_guarda_temp = true;
        } else guarda.SetActive(false);

        if (active.temp != null && active.temp != "")
        {
            temp.SetActive(true);
            tempField.text = active.temp;
            show_guarda_temp = true;
        } else temp.SetActive(false);

        if(show_guarda_temp)
            guarda_temp.SetActive(true);
        else
            guarda_temp.SetActive(false);

        StartCoroutine(Data.Instance.imagesLoader.C_LoadImage(active.id, 200, 200, OnLoaded, "large") );

        SetFoods();
        SetPremios();
    }
    void OnLoaded(Sprite sprite)
    {
        bottle.SetActive(false);
        image.sprite = sprite;
        image.SetNativeSize();
        image.GetComponent<Animation>().Play();
    }
    void SetFoods()
    {
        Utils.RemoveAllChildsIn(foodsContainer);
        List<string> all = Data.Instance.foodsData.GetFoodsByTag(active.tags);
        int qty = all.Count;
       // print("SEt foods qty by tags: " + qty + " active.cepa: " + active.cepa);
        if (qty < 4)
        {
            List<string> allCepas = Data.Instance.foodsData.GetFoodByCepa(active.cepa);
            foreach (string s in allCepas)
            {
                if (qty < 4)
                    all.Add(s);
                qty++;
                //print("Agrega por cepa: " + active.cepa + " : " + s);
            }
        }

        if (all != null && all.Count < 1)
            foods.SetActive(false);
        else
        {
            added.Clear();
            foreach (string s in all)
            {
                if(!AlreadyAdded(s))
                {
                    added.Add(s);
                    FoodIconUI fi = Instantiate(foodIcon, foodsContainer);
                    fi.Init(s);
                }
            }
            foods.SetActive(true);
        }
    }
    public List<string> added;
    bool AlreadyAdded(string newName)
    {
        foreach (string s in added)
            if (s == newName)
                return true;
        return false;
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
