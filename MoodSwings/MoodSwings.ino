#include <SoftwareSerial.h>

const int previousPin = 4;
const int playPin = 5;
const int nextPin = 6;

const int redPin = 9;
const int greenPin = 10;
const int bluePin = 11;

int r = 0;
int g = 0;
int b = 0;
int blinkSpeed = 200;
int fadeSpeed = 1;

String receiveVal;   
bool playing = true;
String prevAction = "";

SoftwareSerial wifi(8,7);

bool DEBUG = true;   //show more logs
int responseTime = 10; //communication timeout

void setup() 
{  
  Serial.begin(115200);
  wifi.begin(115200);
  pinMode(previousPin, INPUT);
  pinMode(playPin, INPUT);
  pinMode(nextPin, INPUT);

  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  noLight();
  sendToWifi("AT",responseTime,DEBUG);
  delay(100);
  sendToWifi("AT+CIFSR",responseTime,DEBUG);
  delay(100);
 sendToWifi("AT+CIPMUX=1",responseTime,DEBUG);
 delay(100);
 sendToWifi("AT+CIPSERVER=1,80",responseTime,DEBUG);
}
//255 = 0; INVERTED COLOR VALUES
void loop() 
{       
  if(wifi.available() > 0)  
  {          
    receiveVal = wifi.readString();
    if(receiveVal.substring(11,15) == "fade")
    {
     fadeColors(fadeSpeed);
    }
    if(receiveVal.substring(11,16) == "blink")
    {
     blinkLight(blinkSpeed);
    }
    if(receiveVal.substring(11,20) == "christmas")
    {
      christmasLight(blinkSpeed);
    }
  }           
  
  if(digitalRead(previousPin))
  {
    while(digitalRead(previousPin));
  }
  if(digitalRead(playPin))
  {
    while(digitalRead(playPin));
    playing = !playing;
    if(prevAction == "blink")
    {
      blinkLight(blinkSpeed);
    }
    else if(prevAction == "fade")
    {
      fadeColors(fadeSpeed);
    }
    else if(prevAction == "christmas")
    {
      christmasLight(blinkSpeed);
    }
  }
  if(digitalRead(nextPin))
  {
    while(digitalRead(nextPin));
  }
}
void christmasLight(int blinkSpeed)
{
  playing = true;
  while(playing)
  {
    digitalWrite(redPin, HIGH);
    digitalWrite(greenPin, LOW);
    delay(blinkSpeed);
    digitalWrite(greenPin, HIGH);
    digitalWrite(redPin, LOW);
    delay(blinkSpeed);
    if(digitalRead(playPin))
    {
      pauseAction();
      noLight();
      prevAction = "christmas";
         break;
    }
  }
}

void blinkLight(int blinkSpeed)
{
  playing = true;
  while(playing)
  {
    digitalWrite(redPin, LOW);
    delay(blinkSpeed);
    digitalWrite(redPin, HIGH);
    delay(blinkSpeed);
    if(digitalRead(playPin))
    {
      pauseAction();
      noLight();
      prevAction = "blink";
      break;
    }
  }
}

void fadeColors(int fadeSpeed)
{
  playing = true;
  while(playing)
  {
    setColor(0, 255, 255, fadeSpeed);    // red
    setColor(255, 0, 255, fadeSpeed);    // green
    setColor(255, 255, 0, fadeSpeed);    // blue
    setColor(0, 0, 255, fadeSpeed);      // yellow
    setColor(175, 255, 175, fadeSpeed);  // purple
    setColor(255, 255, 255, fadeSpeed);  //white
  }
}

void setColor(int red, int green, int blue, int fadeSpeed) 
{
  while ( r != red || g != green || b != blue ) 
  {
    if ( r < red ) r += 1;
    if ( r > red ) r -= 1;

    if ( g < green ) g += 1;
    if ( g > green ) g -= 1;

    if ( b < blue ) b += 1;
    if ( b > blue ) b -= 1;

    if(digitalRead(playPin))
    {
      pauseAction();
      noLight();
      prevAction = "fade";
      break;
    }
    analogWrite(redPin, r);
    analogWrite(greenPin, g);
    analogWrite(bluePin, b); 

    delay(fadeSpeed);
//    delay(8) //sweetspot    
  }
}
void noLight()
{
  analogWrite(redPin, 255);
  analogWrite(greenPin, 255);
  analogWrite(bluePin, 255);
}
void pauseAction()
{
   while(digitalRead(playPin));
    playing = !playing;
    noLight();
}

String sendToWifi(String command, const int timeout, boolean debug){
 String response = "";
 wifi.println(command); // send the read character to the esp8266
 long int time = millis();
 while( (time+timeout) > millis())
 {
   while(wifi.available())
   {
   // The esp has data so display its output to the serial window 
   char c = wifi.read(); // read the next character.
   response+=c;
   }  
 }
 if(debug)
 {
   Serial.println(response);
 }
 return response;
}
