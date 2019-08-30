using UnityEngine;
using UnityEngine.UI;

public class BluetoothControl : MonoBehaviour
{

    public Text textDevices;
#if UNITY_EDITOR
#elif UNITY_ANDROID
    AnroidJavaObject _activity;
#endif
    //Use this for initialization
    void Start()
    {
#if UNITY_EDITOR
        SerialPortControl.GetInstance().OpenPort("COM3", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
        textDevices.text="";
#elif UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        _activity=jc.GetStatic<AndroidJavaObject>("currentActivity");

        Debug.Log("make _activity");
#endif
    }

    public void CallBluetoothInit()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        _activity.Call("BluetoothInit");
#endif
    }

    public void CallConnectedDevice(string name)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        _activity.Call("ConnectedDevice", name);
#endif
    }

    public void ErrorMessage(string errorMsg)
    {
        string msg = errorMsg;

        Debug.Log("ErrorMessage: " + errorMsg);
    }

    public void SearchDevice(string devices)
    {
        string[] device = devices.Split(',');

        for(int i=0; i<devices.Length; i++)
        {
            Debug.Log("SearchDevice: " + device[i]);
        }
    }
    
    public void ReceiveData(string str)
    {
        Debug.Log("Data: " + str);
        textDevices.text = str;
    }

    public void SendData(string str)
    {
#if UNITY_EDITOR
        SerialPortControl.GetInstance().SendData(str);
#endif
    }

}