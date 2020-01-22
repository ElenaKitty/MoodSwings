
LightEffects::LightEffects()
{

}

void LightEffects::drawVertical(int x, uint16_t color)
{
  for (int i = 0; i < 16; i++)
  {
    matrix.drawPixel(x, i, color);
  }
}

void LightEffects::waveLeds()
{
  if (waveColumn == 39)
  {
    waveColumn = 0;
  }
  if (waveColumn < 39)
  {
    if (currentMillis - previousWaveMillis > waveInterval)
    {
      previousWaveMillis = currentMillis;
      drawVertical(waveColumn, rainbow[0]);
      drawVertical(waveColumn - 1, rainbow[1]);
      drawVertical(waveColumn - 2, rainbow[2]);
      drawVertical(waveColumn - 3, rainbow[3]);
      drawVertical(waveColumn - 4, rainbow[4]);
      drawVertical(waveColumn - 5, rainbow[5]);
      matrix.swapBuffers(false);
      drawVertical(waveColumn - 1, 0);
      drawVertical(waveColumn - 2, 0);
      drawVertical(waveColumn - 3, 0);
      drawVertical(waveColumn - 4, 0);
      drawVertical(waveColumn - 5, 0);
      drawVertical(waveColumn - 6, 0);
      waveColumn++;
    }
  }
}
void LightEffects::snakeLeds()
{
  if (snakeRow == 16)
  {
    snakeRow = 0;
  }
  if (snakeColumn <= 32)
  {
    if (currentMillis - previousSnakeMillis > snakeInterval)
    {
      previousSnakeMillis = currentMillis;
      snakeColumn++;
      matrix.drawPixel(snakeColumn, snakeRow, rainbow[0]);
      matrix.drawPixel(snakeColumn + 1, snakeRow, rainbow[1]);
      matrix.drawPixel(snakeColumn + 2, snakeRow, rainbow[2]);
      matrix.drawPixel(snakeColumn + 3, snakeRow, rainbow[3]);
      matrix.drawPixel(snakeColumn + 4, snakeRow, rainbow[4]);
      matrix.drawPixel(snakeColumn + 5, snakeRow, rainbow[5]);
      matrix.drawPixel(snakeColumn + 6, snakeRow, rainbow[6]);

      matrix.drawPixel(snakeColumn + 1, snakeRow - 1, rainbow[1]);
      matrix.drawPixel(snakeColumn + 2, snakeRow - 1, rainbow[2]);
      matrix.drawPixel(snakeColumn + 3, snakeRow - 1, rainbow[3]);
      matrix.drawPixel(snakeColumn + 4, snakeRow - 1, rainbow[4]);
      matrix.drawPixel(snakeColumn + 5, snakeRow - 1, rainbow[5]);
      matrix.drawPixel(snakeColumn + 6, snakeRow - 1, rainbow[6]);

      matrix.swapBuffers(false);
      matrix.drawPixel(snakeColumn, snakeRow, 0);
      matrix.drawPixel(snakeColumn - 1, snakeRow, 0);
      matrix.drawPixel(snakeColumn - 2, snakeRow, 0);
      matrix.drawPixel(snakeColumn - 3, snakeRow, 0);
      matrix.drawPixel(snakeColumn - 4, snakeRow, 0);
      matrix.drawPixel(snakeColumn - 5, snakeRow, 0);
      matrix.drawPixel(snakeColumn - 6, snakeRow, 0);

      matrix.drawPixel(snakeColumn, snakeRow - 1, 0);
      matrix.drawPixel(snakeColumn - 1, snakeRow - 1, 0);
      matrix.drawPixel(snakeColumn - 2, snakeRow - 1, 0);
      matrix.drawPixel(snakeColumn - 3, snakeRow - 1, 0);
      matrix.drawPixel(snakeColumn - 4, snakeRow - 1, 0);
      matrix.drawPixel(snakeColumn - 5, snakeRow - 1, 0);
      matrix.drawPixel(snakeColumn - 6, snakeRow - 1, 0);
      if (snakeRow == 16 && snakeColumn == 32)
      {
        snakeColumn = 33;
      }
      else if (snakeColumn == 32 && snakeRow < 16)
      {
        snakeRow++;
        snakeColumn = -5;
      }
    }
  }
}

void LightEffects::randomLeds()
{

  if (i >= 137)
  {
    i = 0;
    matrix.fillScreen(0);
  }
  int x = random(0, 32);
  int y = random(0, 16);
  int color = random(0, 6);
  matrix.drawPixel(x, y, rainbow[color]);
  matrix.swapBuffers(true);
  i++;
}

void LightEffects::blinkLeds(uint16_t color)
{
  blinkState = !blinkState;
  if (blinkState)
  {
    matrix.fillScreen(color);
  }
  else
  {
    matrix.fillScreen(0);
  }
}

void LightEffects::fadeColors()
{
  matrix.fillScreen(matrix.ColorHSV(hue, saturation, value, false));
  hue += 20;
  if (hue >= 1536) hue -= 1536;
}
