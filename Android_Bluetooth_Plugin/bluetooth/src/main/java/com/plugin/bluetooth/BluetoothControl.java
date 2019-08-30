package com.plugin.bluetooth;

import android.app.Activity;
import android.util.Log;

public class BluetoothControl {

    public static String TAG = "Bluetooth Plugin";
    public static String INIT_MSG = "initialized";
    protected Activity activity;

    public void setActivity(Activity activity) {
        this.activity = activity;
    }

    public String init() {
        Log.d(TAG, "Call by unity");
        return INIT_MSG;
    }
}
