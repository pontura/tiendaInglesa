using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImagesLoader : MonoBehaviour
{

    //https://images-ti-vm1.tiendainglesa.com.uy/small/P463450-1.jpg

    public IEnumerator C_LoadImage(string imageName, int width, int height, System.Action<Sprite> OnLoad, string size = "small")
    {
        Texture2D texture = new Texture2D(width, height);
        string url = "https://images-ti-vm1.tiendainglesa.com.uy/" + size + "/P" + imageName + "-1.jpg";
      //  Debug.Log("LoadImage: " + url);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
          //  Debug.Log(request.error);
        }            
        else
        {
            texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
            OnLoad(sprite);
        }
        request.Dispose();
    }
}
