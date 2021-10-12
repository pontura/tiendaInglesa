using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;

public class ScanSimple : MonoBehaviour
{

    private IScanner BarcodeScanner;
    public Text TextHeader;
    public RawImage Image;
    public AudioSource Audio;

    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    void Start()
    {
        // Create a basic scanner
        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        BarcodeScanner.OnReady += (sender, arg) => {
            // Set Orientation & Texture
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = BarcodeScanner.Camera.GetScale();
            Image.texture = BarcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = Image.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
        };

        // Track status of the scanner
        BarcodeScanner.StatusChanged += (sender, arg) => {
            TextHeader.text = "Status: " + BarcodeScanner.Status;
        };
    }
    void Update()
    {
        if (BarcodeScanner == null)
        {
            return;
        }
        BarcodeScanner.Update();
    }

    #region UI Buttons

    public void ClickStart()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Start");
            return;
        }

        // Start Scanning
        BarcodeScanner.Scan((barCodeType, barCodeValue) => {
            BarcodeScanner.Stop();

            Events.OnScanDone(barCodeValue);
            Debug.Log("Status: " + barCodeType + " value: " +  barCodeValue);

            TextHeader.text = "Found: " + barCodeType + " / " + barCodeValue;

            Audio.Play();

//#if UNITY_ANDROID || UNITY_IOS
//            Handheld.Vibrate();
//#endif
        });
    }

    public void ClickStop()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Stop");
            return;
        }
        BarcodeScanner.Stop();
    }

    public void ClickBack()
    {
        StartCoroutine(StopCamera(() => {
            SceneManager.LoadScene("Boot");
        }));
    }
    public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion
}
