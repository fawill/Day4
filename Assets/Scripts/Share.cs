using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Share : MonoBehaviour
{
    private GameObject canvas;
    private bool isProcessing = false;

    void Start()
    {
        canvas = transform.parent.gameObject;
    }

    public void shareText()
    {
#if UNITY_ANDROID
        if(!isProcessing)
        {
            StartCoroutine(ShareOnAndroid());
        }
#endif
    }

    IEnumerator ShareOnAndroid()
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();

        string destination = CaptureScreenShot();

        //Android Specific
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");        
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");        
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
        intentObject.Call<AndroidJavaObject>("setType", "image/png");

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        
        currentActivity.Call("startActivity", intentObject);

        isProcessing = false;
    }

    string CaptureScreenShot()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();
        canvas.GetComponent<Canvas>().enabled = true;
        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);
        return destination;
    }
}
