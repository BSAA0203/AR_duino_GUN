package com.plugin.bluetooth;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.content.Intent;
import android.util.Log;

import com.plugin.bluetooth.model.BluetoothDeviceModel;
//import com.plugin.bluetooth.model.BluetoothDevices;

import org.json.JSONArray;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class BluetoothControl {
    private static final UUID BTMODULEUUID = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");
    public static String TAG = "Unity";
    public static String INIT_MSG = "initialized";
    public static int REQUEST_ENABLE_BT = 0;

    protected Activity activity;
    private BluetoothAdapter bluetoothAdapter;
    BluetoothSocket bluetoothSocket;
    private BluetoothConnectionThread thread;

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

    public String getPairedDevices() {
        if(this.bluetoothAdapter == null || !this.bluetoothAdapter.isEnabled()) {
            return null;
        }
        String data = "{\"devices\":[";
//        List<BluetoothDeviceModel> pairedDevices = new ArrayList<BluetoothDeviceModel>();
        for(BluetoothDevice device : this.bluetoothAdapter.getBondedDevices()) {
//            BluetoothDeviceModel deviceModel = new BluetoothDeviceModel();
//            deviceModel.address = device.getAddress();
//            deviceModel.name = device.getName();
//
//            pairedDevices.add(deviceModel);
            data += "{\"address\":\"" + device.getAddress();
            data += "\",\"name\":\"" + device.getName() + "\"},";

        }
        data = data.substring(0, data.length() - 1);
        data +="]}";
//        String data = pairedDevices.toString();
        Log.d(TAG, data);
        return data;
    }

    public boolean testConnectToTarget(String address) {
        try {
            Log.d(TAG, "testConnectToTarget connect to: " + address);
            BluetoothDevice device = this.bluetoothAdapter.getRemoteDevice(address);
            BluetoothSocket socket = device.createRfcommSocketToServiceRecord(BTMODULEUUID);

            if(socket != null) {
                socket.connect();
                if(socket.isConnected()) {
                    Log.d(TAG, "testConnectToTarget test connect success");
                }
                socket.close();
                Log.d(TAG, "testConnectToTarget disconnect test connection");
                return true;
            }
            else {
                Log.d(TAG, "testConnectToTarget socket is null");
            }
        }
        catch (Exception e) {
            Log.d(TAG, "testConnectToTarget: " + e.getMessage());
        }
        return false;
    }

    public boolean connectToTarget(String address) {
        try {
            Log.d(TAG, "connectToTarget connect to: " + address);
            BluetoothDevice device = this.bluetoothAdapter.getRemoteDevice(address);
            bluetoothSocket = device.createRfcommSocketToServiceRecord(BTMODULEUUID);

            if(bluetoothSocket != null) {
                bluetoothSocket.connect();
                boolean status = bluetoothSocket.isConnected();
                Log.d(TAG, "testConnectToTarget isConnected: " + status);
                return status;
            }
            else {
                Log.d(TAG, "connectToTarget socket is null");
            }
        }
        catch (Exception e) {
            Log.d(TAG, "connectToTarget: " + e.getMessage());
        }
        return false;
    }

    public boolean disconnect() {
        try {
            if(bluetoothSocket != null && bluetoothSocket.isConnected()) {
                breakThread();
                bluetoothSocket.close();
                Log.d(TAG, "disconnected successfully");
                return true;
            }
            else {
                Log.d(TAG, "something wrong with connection");
                return false;
            }
        }
        catch (Exception e) {
            Log.d(TAG, "disconnect: " + e.getMessage());
        }
        return false;
    }

    private void breakThread() {
        try {
            thread.stopThread();
            thread.join();
            thread = null;
        }
        catch (Exception e) {
            Log.e(TAG, "breakThread: " + e.getMessage());
        }
    }

    public boolean startListen() {
        try {
            if(thread != null) {
                breakThread();
            }
            thread = new BluetoothConnectionThread(bluetoothSocket, null);
            thread.start();
            return true;
        }
        catch (Exception e) {
            Log.e(TAG, "startListen: " + e.getMessage());
        }
        return false;
    }

    public String init() {
        Log.d(TAG, "Call by unity");

        this.bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();

        return INIT_MSG;
    }
}
