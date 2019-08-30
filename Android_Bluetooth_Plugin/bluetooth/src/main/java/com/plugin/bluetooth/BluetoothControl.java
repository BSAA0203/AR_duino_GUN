package com.plugin.bluetooth;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.content.Intent;
import android.util.Log;

public class BluetoothControl {

    public static String TAG = "Bluetooth Plugin";
    public static String INIT_MSG = "initialized";
    public static int REQUEST_ENABLE_BT = 0;

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

    public boolean toggleBluetooth(boolean state) {
        if(this.bluetoothAdapter == null || this.activity == null) {
            return false;
        }
        if(state) {
            this.bluetoothAdapter.enable();
//            Intent i = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
//            activity.startActivityForResult(i, REQUEST_ENABLE_BT);
        }
        else {
            this.bluetoothAdapter.disable();
        }
        return true;
    }

    public boolean isBluetoothEnabled() {
        return this.bluetoothAdapter != null ? this.bluetoothAdapter.isEnabled() : false;
    }

    public String init() {
        Log.d(TAG, "Call by unity");

        this.bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

        return INIT_MSG;
    }
}
