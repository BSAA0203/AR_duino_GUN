using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidWrapper : MonoBehaviour
{

    private AndroidJavaObject androidJavaObject = null;
    public Text message;
    bool bluetoothEnabled = false;
    AndroidJavaClass jclass;
    AndroidJavaObject activity;

    private AndroidJavaObject GetAndroidJavaObject()
    {
        if (androidJavaObject == null)
        {
            androidJavaObject = new AndroidJavaObject("com.plugin.bluetooth.BluetoothControl");
        }
        return androidJavaObject;
    }

    // Use this for initialization
    void Start()
    {
        // Retrieve current Android Activity from the Unity Player
        jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jclass.GetStatic<AndroidJavaObject>("currentActivity");

        this.androidJavaObject = GetAndroidJavaObject();
        this.androidJavaObject.Call("setActivity", activity);
		try
        {
        	this.message.text = CallInit(this.androidJavaObject);
            if (this.message.text == "initialized")
            {
                if (this.getAvailableDeviceBluetoothState(this.androidJavaObject))
                {
                    this.message.text = "Bluetooth available";

                    bluetoothEnabled = this.isBluetoothEnabled(this.androidJavaObject);
                    this.message.text = "Bluetooth enabled: " + bluetoothEnabled;
                    if(bluetoothEnabled) {
                    	this.message.text = "Bluetooth already turned on";
                    }
                    else {
						bluetoothEnabled = this.turnOnBluetooth(this.androidJavaObject);
                    	this.message.text = "Bluetooth force turned on";
                    }
                }
                else
                {
                    this.message.text = "Bluetooth not available";
                }
            }
        }
        catch (UnityException e)
        {
            this.message.text = e.Message;
        }

		try 
		{
			if(bluetoothEnabled) {
				string pairedDeviceListStr = this.getPairedDevices(this.androidJavaObject);
				this.message.text = pairedDeviceListStr;
				Debug.Log("paired list:" + this.message.text);
				PairedDevicesListModel pairedDeviceList = JsonUtility.FromJson<PairedDevicesListModel>(pairedDeviceListStr);
				Debug.Log("list?: " + (pairedDeviceList != null));
				if(pairedDeviceList.devices == null) {
					pairedDeviceList.devices = new List<PairedDeviceModel>();
				}
				this.message.text = "Json parsed list size: " + pairedDeviceList.devices.Count;

				this.message.text = "Test connect result: " + this.testConnectToTarget(this.androidJavaObject, "98:D3:51:F9:4C:63");

				this.message.text = "Connect: " + this.connectToTarget(this.androidJavaObject, "98:D3:51:F9:4C:63");
				// this.message.text = "Disconnect: " + this.disconnect(this.androidJavaObject);

				this.message.text = "Listen: " + this.startListen(this.androidJavaObject);

				

				// this.message.text = "Disconnect: " + this.disconnect(this.androidJavaObject);
			}
		}
        catch (UnityException e)
        {
            this.message.text = e.Message;
        }
    }

	string getLastData(AndroidJavaObject activity) {
		return activity.Call<string>("getLastData");
	}

	string CallInit(AndroidJavaObject activity) {
		return activity.Call<string>("init");
	}

	bool getAvailableDeviceBluetoothState(AndroidJavaObject activity) {
		return activity.Call<bool>("getDeviceBluetoothState");
	}

	bool isBluetoothEnabled(AndroidJavaObject activity) {
		return activity.Call<bool>("isBluetoothEnabled");
	}

	bool turnOnBluetooth(AndroidJavaObject activity) {
		return activity.Call<bool>("toggleBluetooth", true);
	}

	string getPairedDevices(AndroidJavaObject activity) {
		return activity.Call<string>("getPairedDevices");
	}

	bool testConnectToTarget(AndroidJavaObject activity, string address) {
		return activity.Call<bool>("testConnectToTarget", address);
	}

	bool connectToTarget(AndroidJavaObject activity, string address) {
		return activity.Call<bool>("connectToTarget", address);
	}

	bool disconnect(AndroidJavaObject activity) {
		return activity.Call<bool>("disconnect");
	}

	bool startListen(AndroidJavaObject activity) {
		return activity.Call<bool>("startListen");
	}

    // Update is called once per frame
    void Update()
    {
        this.message.text = "Message: " + this.getLastData(this.androidJavaObject);
    }
}
