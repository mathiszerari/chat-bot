import socket

HOST = '10.57.33.239'  
PORT = 3042

sckt = socket.socket(socket.AF_INET, socket.SOCK_STREAM) #socket.AF_INET = IPV4 address, SOCK_STREAM = use TCP protocol

sckt.connect((HOST,PORT)) #Connect socket to server

sckt.send("Message de test".encode()) #Send data to socket

sckt.close()

