using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeButtons : MonoBehaviour
{
    public List<GameObject> all;
    public float delay = 0.02f;

    public void Init()
    {
        Reset();
        StartCoroutine(InitC());
    }
    public void Add(GameObject go)
    {
        all.Add(go);
        go.GetComponent<Animation>().Play("off");
    }
    IEnumerator InitC()
    {
        yield return new WaitForSeconds(0.1f);
        int i = 0;
        while(i<all.Count)
        {
            GameObject go = all[i];
            go.GetComponent<Animation>().Play("on");
            yield return new WaitForSeconds(delay);
            i++;
        }
    }
    public void Reset()
    {
        all.Clear();
        StopAllCoroutines();
        Utils.RemoveAllChildsIn(transform);
    }
}
