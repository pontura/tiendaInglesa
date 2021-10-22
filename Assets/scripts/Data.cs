using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;

public class Data : MonoBehaviour
{
    const string PREFAB_PATH = "Data";
    static Data mInstance = null;
    public bool DEBUG;
    public string lastScene;
    public string newScene;
    public SpreadsheetLoader spreadsheetLoader;
    public WinesData winesData;
    public ImagesLoader imagesLoader;
    public FiltersData filtersData;
    public SommelierData sommelierData;

    public static Data Instance
    {
        get
        {
            return mInstance;
        }
    }
    public void LoadLevel(string aLevelName)
    {
        this.newScene = aLevelName;
         SceneManager.LoadScene(newScene);
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;

        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);

    }





    //internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    //{
    //    Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    //}

    //internal void PermissionCallbacks_PermissionGranted(string permissionName)
    //{
    //    Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    //}

    //internal void PermissionCallbacks_PermissionDenied(string permissionName)
    //{
    //    Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
    //}
    //void Start()
    //{
    //    if (Permission.HasUserAuthorizedPermission(Permission.Camera))
    //    {
    //        // The user authorized use of the microphone.
    //    }
    //    else
    //    {
    //        bool useCallbacks = false;
    //        if (!useCallbacks)
    //        {
    //            // We do not have permission to use the microphone.
    //            // Ask for permission or proceed without the functionality enabled.
    //            Permission.RequestUserPermission(Permission.Camera);
    //        }
    //        else
    //        {
    //            var callbacks = new PermissionCallbacks();
    //            callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
    //            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
    //            callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
    //            Permission.RequestUserPermission(Permission.Camera, callbacks);
    //        }
    //    }
    //}
}
