import customtkinter
import tkinter

class Clientui(customtkinter.CTk):
    def __init__(self):
        super().__init__()

        self.geometry("500x700")
        self.title("Chatbot d'avril")
        self.minsize(300, 200)
        self._set_appearance_mode("dark")
        

        #Font parameter
        self.send_button_font = customtkinter.CTkFont(family="Arial", size=18, weight="bold")
        self.chat_text_font =  customtkinter.CTkFont(family="Arial", size=18, weight="normal")

        #self.chatArea = customtkinter.CTkTextbox(master=self, fg_color="#4a4a4a", bg_color='#4a4a4a', text_color="#ffffff", font=chat_text_font, state= "disabled")
        #self.chatArea.grid(row=0, column=0, columnspan=3, padx=20, pady=(20, 0), sticky="nsew")

        #footer frame wrapper
        self.bottom_frame = tkinter.Frame(self, padx=20, pady=20, width=500, background="#242424")
        self.bottom_frame.pack(side="bottom")

        #Message section
        self.message_wrapper = tkinter.Frame(self, padx=20, pady=20, width=500, background="#242424")
        self.message_wrapper.pack(side="bottom", anchor="w")
        

        self.inputMessage = customtkinter.CTkEntry(master=self.bottom_frame, placeholder_text="Ecrivez votre message ici...", width=300) #Input text
        self.inputMessage.pack(side="left",anchor="w")
        self.button = customtkinter.CTkButton(master=self.bottom_frame, command=self.send_messsage, text="Envoyer", font=self.send_button_font, width=100)
        self.button.pack(side="right",anchor="e")

    def send_messsage(self):
        #message frame
        message_frame = tkinter.Frame(master=self.message_wrapper, bg="#242424")
        message_frame.pack(side="top", anchor="w")

        message = tkinter.StringVar(value=self.inputMessage.get())

        #pseudo label
        pseudo = tkinter.StringVar(value= "Pseudo") #Pseudo à rendre dynamique
        pseudo_label =  customtkinter.CTkLabel(master=message_frame, textvariable=pseudo, wraplength=450, pady=10, padx=0, font= self.chat_text_font, text_color="#ffffff", bg_color="#242424", anchor="w", justify="left")
        pseudo_label.pack(side="top", anchor="w")

        #message label
        message_label = customtkinter.CTkLabel(master=message_frame, textvariable=message, wraplength=450, pady=10, padx=10, font= self.chat_text_font, text_color="#ffffff", bg_color="#007acc", justify="left")
        message_label.pack(side="bottom", anchor="w")

        #reset input value
        self.inputMessage.delete(0, "end")


if __name__ == "__main__":
    app = Clientui()
    app.mainloop()