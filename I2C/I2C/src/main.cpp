#include <Wire.h>
#include <ESP8266WiFi.h>
#include <stdint.h>

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
}

void loop() {

  uint8_t wrBuff = 0x02;
  uint8_t rdBuff[4] = {0,0,0,0};
  i2cWriteRead(0x6A, 1, &wrBuff, 3, &rdBuff[0]);

  for(int i = 0; i < 3; i++)
  {
    Serial.printf("rdBuff[%d] = %x\n", i, rdBuff[i]);
  }

 Serial.println();
 delay(1000);
}