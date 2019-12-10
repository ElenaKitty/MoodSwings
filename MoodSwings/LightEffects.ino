LightEffects::LightEffects()
{
  
}

void LightEffects::blinkLight(int blinkSpeed)
{
    digitalWrite(redPin, LOW);
    delay(blinkSpeed);
    digitalWrite(redPin, HIGH);
    delay(blinkSpeed);
}

void LightEffects::fadeColors(int fadeSpeed)
{
    setColor(0, 255, 255, fadeSpeed);    // red
    setColor(255, 0, 255, fadeSpeed);    // green
    setColor(255, 255, 0, fadeSpeed);    // blue
    setColor(0, 0, 255, fadeSpeed);      // yellow
    setColor(175, 255, 175, fadeSpeed);  // purple
    setColor(255, 255, 255, fadeSpeed);  //white
}

void LightEffects::setColor(int red, int green, int blue, int fadeSpeed) 
{
  while ( r != red || g != green || b != blue ) 
  {
    if ( r < red ) r += 1;
    if ( r > red ) r -= 1;

    if ( g < green ) g += 1;
    if ( g > green ) g -= 1;

    if ( b < blue ) b += 1;
    if ( b > blue ) b -= 1;
    
    analogWrite(redPin, r);
    analogWrite(greenPin, g);
    analogWrite(bluePin, b); 

    delay(fadeSpeed);
//    delay(8) //sweetspot    
  }
}
void LightEffects::noLight()
{
  analogWrite(redPin, 255);
  analogWrite(greenPin, 255);
  analogWrite(bluePin, 255);
}
