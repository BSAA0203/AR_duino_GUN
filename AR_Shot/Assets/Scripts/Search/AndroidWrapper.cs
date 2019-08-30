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
				List<PairedDeviceModel> pairedDeviceList = this.getPairedDevices(this.androidJavaObject);
				this.message.text = "paired device list size: " + pairedDeviceList.Count;
				if(pairedDeviceList == null) {
					this.message.text = "Empty";
				}
				else {
				this.message.text = pairedDeviceList.ToString();
				}
			}
		}
        catch (UnityException e)
        {
            this.message.text = e.Message;
        }
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

	List<PairedDeviceModel> getPairedDevices(AndroidJavaObject activity) {
		return activity.Call<List<PairedDeviceModel>>("getPairedDevices");
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
