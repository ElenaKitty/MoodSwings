#include <RGBmatrixPanel.h>
#include <Wire.h>

#include <avr/wdt.h>

#define CLK  8
#define OE   9
#define LAT 10
#define A   A0
#define B   A1
#define C   A2
#define F2(progmem_ptr) (const __FlashStringHelper *)progmem_ptr

RGBmatrixPanel matrix(A, B, C, CLK, LAT, OE, false);

uint16_t red = matrix.Color888(255, 0, 0);
uint16_t orange = matrix.Color888(255, 127, 0);
uint16_t yellow = matrix.Color888(255, 255, 0);
uint16_t green = matrix.Color888(0, 255, 0);
uint16_t blue = matrix.Color888(0, 0, 255);
uint16_t indigo = matrix.Color888(46, 43, 95);
uint16_t violet = matrix.Color888(139, 0, 255);

uint16_t rainbow[] = {red, orange, yellow, green, blue, indigo, violet};

int r, g, b = 0;
int hue = 0;
int saturation = 255;
int value = 255;

bool blinking = false;
bool fading = false;
bool snaking = false;
bool randoming = false;
bool waveing = false;

bool blinkState = false;

unsigned long currentMillis = 0;

unsigned long previousMillis = 0;
unsigned long previousBlinkMillis = 0;
unsigned long previousSnakeMillis = 0;
unsigned long previousWaveMillis = 0;
unsigned long previousRandomMillis = 0;

unsigned long interval = 0; //5
unsigned long snakeInterval = 0; //20
unsigned long blinkInterval = 0; //100
unsigned long waveInterval = 0; //20
unsigned long randomInterval = 0; //1

bool redBlink = false;
bool blueBlink = false;
bool greenBlink = false;

int snakeRow = 0;
int snakeColumn = -5;
int waveColumn = 0;

class LightEffects
{
  public:
    LightEffects();
    void waveLeds();
    void snakeLeds();
    void randomLeds();
    void blinkLeds(uint16_t color);
    void fadeColors();
  private:
    void drawVertical(int x, uint16_t color);
};
LightEffects effects;
String received = "";

int i = 0;
//bool executed;
void setup() {
  Wire.begin(4);                // join i2c bus with address #4
  Wire.onReceive(receiveEvent); // register event
  Serial.begin(9600);           // start serial for output

  matrix.begin();
}

void loop()
{
  currentMillis = millis();
  receiveSerial();

  if (received != "")
  {
    Serial.println("Received: " + received);
  }
  if (received.indexOf("blink") > -1 || (blinking && received == ""))
  {
    if (currentMillis - previousBlinkMillis > blinkInterval)
    {
      previousBlinkMillis = currentMillis;
      if (received.indexOf("red") > -1)
      {
        redBlink = true;
        greenBlink = false;
        blueBlink = false;
        effects.blinkLeds(red);
        received = "";
      }
      else if (received.indexOf("green") > -1)
      {
        greenBlink = true;
        redBlink = false;
        blueBlink = false;
        effects.blinkLeds(green);
        received = "";
      }
      else if (received.indexOf("blue") > -1)
      {
        blueBlink = true;
        greenBlink = false;
        redBlink = false;
        effects.blinkLeds(blue);
        received = "";
      }
      else if (redBlink && blinking)
      {
        effects.blinkLeds(red);
      }
      else if (greenBlink && blinking)
      {
        effects.blinkLeds(green);
      }
      else if (blueBlink && blinking)
      {
        effects.blinkLeds(blue);
      }
    }
    blinking = true;
    fading = false;
    snaking = false;
    randoming = false;
    waveing = false;
  }
  else if (received.indexOf("fade") > -1 || (fading && received == ""))
  {
    fading = true;
    received = "";
    blinking = false;
    snaking = false;
    randoming = false;
    waveing = false;
    if (currentMillis - previousMillis > interval)
    {
      previousMillis = currentMillis;
      effects.fadeColors();
    }
  }
  else if (received.indexOf("snake") > -1 || (snaking && received == ""))
  {
    snaking = true;
    fading = false;
    blinking = false;
    randoming = false;
    waveing = false;
    effects.snakeLeds();
    received = "";
  }
  else if (received.indexOf("random") > -1 || (randoming && received == ""))
  {
    received = "";
    snaking = false;
    fading = false;
    blinking = false;
    randoming = true;
    waveing = false;
    if(currentMillis - previousRandomMillis > randomInterval)
    {
      previousRandomMillis = currentMillis;
      effects.randomLeds();
    }
  }
  else if (received.indexOf("wave") > -1 || (waveing && received == ""))
  {
    snaking = false;
    fading = false;
    blinking = false;
    randoming = false;
    waveing = true;
    effects.waveLeds();
    received = "";
  }
}

void receiveEvent(int howMany)
{
  matrix.fillScreen(0);
  while (Wire.available() > 0) // loop through all but the last
  {
    char c = Wire.read(); // receive byte as a character
    received += c;
    Serial.println(received);
  }
  int beginInput = received.indexOf('#');
  int endInput = received.indexOf('~');
  String information = received.substring(beginInput + 1, endInput);
  String part = "";
  String function = "";
  int i = 0;
  while (information.indexOf('^') != -1)
  {
    part = information.substring(0, information.indexOf('^'));
    if (i == 0)
    {
      function = part;
    }
    if (i == 1)
    {
      if (function == "blink")
      {
        blinkInterval = part.toInt();
      }
      else if (function == "fade")
      {
        interval = part.toInt();
      }
      else if (function == "snake")
      {
        snakeInterval = part.toInt();
      }
      else if (function == "random")
      {
        randomInterval = part.toInt();
      }
      else if (function == "wave")
      {
        waveInterval = part.toInt();
      }
    };
    i++;
    information.remove(0, part.length() + 1);
  }
}

void receiveSerial()
{
  while (0 < Serial.available()) // loop through all but the last
  {
    char c = Serial.read(); // receive byte as a character
    received += c;
    Serial.println(received);
  }
  int beginInput = received.indexOf('#');
  int endInput = received.indexOf('~');
  String information = received.substring(beginInput + 1, endInput);
  String part = "";
  String function = "";
  int i = 0;
  while (information.indexOf('^') != -1)
  {
    part = information.substring(0, information.indexOf('^'));
    if (i == 0)
    {
      function = part;
    }
    if (i == 1)
    {
      if (function == "blink")
      {
        blinkInterval = part.toInt();
      }
      else if (function == "fade")
      {
        interval = part.toInt();
      }
      else if (function == "snake")
      {
        snakeInterval = part.toInt();
      }
      else if (function == "random")
      {
        randomInterval = part.toInt();
      }
      else if (function == "wave")
      {
        waveInterval = part.toInt();
      }
    };
    i++;
    information.remove(0, part.length() + 1);
  }
}
