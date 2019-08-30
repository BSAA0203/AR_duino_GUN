package com.plugin.bluetooth;

import android.bluetooth.BluetoothSocket;
import android.os.SystemClock;
import android.util.Log;

import java.io.InputStream;
import java.util.logging.Handler;

public class BluetoothConnectionThread extends Thread {

    public static final String TAG = "Unity";

    BluetoothSocket socket;
    InputStream inputStream;
    Handler handler;
    boolean running;

    public BluetoothConnectionThread(BluetoothSocket socket, Handler handler) {
        this.socket = socket;
        this.handler = handler;

        try {
            running = true;
            inputStream = socket.getInputStream();

            Log.d(TAG, "BluetoothConnectionThread init");
        }
        catch (Exception e) {
            Log.e(TAG, "BluetoothConnectionThread: " + e.getMessage());
        }
    }

    @Override
    public void run() {

        Log.d(TAG, "run thread started");
        byte[] buffer = new byte[6];  // buffer store for the stream
        int bytes; // bytes returned from read()
        while(this.running) {
            try {
                bytes = inputStream.available();
                if(bytes != 0) { //pause and wait for rest of data. Adjust this depending on your sending speed.
                    bytes = inputStream.available(); // how many bytes are ready to be read?
                    bytes = inputStream.read(buffer, 0, bytes); // record how many bytes we actually read

                    Log.d(TAG,"read data: " + (new String(buffer)));
//                    mHandler.obtainMessage(MESSAGE_READ, bytes, -1, buffer)
//                            .sendToTarget(); // Send the obtained bytes to the UI activity
                }
                SystemClock.sleep(100);
            }
            catch (Exception e) {
                this.running = false;
                Log.e(TAG,"run: " + e.getMessage());
            }
        }

        Log.d(TAG, "run, bye bye");
    }

    public void stopThread() {
        this.running = false;
    }
}
