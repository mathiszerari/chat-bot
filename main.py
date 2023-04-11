from client import Client

HOST = '10.57.33.239'  #IP address server machine
PORT = 3042

client = Client(HOST,PORT)
client.send_message("test")
client.send_message("test")
client.send_message("test")
client.send_message("test")

