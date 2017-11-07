//Battery test 
//this test will set the tap frequency of each relay 
//1) sign in to account on EPD
//2) put the battery life script on the device 
//3) fully charge the EPD
//4) connect device to Arduino setup

//*******************************************************************************

//*******************************************************************************

#include "Relay.h" 

int myTimeArray[] = {60,60,60,60,60,60,60,60}; //set time interval of relay here (in seconds)
int myToggleArray[] = {0,0,1,2,1,2,0,0}; //if there is a device that has two touchpoints toggle here 

//example
//if set like this
//int myTimeArray[] = {30,60,60,60,60,60,5,60};
//int myToggleArray[] = {0,0,1,2,1,2,0,0};
//first relay will tap once every 30 seconds and 7th relay will tap every 5 seconds
//but the rest will tap once every 60 seconds
//relay 3 & 4 will toogle (between state 1 and 2) meaning relay 3 will click at 60 seconds and relay 4 will click at 120 seconds
//same with relay 5 & 6, they will 
//toggle will be useful for flipping within a book with two tap points 

void setup() {
  Serial.begin(9600);
  pinMode(2, OUTPUT);
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
}
  

void loop() {
   currentTime = millis()/1000;
   Serial.print("currentTime: ");
   Serial.println(currentTime);
  for(int i=0; i<sizeof(myTimeArray); i++){
   if(currentTime%myTimeArray[i] == 0 && myToggleArray[i] == 0){
      Serial.print("index: ");
      Serial.println(i);
      digitalWrite(i+2, HIGH);
    }   
    else if(currentTime%myTimeArray[i] == 0 && myToggleArray[i] == 1){
      Serial.print("index: ");
      Serial.println(i);
      digitalWrite(i+2, HIGH);
      myToggleArray[i] = 2;
    }
    else if(currentTime%myTimeArray[i] == 0 && myToggleArray[i] == 2){
      myToggleArray[i] = 1;
    }
    
  }
  delay(300);
  digitalWrite(2, LOW);
  digitalWrite(3, LOW);
  digitalWrite(4, LOW);
  digitalWrite(5, LOW);
  digitalWrite(6, LOW);
  digitalWrite(7, LOW);
  digitalWrite(8, LOW);
  digitalWrite(9, LOW);
  delay(700);
}


