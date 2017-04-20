#include <Encoder.h>

Encoder nov(2, 3);

void setup() {
  Serial.begin(9600);
}
long nlp = -999; 

void loop() {
  long newLeft;
  newLeft = nov.read();

  if (newLeft/4 != nlp/4)
  {
    if (newLeft/4 > nlp/4)
    {
      //Serial.println("R");
      Serial.write("R");
    }
    else
    {
    //Serial.println("L");
     Serial.write("L");
    }
    nlp = newLeft;
  }
  delay(1);
}

