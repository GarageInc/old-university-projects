from Tkinter import *
import Tkinter as tki

import os.path
import time
import datetime
import sys

class App(object):

    def __init__(self):
        self.root = tki.Tk()

    	# self.best = StringVar()
	# self.best.set('start')

	self.fileName = "result.txt"
	self.file_size_stored = 0	
	self.last_position = 0
	self.M = 0

        txt_frm = tki.Frame(self.root, width=600, height=600)
        txt_frm.pack(fill="both", expand=True)
        txt_frm.grid_propagate(False)
        txt_frm.grid_rowconfigure(5, weight=1)
        txt_frm.grid_columnconfigure(0, weight=1)

	self.label = Label(txt_frm, text="Time in milliseconds:")
    	self.label.grid(row=0)

	self.txtTime = Entry(txt_frm)
	self.txtTime.insert(END, "1000")
    	self.txtTime.grid(row=1)

	self.button = Button(txt_frm, text="Start", command=self.buttonListener)
    	self.button.grid(row=2)

        self.txt = tki.Text(txt_frm,  borderwidth=3, relief="sunken")
	# self.txt.textvariable = self.best
        self.txt.config(font=("consolas", 12), undo=True, wrap='word')
        self.txt.grid(row=3, column=0, sticky="nsew", padx=2, pady=2)
	
        scrollb = tki.Scrollbar(txt_frm, command=self.txt.yview)
        scrollb.grid(row=3, column=1, sticky='nsew')

        self.txt['yscrollcommand'] = scrollb.set


    def buttonListener(self):
	self.button.config(state='disabled')
	
	#not validate
	self.M = int(self.txtTime.get());

  	self.root.after( 0, self.updater )

    def updater( self ):
	
	#input("Enter file path/name:")
	#print("FILE: " + fileName)

	if(os.path.isfile( self.fileName )):	
	
		self.file_size_current = os.stat( self.fileName ).st_size
	      		
		if self.file_size_stored == self.file_size_current:
  			self.root.after( self.M, self.updater )
			return
	
		self.file_size_stored = self.file_size_current

		file = open( self.fileName )

		file.seek( self.last_position )
		line = file.read()

		lines = line.split('\n')
		
		line = ""
		for l in lines:
			if ( l ):
				elements = l.strip().split(',')
				mcs = float(elements[3]) # because in mcs, we want ms
				elements[3] = (datetime.datetime.fromtimestamp(0) + datetime.timedelta(microseconds=mcs)).strftime("%Y-%m-%d %H:%M:%S.%f") 			
				line = line + ", ".join( elements ) + "\n"

			
		self.txt.insert(END, line);
		self.txt.update()
		self.txt.see("end")

		self.last_position = file.tell()
		
  		self.root.after( self.M, self.updater )
	
		file.close()
		
	else:
		self.txt.insert(END, "No such file! Incorrect file path. Try again.\n");
		self.txt.update()
		self.button.config(state='active')


app = App()
app.root.mainloop()
