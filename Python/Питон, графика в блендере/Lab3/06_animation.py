import Blender
from Blender import Draw, BGL 
from Blender.BGL import *
import time
a=[10,10]
def changeA():
	global a
	a[0] +=2
	a[1] +=1
#	print 'step',a
	if a[1]> 100: Draw.Exit()
	time.sleep(0.1)	
	Draw.Redraw(1)
	
def event(evt, val):    # function to handle input events
 
  if evt == Draw.ESCKEY :	
    Draw.Exit()         # exit when user presses ESC
    return

def gui():              # function to draw the screen
	global a
	glClearColor(0,0,0,1) # background color
 	glClear(BGL.GL_COLOR_BUFFER_BIT) # clear image buffer
	glColor3f(1.0,0,0)
	glLineWidth(5)
	glBegin(GL_LINES)
	glVertex2i(10,a[0])	
	glVertex2i(130,a[1])	
	glEnd()	
	changeA()
	
Draw.Register(gui, event,None)  # registering the 3 callbacks