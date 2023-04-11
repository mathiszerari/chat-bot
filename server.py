import socket

HOST = ""
PORT = 3042

sckt = socket.socket(socket.AF_INET, socket.SOCK_STREAM) #socket.AF_INET = IPV4 address, SOCK_STREAM = use TCP protocol
print("socket : ",sckt)

sckt.bind((HOST,PORT)) #Address Ip, Port


while True:
    sckt.listen(5) # Listen incoming connexions
    client,address = sckt.accept() # Return socket (client= new socket, address = (address IP, PORT))
    

    print(f"{client} client")
    print(f"{address} connected")

    response = client.recv(1024) # Limit to 1024 characters
    response = response.decode("utf8")

    if response != "":
        messages = response.split("\n")
        messages.pop()
        print(messages)
        for message in messages:
            print(message)

print("Close")
client.close()
sckt.close()