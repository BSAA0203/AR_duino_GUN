#include <SoftwareSerial.h>// import the serial library
 
SoftwareSerial bluetoothSerial(10, 11); // RX, TX
int BluetoothData; // the data given from Computer
int triggerBtnPin = 2;
int triggerStatus = 0;
bool isShoot = false;

void setup() {
  // put your setup code here, to run once:
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(triggerBtnPin, INPUT_PULLUP);
  digitalWrite(LED_BUILTIN, LOW);
  bluetoothSerial.begin(9600);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  // watch bullet trigger status.
  triggerStatus = digitalRead(triggerBtnPin);

  // normal bullet trigger status
  // which is not shoot
  if(triggerStatus == HIGH) {
    digitalWrite(LED_BUILTIN, HIGH);
    // set isShoot false so that user can shoot it later
    isShoot = false;
  }
  // bullet trigger clicked
  else {
    digitalWrite(LED_BUILTIN, LOW);
    // if user not shooted before
    if(!isShoot) {
      // if bluetooth connection is available
      if(bluetoothSerial.available()) {
        // send 'shoot' string to app
      bluetoothSerial.write("shoot\n");
      bluetoothSerial.flush();
      Serial.println("Shoot sent using bluetooth.");
    }
    // if bluetooth connection not available
    else {
      Serial.println("Bluetooth not available.");
    }
    // set isShoot is shooted
    isShoot = true;
    delay(3000);
    }
    // if isShoot set already shooted before
    else {
      Serial.println("Shoot later.");
      digitalWrite(LED_BUILTIN, HIGH);
      delay(1500);
      digitalWrite(LED_BUILTIN, LOW);
      delay(1500);
      isShoot = false;
    }
  }
}
