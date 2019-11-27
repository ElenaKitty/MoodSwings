const int previousPin = 3;
const int playPin = 4;
const int nextPin = 5;
const int rxPin = 2;
const int txPin = 1;

const int redPin = 9;
const int greenPin = 10;
const int bluePin = 11;

int r = 0;
int g = 0;
int b = 0;
int blinkSpeed = 50;
int fadeSpeed = 1;

char receiveVal;   
bool playing = true;
String prevAction = "";

void setup() 
{
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(previousPin, INPUT);
  pinMode(playPin, INPUT);
  pinMode(nextPin, INPUT);

  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  noLight();
}
//255 = 0; INVERTED COLOR VALUES
void loop() 
{       
  if(Serial.available() > 0)  
  {          
    receiveVal = Serial.read();      
    if(receiveVal == 'f')
    {
     fadeColors(fadeSpeed);
    }
    if(receiveVal == 'b')
    {
     blinkLight(blinkSpeed);
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
  }
  if(digitalRead(nextPin))
  {
    while(digitalRead(nextPin));
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
      while(digitalRead(playPin));
      playing = !playing;
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
      while(digitalRead(playPin));
      playing = !playing;
      analogWrite(redPin, 255);
      analogWrite(greenPin, 255);
      analogWrite(bluePin, 255); 
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
