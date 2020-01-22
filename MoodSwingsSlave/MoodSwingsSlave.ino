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

unsigned long interval = 5;
unsigned long snakeInterval = 20;
unsigned long blinkInterval = 100;
unsigned long waveInterval = 20;

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
    void waveLeds(uint16_t color);
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
  else if (received == "fade" || (fading && received == ""))
  {
    Serial.println("fading");
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
  else if (received == "snake" || (snaking && received == ""))
  {
    snaking = true;
    fading = false;
    blinking = false;
    randoming = false;
    waveing = false;
    effects.snakeLeds();
    received = "";
  }
  else if (received == "random" || (randoming && received == ""))
  {
    snaking = false;
    fading = false;
    blinking = false;
    randoming = true;
    waveing = false;
    effects.randomLeds();
    received = "";
  }
  else if (received == "wave" || (waveing && received == ""))
  {
    snaking = false;
    fading = false;
    blinking = false;
    randoming = false;
    waveing = true;
    effects.waveLeds(red);
    received = "";
  }
}

void receiveEvent(int howMany)
{
  if (Wire.available() > 0)
  {
    matrix.fillScreen(matrix.Color888(0, 0, 0));
  }
  while (0 < Wire.available()) // loop through all but the last
  {
    char c = Wire.read(); // receive byte as a character
    received += c;
  }
}

void receiveSerial()
{
  if (Serial.available() > 0)
  {
    matrix.fillScreen(matrix.Color888(0, 0, 0));
  }
  while (0 < Serial.available()) // loop through all but the last
  {
    char c = Serial.read(); // receive byte as a character
    received += c;
  }
}

void softwareReset( uint8_t prescaller) {
  // start watchdog with the provided prescaller
  wdt_enable( prescaller);
  // wait for the prescaller time to expire
  // without sending the reset signal by using
  // the wdt_reset() method
  while (1) {}
}
