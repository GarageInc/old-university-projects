'''
Map2 Example
'''
import time
import Blender
import random
from Blender.BGL import *
from Blender import Draw
lst=[[-4,0,0],
	 [-2,0,0],
	 [2,0,0],
	 [4,0,0],
	
	 [-4,2,0],
	 [-2,2,0],
	 [2,2,0],
	 [4,2,0],
	
	 [-4,4,0],
	 [-2,4,0],
	 [2,4,0],
	 [4,4,0],
	
	 [-4,6,0],
	 [-2,6,0],
	 [2,6,0],
	 [4,6,0]]

#lst=[(-4,0,0),(-6,4,0),(5,-4,0),(4,0,0)]
buff=Buffer(GL_FLOAT,[16,3],lst)
ang=1

def change():
	global buff,lst,ang
	time.sleep(0.01)
	i=0;
	while i<16:
		j=0
		while j<2:
			x=(random.randint(0,10)-5)*0.05
			print x;
			lst[i][j]+=x
			j+=1
		i+=1
		
	buff=Buffer(GL_FLOAT,[16,3],lst)
	ang +=1
	Draw.Redraw(1)
	
def gui():
	glClearColor(0,0,0,1)
	glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
	glEnable(GL_DEPTH_TEST)
	
#	glViewport(300,300,200,200)
	glMatrixMode(GL_PROJECTION)
	glLoadIdentity()
	glOrtho(-15,15,-15,15,-50,50)
	glRotatef(ang,1,0,0)

	glPointSize(5)
	glColor3f(0,1,0)
	
	glBegin(GL_POINTS)
	for i in range(4):
		glVertex3f(*lst[i])
	glEnd()
	glColor3f(0,0,1)
	glBegin(GL_POINTS)
	for i in range(4,8):
		glVertex3f(*lst[i])
	glEnd()
	glColor3f(1,0,1)

	glBegin(GL_POINTS)
	for i in range(8,12):
		glVertex3f(*lst[i])
	glEnd()
	glColor3f(1,1,1)

	glBegin(GL_POINTS)
	for i in range(12,16):
		glVertex3f(*lst[i])
	glEnd()
	
	glColor3f(1,0,0)
#	table 4 X $	
	glMap2f(GL_MAP2_VERTEX_3,0,10,3,4,0,10,12,4,buff)
	glEnable(GL_MAP2_VERTEX_3)
	glMapGrid2f(10,0,10,10,0,10)
	glEvalMesh2(GL_LINE,0,10,0,10)
# 	a part of the mesh is used 
#	glEvalMesh2(GL_LINE,0,5,0,5)
	change()
	
def key(evt,val):
	if evt==Draw.ESCKEY:Draw.Exit()
Draw.Register(gui,key,None)
		