
#include <SPI.h>
#include <MFRC522.h>
// pin configs
#define RST_PIN         9          
#define SS_PIN          10        
//defining userId
String userid;


MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance

void setup() {
  
	Serial.begin(9600);		// Initialize serial communications with the PC
	while (!Serial);		// Do nothing if no serial port is opened (added for Arduinos based on ATMEGA32U4)
	SPI.begin();			// Init SPI bus
	mfrc522.PCD_Init();		// Init MFRC522
	delay(4);				// Optional delay. Some board do need more time after init to be ready, see Readme
	//mfrc522.PCD_DumpVersionToSerial();	// Show details of PCD - MFRC522 Card Reader details
	//Serial.println(F("Scan PICC to see UID, SAK, type, and data blocks..."));
 
}
//Defining the key
MFRC522::MIFARE_Key key = {keyByte: {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF}};
void loop() {
  
  mfrc522.PCD_Init();
	// Reset the loop if no new card present on the sensor/reader. This saves the entire process when idle.
	if ( ! mfrc522.PICC_IsNewCardPresent()) {
		return;
	}
	// Select one of the cards
	if ( ! mfrc522.PICC_ReadCardSerial()) {
		return;
	}
 
  //Get the UID
  String currentUID;
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    
    currentUID += String(mfrc522.uid.uidByte[i], HEX);
  }
  //If same card do nothing
  if (userid == currentUID) return;
  userid = currentUID ;
  Serial.print(userid);
  Serial.print("#");


  
  printBlock(1,16);
  printBlock(2,16);
  printBlock(8,16);
  printBlock(9,8);
  Serial.println("");

  //Stop authentication and the picc communication
   mfrc522.PICC_HaltA();
   mfrc522.PCD_StopCrypto1();
}

void printBlock (byte block, byte readLength) {
  byte buffer[18];
  byte len = 18;
  
  //Authenticate using key
  mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, block, &key, &(mfrc522.uid)); //line 834 of MFRC522.cpp file
  //read
  mfrc522.MIFARE_Read(block, buffer, &len);
  
  //Print tc no
  for (uint8_t i = 0; i < readLength; i++) {
    if (buffer[i] != 32)
    {
      Serial.write(buffer[i] );
    } 
  }

}
