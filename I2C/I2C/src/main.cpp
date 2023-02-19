#include <Wire.h>
#include <ESP8266WiFi.h>
#include <stdint.h>
#include "msg.h"


uint8_t MSGBuffer[sizeof(Msg)];

int port = 8888;  //Port number
WiFiServer server(port);

const char* ssid = "NimdNeevBaluskoli";
const char* password = "81194263441";


typedef enum {
  I2C_STATUS_SUCCESS,
  I2C_STATUS_FAILURE
}I2C_STATUS;

I2C_STATUS i2cWriteRead(uint16_t i2cAddress, uint16_t nWrLen, uint8_t* wrBuff, uint16_t nRdLen, uint8_t* rdBuff)
{
  Wire.beginTransmission(i2cAddress);
  Wire.write(wrBuff, nWrLen);

  if(nRdLen > 0)
  {
    Wire.requestFrom(i2cAddress, nRdLen);

    int index = 0;
    if(Wire.available())
    {
      index += Wire.readBytes(&rdBuff[index], nRdLen);

      for(int i = 0 ; i < nRdLen; i++)
      {
          Serial.printf("rd %d = %x", i, rdBuff[i]);
      }

      if(index != nRdLen)
      {
        Serial.println("I2C Error!");
        Wire.endTransmission(); 
        return I2C_STATUS_FAILURE;
      }
    }
  }

  Wire.endTransmission(); 
  return I2C_STATUS_SUCCESS;
}

void setup() {
 Serial.begin(9600); /* begin serial for debug */
 Wire.begin(D1, D2); /* join i2c bus with SDA=D1 and SCL=D2 of NodeMCU */

 WiFi.mode(WIFI_STA);
 WiFi.begin(ssid, password);
 Serial.println("Connecting to Wifi");
 while (WiFi.status() != WL_CONNECTED) {
   delay(500);
   Serial.print(".");
   delay(500);
  }

  Serial.println("");
  Serial.print("Connected to ");
  Serial.println(ssid);

  Serial.print("IP address: ");
  Serial.println(WiFi.localIP()); 


  server.begin();

  Serial.print(WiFi.localIP());
  Serial.print(" on port ");
  Serial.println(port);


}

void loop() {



  WiFiClient client = server.available();
  
  if (client) {
    if(client.connected())
    {
      Serial.println("Client Connected");
    }
    
    while(client.connected()){  
      int index = 0;    
      while(client.available()>0 || index != sizeof(Msg)){
        // read data from the connected client

        index += client.readBytes(&MSGBuffer[index], sizeof(Msg)); 
      }

      if (index != sizeof(Msg))
      {
        Serial.println("MSG not completly recieved");
      }

      //Verify the header

      Msg* msg = (Msg*)&MSGBuffer[0];



      if (msg->hdr.signature != 0x1234)
      {
        Serial.println("No proper connection");
      }
      else 
      {
        Serial.println("Header recieved");
      }
      //Send Data to connected client

      int rdLen = msg->payload.i2c.tran.v1.req.rdLen;

      i2cWriteRead(msg->payload.i2c.tran.v1.req.chipAddr, msg->payload.i2c.tran.v1.req.wrLen,
                  &msg->payload.i2c.tran.v1.req.wrBuff[0], msg->payload.i2c.tran.v1.req.rdLen,
                  &msg->payload.i2c.tran.v1.resp.rdBuff[0]);

      msg->payload.i2c.tran.v1.resp.rdLen = rdLen;


      client.write(&MSGBuffer[0], sizeof(Msg));

      //client.write("Hello From Server");
    }
    client.stop();
    Serial.println("Client disconnected"); 
  }






/*  uint8_t wrBuff = 0x02;
  uint8_t rdBuff[4] = {0,0,0,0};
  i2cWriteRead(0x6A, 1, &wrBuff, 3, &rdBuff[0]);

  for(int i = 0; i < 3; i++)
  {
    Serial.printf("rdBuff[%d] = %x\n", i, rdBuff[i]);
  }
  */
 delay(1000);
}