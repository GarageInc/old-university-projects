import Blender
from Blender import Draw 
from Blender.BGL import *
import os
os.system('cls')

lst=[(200,400,0), (100,600,0), (700,200,0), (600,400,0)]

def event(evt, val):
	if evt == Draw.ESCKEY :
		Draw.Exit()
		return

def gui():              
	glClearColor(0,0,0,1) 
 	glClear(GL_COLOR_BUFFER_BIT) 
	
	glLineWidth(1)
		
	glColor3f(1.0,0,0)
	
	buf=Buffer(GL_FLOAT,[len(lst),3],lst)
	
	glMap1f(GL_MAP1_VERTEX_3, 0, 50, 3, len(lst), buf)
	
	glEnable(GL_MAP1_VERTEX_3)

	glBegin(GL_LINE_STRIP)
	for i in range(0, 51):
		glEvalCoord1f(i)
	glEnd()	
	
	glColor3f(0, 1.0, 0.5)
	glPointSize(5)
	glBegin(GL_POINTS)
	for i in range(0, len(lst)):
		glVertex2f(lst[i][0], lst[i][1])
	glEnd()
	
	
Draw.Register(gui, event,None)