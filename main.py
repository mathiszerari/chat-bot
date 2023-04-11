from client import Client
import threading

HOST = '10.57.33.239'  #IP address server machine
PORT = 3042

client = Client(HOST,PORT)

message_thread = threading.Thread(client.send_message(), args=("test"))
