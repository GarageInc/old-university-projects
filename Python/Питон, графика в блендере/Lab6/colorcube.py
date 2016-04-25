'''
Color cube on base of plane
Implement MultMatrix
Camera looks at vertex
'''
import Blender
from Blender import Draw, BGL
from Blender.BGL import *
import time
import math
from math import sqrt
a=0
def changeA():
	global a
	a +=1
	time.sleep(0.1)
	Draw.Redraw(1)
def key(evt,val):
	if evt==Draw.ESCKEY: Draw.Exit()
def myFace():
	glBegin(GL_QUADS)
	glVertex3f(-10,-10,0)
	glVertex3f(10,-10,0)
	glVertex3f(10,10,0)
	glVertex3f(-10,10,0)
	glEnd()
def myCube():
	glPushMatrix()
	glTranslatef(0,0,10)
	glColor3f(1,0,0)
	myFace()
	glTranslatef(0,0,-20)
	glRotatef(180,0,1,0)
	glColor3f(0,1,0)
	myFace()
	glPopMatrix()
	glPushMatrix()
	glRotatef(90,0,1,0)
	glTranslatef(0,0,10)
	glColor3f(0,0,1)
	myFace()
	glTranslatef(0,0,-20)
	glRotatef(180,0,1,0)
	glColor3f(1,1,0)
	myFace()
	glPopMatrix()
	glRotatef(90,1,0,0)
	glTranslatef(0,0,10)
	glColor3f(1,0,1)
	myFace()
	glTranslatef(0,0,-20)
	glRotatef(180,1,0,0)
	glColor3f(0,1,1)
	myFace()
	
def gui():
	glClearColor(0,0,0,1)
	glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
	glEnable(GL_DEPTH_TEST)
	glViewport(200,200,500,500)
	glMatrixMode(GL_PROJECTION)
	glLoadIdentity()		
#	glFrustum(-100,100,-100,100,30,500)
	glOrtho(-20,20,-20,20,-500,500)
	v1=1/sqrt(3)
	v2=1/sqrt(2)
	v3=v1*v2
	v4=v1/v2
	buf=Buffer(GL_FLOAT,16,[v3,v2,v1,0,v3,-v2,v1,0,-v4,0,v1,0,0,0,0,1])
#	glMultMatrixf(buf)
	glTranslatef(0,0,-50)
	glMatrixMode(GL_MODELVIEW)
	glLoadIdentity()
	glRotatef(a,0,0,1)
	glMultMatrixf(buf)

	myCube()
	changeA()
Draw.Register(gui,key,None)