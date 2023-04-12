import socket
import threading

class Client:

    def __init__(self, server_host, server_port):
        self.__server_host = server_host
        self.__server_port = server_port
        self.__sckt = socket.socket(socket.AF_INET, socket.SOCK_STREAM) #socket.AF_INET = IPV4 address, SOCK_STREAM = use TCP protocol
        self.__pseudo = self.create_pseudo()
        self.connect()
    
    def get_server_host(self):
        return self.__server_host
    
    def get_server_port(self):
        return self.__server_port
    
    def get_pseudo(self):
        return self.__pseudo
    
    def create_pseudo(self):
        tmp_pseudo = input("Enter pseudo : ")
        
        while(len(tmp_pseudo)>20 or len(tmp_pseudo) <= 0):
            tmp_pseudo = input("Enter pseudo : ")
        
        self.__pseudo = tmp_pseudo
    
    def connect(self):
        self.__sckt.connect((self.__server_host,self.__server_port)) #Connect socket to server
    
    def disconnect(self):
        self.__sckt.close()

    def send_message(self):
        message = input("Enter message : ")

        while(len(message)<= 0):
            message = input("Enter message : ")
            
        self.__sckt.send((message + "\n").encode()) #Send data to socket







