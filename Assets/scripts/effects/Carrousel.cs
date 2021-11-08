using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrousel : MonoBehaviour
{
    public float speed = 1;
    public float duration = 3;

    public Image[] all;
    [SerializeField] Transform container;

    public int id;

    private void Awake()
    {
        all = container.GetComponentsInChildren<Image>();
    }
    void OnEnable()
    {
        id = 0;
        Next();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public Image image1;
    public Image image2;

    void Next()
    {
        image1 = all[id];
        id++;

        if (id > all.Length - 1)
            id = 0;

        image2 = all[id];

        StartCoroutine(Fade());


    }
    IEnumerator Fade()
    {
        foreach (Image img in all)
        {
            img.transform.SetParent(transform);
            img.gameObject.SetActive(false);
        }
        Color c = image1.color;
        c.a = 1;
        image1.color = c;

        c = image2.color;
        c.a = 0;
        image2.color = c;
        //image1.fillAmount = 1;
        //image2.fillAmount = 0;

        image1.transform.SetParent(container);
        image2.transform.SetParent(container);

        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        float i = 1;
        while(i>0)
        {
            i -= speed * Time.deltaTime;
            // image2.fillAmount = 1-i;
            c = image2.color;
            c.a = 1-i;
            image2.color = c;
            yield return new WaitForEndOfFrame();
        }

        Next();
    }
}
