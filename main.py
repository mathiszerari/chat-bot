from client import Client
import sys
import threading

HOST = '10.57.33.239'  #IP address server machine
PORT = 3042

client = Client(HOST,PORT)

menu_options = {"1":"Send message", "q": "quit"}

def show_menu():
    print("--MENU--")
    for item in menu_options.items():
        print(item[0], ":", item[1])

def choose_option():

    while(True):
        show_menu()
        choice = input("Choose option")
        if(choice =="1"):
            client.send_message("test1")
        
        elif(choice =="q" or choice == "Q"):
            client.disconnect()
            sys.exit()

while(True):
    choose_option()


# client.send_message("test1")
# client.send_message("test2")
# client.send_message("test3")
# client.send_message("test")


