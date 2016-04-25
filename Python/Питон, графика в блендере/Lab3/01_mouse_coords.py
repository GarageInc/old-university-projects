import Blender
from Blender import Draw,BGL
mystr=""
def event(evt, val):    # эта функция обрабатывает прерывания от клавиатуры и мыши
	global mystr
	if evt == Draw.ESCKEY:
    		Draw.Exit()                 # выход из скрипта
    		return
  	if val==0 and evt ==Draw.LEFTMOUSE:
		coor=Blender.Window.GetMouseCoords()
		print coor
		mystr =str(coor[0])+"  "+str(coor[1])
		Draw.Redraw(1)


		

def gui():              # function drawing in screen
 	BGL.glRasterPos2i(10, 230) # установка позиции
  	Draw.Text("coods"+" "+mystr)
  	
Draw.Register(gui, event, None)