from client import Client
import threading

HOST = '10.57.33.239'  #IP address server machine
PORT = 3042

client = Client(HOST,PORT)
client.send_message("test1")
client.send_message("test2")
client.send_message("test3")
# client.send_message("test")


