import Blender
from Blender import Draw,BGL
from Blender.BGL import *




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
	glShadeModel(GL_SMOOTH)
	glBegin(GL_POLYGON)
	glVertex2i(500,10)	
	glColor3f(0,1,0)
	glVertex2i(530,600)
	glColor3f(0,0,1)
	glVertex2i(30,60)
	glColor3f(1,1,1)
	glVertex2i(10,600)	
	glEnd()	
	
	
Draw.Register(gui, event,None)