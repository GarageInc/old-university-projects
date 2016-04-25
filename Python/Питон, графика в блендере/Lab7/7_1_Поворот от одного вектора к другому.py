
'''
Color cube on base of plane
Implement MultMatrix
Camera looks at vertex
'''
from Blender import Draw, BGL,Mathutils
from Blender.BGL import *
from Blender.Mathutils import *
import time
import math
from math import sin,cos,pi,sqrt
a=0

t=0.001
vect = Vector(6,8,2);
def changeA():
	global a
	global vect
	global t
	a +=1
	vect = (1-t)*Vector(6,8,2)+t*Vector(-4,6,1)
	t+=0.001
	if a>1000: Draw.Exit()
	time.sleep(0.001)
	Draw.Redraw(1)
	
def key(evt,val):
	if evt==Draw.ESCKEY: Draw.Exit()
		
def myFace():
	glBegin(GL_QUADS)
	z=2
	glVertex3f(-z,-z,0)
	glVertex3f(z,-z,0)
	glVertex3f(z,z,0)
	glVertex3f(-z,z,0)
	glEnd()
	
def myCube():
	d=2
	glPushMatrix()
	glTranslatef(0,0,d)
	glColor3f(1,0,0)
	myFace()
	glTranslatef(0,0,-2*d)
	glRotatef(180,0,1,0)
	glColor3f(0,1,0)
	myFace()
	glPopMatrix()
	glPushMatrix()
	glRotatef(90,0,1,0)
	glTranslatef(0,0,d)
	glColor3f(0,0,1)
	myFace()
	glTranslatef(0,0,-2*d)
	glRotatef(180,0,1,0)
	glColor3f(1,1,0)
	myFace()
	glPopMatrix()
	glRotatef(90,1,0,0)
	glTranslatef(0,0,d)
	glColor3f(1,0,1)
	myFace()
	glTranslatef(0,0,-2*d)
	glRotatef(180,1,0,0)
	glColor3f(0,1,1)
	myFace()

def gui():
	glClearColor(0,0,0,1)
	glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)
	glEnable(GL_DEPTH_TEST)
	glViewport(0,0,500,500)
	glMatrixMode(GL_PROJECTION)
	glLoadIdentity()		
	#glFrustum(-100,100,-100,100,30,500)
	glOrtho(-10,10,-10,10,-500,500)
	
	vect3=vect
	norm=sqrt(vect3[0]**2+vect3[1]**2+vect3[2]**2)
	vect3=vect3/norm
	
	vect2=Vector(0,1,0)-vect3[1]*vect3
	norm=sqrt(vect2[0]**2+vect2[1]**2+vect2[2]**2)
	vect2=vect2/norm
	
	vecCros=Mathutils.CrossVecs(vect3,vect2)
	norm=sqrt(vecCros[0]**2+vecCros[1]**2+vecCros[2]**2)
	vect1=vecCros/norm
	
	buf2=Buffer(GL_FLOAT,16,[vect1[0],vect1[1],vect1[2],0,
							 vect2[0],vect2[1],vect2[2],0,
							 vect3[0],vect3[1],vect3[2],0,
							 0,0,0,1])
	
	#buf2=Buffer(GL_FLOAT,16,[1,0,0,v1,
		#					 0,1,0,v2,
			#				 0,0,1,v3,
				#			 0,0,0,1])
#	glMultMatrixf(buf)
	glTranslatef(0,0,-50)
	glMatrixMode(GL_MODELVIEW)
	glLoadIdentity()
#	glRotatef(a,1,1,1)
	glMultMatrixf(buf2)

	myCube()
	changeA()
Draw.Register(gui,key,None)