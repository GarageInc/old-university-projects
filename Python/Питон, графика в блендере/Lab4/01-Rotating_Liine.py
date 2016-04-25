# rotatingting  line
import Blender
from Blender import Draw 
from Blender.BGL import *
import time
from Blender.Mathutils import *
import math
from math import sin,cos
pt0=Vector([100,100]) # two types of constructors
pt1=Vector(100,200)  #
mddl=(pt0+pt1)*0.5
pt0s=pt0-mddl
pt1s=pt1-mddl
a=0
def changeA():
	global a
	a +=.2
	#	print 'step',a
	if a> 6: Draw.Exit()
	time.sleep(0.1)	
	Draw.Redraw(1)
def rotMatr(ang): # Матрица поворота
	mtr=Matrix([cos(ang),-sin(ang)],[sin(ang),cos(ang)])
														# size depends on arg list
	return mtr	
def event(evt, val):    
 
  if evt == Draw.ESCKEY :	
    Draw.Exit()         
    return

def gui():              
	glClearColor(0,0,0,1) # background color
 	glClear(GL_COLOR_BUFFER_BIT) # clear image buffer
	glColor3f(1.0,0,0)
	glLineWidth(5)
	mtr1=rotMatr(a)
	rpt0=mtr1*pt0s
	rpt1=mtr1*pt1s
	pt0t=rpt0+mddl
	pt1t=rpt1+mddl	
	glBegin(GL_LINES)
	glVertex2f(*pt0t) # access to coords	
	glVertex2f(*pt1t)	
	glEnd()	
	
	changeA()
	
Draw.Register(gui, event,None)