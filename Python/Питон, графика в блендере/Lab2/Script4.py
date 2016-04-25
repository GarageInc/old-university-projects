import Blender
from Blender import Draw,BGL
import os
os.system("cls")
mystring = ""
def event(evt, val):    # эта функция обрабатывает прерывания от клавиатуры и мыши
	if evt == Draw.ESCKEY:
    		Draw.Exit()                 # выход из скрипта
    		return
 	global mystring#, mymsg
  	if Draw.AKEY <= evt <= Draw.ZKEY:
		if val==0:	#чтобы буква не печаталась два раза
			mystring += chr(evt) 
	# это срабатывает при нажатие на символьную клавишу. chr(evt) преобразует символ клавиши в 	соответствующий символ
			Draw.Redraw(1)

def gui():              # function drawing in screen
 	BGL.glRasterPos2i(10, 230) # установка позиции
  	Draw.Text("Type letters from a to z, ESC to leave.")
  	BGL.glRasterPos2i(20, 200)
  	Draw.Text(mystring)
Draw.Register(gui, event, None)  # регистрация функций и начало основного цикла