import serial
import time
import Encrypt
import cv2
from time import sleep
import json
list=['COM1','COM2','COM3','COM4','COM5','COM6','COM7','COM8','COM9','COM10','COM11','COM12','COM13','COM14','COM15','COM16','COM17','COM18',]

COM1='COM1'
COM2='COM2'
COM3='COM3'
COM4='COM4'
COM5='COM5'
COM6='COM6'
COM7='COM7'
COM8='COM8'
COM9='COM9'
COM10='COM10'
COM11='COM11'
COM12='COM12'
COM13='COM13'
COM14='COM14'
COM15='COM15'
COM16='COM16'
COM17='COM17'
COM18='COM18'
COM19='COM19'
time.sleep(1)
ser = serial.Serial()

ser.baudrate = 9600
ser.port = COM5
ser.open()
enc = Encrypt.Encrypt()
name = input("Name:")
surname = input("Surname:")
tcNo = input("Tc no:")
tcNoEnc = (enc.encrypt(tcNo).decode() + "#").encode()
import requests

isPost = False

while True:
    try:
        cc=str(ser.readline())
        key = cc[2:][:-5]
        print(key)
        # arr = key.split("#")
        # # print(arr[1])
        # print(arr[0])
        # print(enc.decrypt((arr[1].encode())))
        if(key[0]=="#"):
            
            url = 'http://localhost:51922/api/Attendence/PostStudent'
            myobj = {'Name': name,'Surname':surname,"TCNo":tcNo,"CardUID":key[1:len(key)]}
            x = requests.post(url, data = json.dumps(myobj),headers={'Content-type':'application/json', 'Accept':'application/json'})
            print(x)
        if(key == "Type tcno hash, ending with #"):
            sleep(1)
            ser.write(tcNoEnc)
            
    except Exception as e:
        print(e)
        break
#     39565299#A2ykqnlY+ykvTp4GKQozTSVE7lxbPUPnJqMUWy74elhCsqg/5qEKPA==
# b9a84ca3#sqKImQcF3wX8x2UHlCwOWngkmthjrHUEXKZv1PaBDzO8EQM97zK9ig==
