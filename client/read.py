import serial
import time
import Encrypt
from datetime import datetime
import requests
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
now = datetime.now().strftime('%Y-%m-%dT%H:%M:%S.%f')
while True:
    try:
        cc=str(ser.readline())
        key = cc[2:][:-5]
        arr = key.split("#")
        
        now = datetime.now().strftime('%Y-%m-%dT%H:%M:%S.%f')
        url = 'http://localhost:51922/api/Attendence'
        print(enc.decrypt((arr[1].encode())))
        data = {'checkIn': str(now),'UID':arr[0],"TCNo":enc.decrypt((arr[1].encode()))}
        x = requests.post(url, data = json.dumps(data),headers={'Content-type':'application/json', 'Accept':'application/json'})
        response_dict = json.loads(x.text)
        print("Student ",response_dict["studentName"],response_dict["studentSurname"],"attended at",response_dict["checkIn"])
        
    except Exception as e:
        print(e)
        print("Wrong credentials")