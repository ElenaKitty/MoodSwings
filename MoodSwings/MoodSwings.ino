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


String receiveVal;   
String prevAction = "";

String artist;
String track;
String function;
int Speed;

SoftwareSerial wifi(8,7);

bool DEBUG = true;   //show more logs
int responseTime = 10; //communication timeout

bool playing = true;
bool blinkLight = false;
bool fadeLight = false;

class WifiModule
{
  public:
   WifiModule();
   String sendToWifi(String command, const int timeout, boolean debug);
   void checkInput();
  private:
   void checkCharacters();
};
class LightEffects
{
  public:
    LightEffects();
    void noLight();
    void setColor(int red, int green, int blue, int fadeSpeed);
    void fadeColors(int fadeSpeed);
    void blinkLight(int blinkSpeed);
};
WifiModule module;
LightEffects effect;
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
  effect.noLight();
  module.sendToWifi("AT",responseTime,DEBUG);
  delay(100);
  module.sendToWifi("AT+CIFSR",responseTime,DEBUG);
  delay(100);
  module.sendToWifi("AT+CIPMUX=1",responseTime,DEBUG);
  delay(100);
  module.sendToWifi("AT+CIPSERVER=1,80",responseTime,DEBUG);
}
//255 = 0; INVERTED COLOR VALUES
void loop() 
{   
  module.checkInput();
  if(function == "blink")
  {
    blinkLight = true;
  }
  if(function == "fade")
  {
    fadeLight = true;
  }
  if(blinkLight)
  {
    function = "";
    effect.blinkLight(Speed);
  }
  if(fadeLight)
  {
    function = "";
    effect.fadeColors(Speed);
  }
  if(digitalRead(previousPin))
  {
    while(digitalRead(previousPin));
    if(artist != "")
    {
      Serial.println("Artist: " + artist);
    }
    if(track != "")
    {
      Serial.println("Track: " + track);
    }
    if(function != "")
    {
      Serial.println("Function: " + function);
    }
    if(Speed != 0)
    {
      Serial.println(Speed); //werkt
    }
  }
  if(digitalRead(playPin))
  {
    while(digitalRead(playPin));
    Serial.println("Blink: " + blinkLight);
    Serial.println("Fade: " + fadeLight);
    if(blinkLight == false && fadeLight == false)
    {
      if(prevAction == "blink")
      {
        blinkLight = true;
        effect.blinkLight(Speed);
      }
      else if(prevAction == "fade")
      {
        fadeLight = true;
        effect.fadeColors(Speed);
      }
    }
    else if(blinkLight)
    {
      blinkLight = false;
      prevAction = "blink";
    }
    else if(fadeLight)
    {
      fadeLight = false;
      prevAction = "fade";
    }
  }
}
