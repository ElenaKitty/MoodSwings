#include <SoftwareSerial.h>

const int previousPin = 4;
const int playPin = 7;
const int nextPin = 8;

const int redPin = 3;
const int greenPin = 5;
const int bluePin = 6;

int r = 0;
int g = 0;
int b = 0;

String receiveVal;
String prevAction = "";

String artist;
String track;
String function;
int Speed;
 
SoftwareSerial wifi(12, 11);

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
  module.checkInput();
  if (function == "blink")
  {
    blinkLight = true;
  }
  if (function == "fade")
  {
    fadeLight = true;
  }
  if (blinkLight)
  {
    function = "";
    effect.blinkLight(Speed);
  }
  if (fadeLight)
  {
    function = "";
    effect.fadeColors(Speed);
  }
  if (digitalRead(previousPin))
  {
    while (digitalRead(previousPin));
    Serial.println("Previous");
    module.sendToWifi("AT+CIPSEND=0,8", responseTime, DEBUG);
    delay(100);
    module.sendToWifi("Previous", responseTime, DEBUG);
    delay(100);
  }
  if (digitalRead(playPin))
  {
    while (digitalRead(playPin));
    Serial.println("Play");
    if (blinkLight == false && fadeLight == false)
    {
      if (prevAction == "blink")
      {
        blinkLight = true;
        effect.blinkLight(Speed);
      }
      else if (prevAction == "fade")
      {
        fadeLight = true;
        effect.fadeColors(Speed);
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
  }
  if(digitalRead(nextPin))
  {
    while(digitalRead(nextPin));
    module.sendToWifi("AT+CIPSEND=0,4", responseTime, DEBUG);
    delay(100);
    module.sendToWifi("Next", responseTime, DEBUG);
  }
}
