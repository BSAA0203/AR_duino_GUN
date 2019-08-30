package com.plugin.bluetooth;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Intent;
import android.util.Log;

import com.plugin.bluetooth.model.BluetoothDeviceModel;
//import com.plugin.bluetooth.model.BluetoothDevices;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class BluetoothControl {
    private static final UUID BTMODULEUUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");
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

    public List<BluetoothDeviceModel> getPairedDevices() {
        if(this.bluetoothAdapter == null || !this.bluetoothAdapter.isEnabled()) {
            return null;
        }
        List<BluetoothDeviceModel> pairedDevices = new ArrayList<BluetoothDeviceModel>();
        for(BluetoothDevice device : this.bluetoothAdapter.getBondedDevices()) {
            BluetoothDeviceModel deviceModel = new BluetoothDeviceModel();
            deviceModel.address = device.getAddress();
            deviceModel.name = device.getName();

            pairedDevices.add(deviceModel);
        }

        return pairedDevices;
    }

    public String init() {
        Log.d(TAG, "Call by unity");

        this.bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

        return INIT_MSG;
    }
}
