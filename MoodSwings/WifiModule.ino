WifiModule::WifiModule()
{
  
}

String WifiModule::sendToWifi(String command, const int timeout, boolean debug)
{
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
void WifiModule::checkInput()
{
  String response ="";
  if(wifi.available() > 0)  
  {
    while(wifi.available())
    {
      // The esp has data so display its output to the serial window 
      char c = wifi.read(); // read the next character.
      response+=c;
    }  
//    Serial.println(response);
    int beginInput = response.indexOf('#');
    int endInput = response.indexOf('$');
    String information = response.substring(beginInput+1,endInput);
    Serial.println(information);
    String part = "";
    int i = 0;
    while(information.indexOf(',') != -1)
    {
      part = information.substring(0, information.indexOf(','));
      if(i == 0)
      {
        artist = part;
      }
      if(i == 1)
      {
        track = part;
      }
      if(i == 2)
      {
        function = part;
      }
      if(i == 3)
      {
        Speed = part.toInt();
      }
      i++;      
      information.remove(0, part.length()+1);
    }  
  }        
}
