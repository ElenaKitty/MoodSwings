#include <SoftwareSerial.h>
#include <Wire.h>
#include <LiquidCrystal.h>

const int previousPin = 8;
const int playPin = 7;
const int nextPin = 4;


String artist = "";
String track = "";
String function = "";
String color = "";
String prevAction = "";
int Speed = 0;


SoftwareSerial wifi(12, 11);
LiquidCrystal lcd(6, 10, 9, 5, 3, 2);

bool DEBUG = true;   //show more logs
int responseTime = 10; //communication timeout

bool checking = false;
bool blinkLight = false;
bool fadeLight = false;
bool snakeLight = false;
bool randomLight = false;
bool waveLight = false;

class WifiModule
{
  public:
    WifiModule();
    String sendToWifi(String command, const int timeout, boolean debug);
    void checkInput();
  private:
    void checkCharacters();
};
WifiModule module;
void setup()
{
  Wire.begin();
  lcd.begin(16, 2);
  Serial.begin(9600);
  wifi.begin(9600);

  pinMode(previousPin, INPUT);
  pinMode(playPin, INPUT);
  pinMode(nextPin, INPUT);

  module.sendToWifi("AT", responseTime, DEBUG);
  delay(100);
  module.sendToWifi("AT+CIFSR", responseTime, DEBUG);
  delay(100);
  module.sendToWifi("AT+CIPMUX=1", responseTime, DEBUG);
  delay(100);
  module.sendToWifi("AT+CIPSERVER=1,80", responseTime, DEBUG);
}
void loop()
{
  checking = true;
  while (checking)
  {
    module.checkInput();
    if (digitalRead(previousPin))
    {
      while (digitalRead(previousPin));
      module.sendToWifi("AT+CIPSEND=0,8", responseTime, DEBUG);
      delay(100);
      module.sendToWifi("Previous", responseTime, DEBUG);
      delay(100);
    }
    if (digitalRead(playPin))
    {
      while (digitalRead(playPin));
      if (blinkLight == false && fadeLight == false)
      {
        if (prevAction == "blink")
        {
          blinkLight = true;
        }
        else if (prevAction == "fade")
        {
          fadeLight = true;
        }
      }
      else if (blinkLight)
      {
        blinkLight = false;
        prevAction = "blink";
      }
      else if (fadeLight)
      {
        fadeLight = false;
        prevAction = "fade";
      }
      module.sendToWifi("AT+CIPSEND=0,12", responseTime, DEBUG);
      delay(100);
      module.sendToWifi("Resume/Pause", responseTime, DEBUG);

//      //testing
//      String info = "#clear$";
//      int str_length = info.length() + 1;
//      char charArray[str_length];
//      info.toCharArray(charArray, str_length);
//      Wire.beginTransmission(4);
//      Wire.write(charArray);
//      Wire.endTransmission();

    }
    if (digitalRead(nextPin))
    {
      while (digitalRead(nextPin));
      module.sendToWifi("AT+CIPSEND=0,4", responseTime, DEBUG);
      delay(100);
      module.sendToWifi("Next", responseTime, DEBUG);
    }
  }
  if (function == "blink")
  {
    blinkLight = true;
    fadeLight = false;
    snakeLight = false;
    randomLight = false;
    waveLight = false;
  }
  if (function == "fade")
  {
    fadeLight = true;
    blinkLight = false;
    snakeLight = false;
    randomLight = false;
    waveLight = false;
  }
  if (function == "snake")
  {
    fadeLight = false;
    blinkLight = false;
    snakeLight = true;
    randomLight = false;
    waveLight = false;
  }
  if (function == "random")
  {
    fadeLight = false;
    blinkLight = false;
    snakeLight = false;
    randomLight = true;
    waveLight = false;
  }
  if (function == "wave")
  {
    fadeLight = false;
    blinkLight = false;
    snakeLight = false;
    randomLight = false;
    waveLight = true;
  }
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print(artist);
  lcd.setCursor(0, 1);
  lcd.print(track);
  String info = "#" + function + "^" + Speed + "^"  + color + "^$";
  int str_length = info.length() + 1;
  char charArray[str_length];
  info.toCharArray(charArray, str_length);
  //  Serial.println(charArray);
  //  if (blinkLight)
  //  {
  Wire.beginTransmission(4);
  Wire.write(charArray);
  Wire.endTransmission();
  //}
  //  if (fadeLight)
  //  {
  //    Wire.beginTransmission(4);
  //    Wire.write("fade");
  //    Wire.endTransmission();
  //  }
  //  if (snakeLight)
  //  {
  //    Wire.beginTransmission(4);
  //    Wire.write("snake");
  //    Wire.endTransmission();
  //  }
  //  if (randomLight)
  //  {
  //    Wire.beginTransmission(4);
  //    Wire.write("random");
  //    Wire.endTransmission();
  //  }
  //  if (waveLight)
  //  {
  //    Wire.beginTransmission(4);
  //    Wire.write("wave");
  //    Wire.endTransmission();
  //
  //  }
}
