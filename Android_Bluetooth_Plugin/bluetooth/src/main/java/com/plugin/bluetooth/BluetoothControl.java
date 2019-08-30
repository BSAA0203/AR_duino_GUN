package com.plugin.bluetooth;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.util.Log;

public class BluetoothControl {

    public static String TAG = "Bluetooth Plugin";
    public static String INIT_MSG = "initialized";
    protected Activity activity;
    private BluetoothAdapter bluetoothAdapter;

    public void setActivity(Activity activity) {
        this.activity = activity;
    }

    public boolean getDeviceBluetoothState() {
        if(this.bluetoothAdapter == null) {
            return false;
        }
        return true;
    }

    public String init() {
        Log.d(TAG, "Call by unity");

        this.bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

        return INIT_MSG;
    }
}
