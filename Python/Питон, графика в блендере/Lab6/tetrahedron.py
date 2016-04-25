import Blender
from Blender import Draw
from Blender.BGL import *
from Blender.Mathutils import *
from math import asin,sqrt,pi
'''
Tetrahedron
'''
v1=Vector(0,1,0) # vertex
v2=RotationMatrix(180-360/pi*asin(1/sqrt(3)),3,'z')*v1
v3=RotationMatrix(120,3,'y')*v2
v4=RotationMatrix(120,3,'y')*v3
def key(evt,val):
	if evt==Draw.ESCKEY:Draw.Exit()
def gui():		
	glClearColor(0,0,0,1)
	glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
	glEnable(GL_DEPTH_TEST)
	
	glViewport(30,30,500,500)
	glMatrixMode(GL_PROJECTION)
	glLoadIdentity()
	glOrtho(-5,5,-5,5,-50,50)
	glTranslatef(0,0,-10)
	glRotatef(50,1,1,1)
	glBegin(GL_TRIANGLES)
	glColor3f(1,0,0)
	glVertex3f(*v1)
	glVertex3f(*v2)
	glVertex3f(*v3)
	glColor3f(0,1,0)
	glVertex3f(*v1)
	glVertex3f(*v3)
	glVertex3f(*v4)
	glColor3f(0,0,1)
	glVertex3f(*v1)
	glVertex3f(*v4)
	glVertex3f(*v2)
	glColor3f(1,1,0)
	glVertex3f(*v2)
	glVertex3f(*v3)
	glVertex3f(*v4)
	glEnd()	
			
Draw.Register(gui,key,None)
		