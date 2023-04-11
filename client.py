import socket
import threading

class Client:

    def __init__(self, server_host, server_port):
        self.__server_host = server_host
        self.__server_port = server_port
        self.connect()
    
    def get_server_host(self):
        return self.__server_host
    
    def get_server_port(self):
        return self.__server_port
    
    def connect(self):
        sckt = socket.socket(socket.AF_INET, socket.SOCK_STREAM) #socket.AF_INET = IPV4 address, SOCK_STREAM = use TCP protocol
        sckt.connect((self.__server_host,self.__server_port)) #Connect socket to server
    

    def send_message(self,message):
        sckt = socket.socket(socket.AF_INET, socket.SOCK_STREAM) #socket.AF_INET = IPV4 address, SOCK_STREAM = use TCP protocol
        sckt.connect((self.__server_host,self.__server_port)) #Connect socket to server
        sckt.send(message.encode()) #Send data to socket
        sckt.close()






