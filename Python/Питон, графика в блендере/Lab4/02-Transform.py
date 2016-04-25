# Transform
import Blender
from Blender import Draw 
from Blender.BGL import *
import time
from Blender.Mathutils import *
import math
from math import sin,cos

pt0=Vector([200,200]) # two types of constructors
pt1=Vector(300,220)  #
pt2=Vector(250, 300)

def bVec():
	b=Vector(0,0)
	return b
	
def rotMatr(ang): 
	mtr=Matrix([cos(ang),-sin(ang)],[sin(ang),cos(ang)])
	return mtr	

def diagMatr():
	mtr=Matrix([2,0],[0,2])
	return mtr

def detPos():
	mtr=Matrix([1.5,1],[0.5,1.2])
	return mtr

def detNeg():
	mtr=Matrix([0.5,1],[1.5,-1])
	return mtr


def event(evt, val):    
 
  if evt == Draw.ESCKEY :	
    Draw.Exit()         
    return

def gui():              
	glClearColor(0,0,0,1) 
 	glClear(GL_COLOR_BUFFER_BIT) 
	
	glLineWidth(3)
	
	b=bVec()
	mtr=rotMatr(0.5)
	#mtr=diagMatr()	
	#mtr=detPos()
	#mtr=detNeg()
	
	rpt0=mtr*pt0+b
	rpt1=mtr*pt1+b
	rpt2=mtr*pt2+b
	
	glColor3f(1.0,0,0)
	
	glRasterPos2f(pt0[0]-15, pt0[1]-15) 
  	Draw.Text("A")
	glRasterPos2f(pt1[0]+15, pt1[1]-15) 
  	Draw.Text("B")
	glRasterPos2f(pt2[0]+15, pt2[1]+15) 
  	Draw.Text("C")


	
	glBegin(GL_LINE_LOOP)
	glVertex2f(*pt0) 
	glVertex2f(*pt1)	
	glVertex2f(*pt2)
	glEnd()	
	
	glColor3f(0,1.0,0)
	
	glRasterPos2f(rpt0[0]-15, rpt0[1]-15) 
  	Draw.Text("A")
	glRasterPos2f(rpt1[0]+15, rpt1[1]-15) 
  	Draw.Text("B")
	glRasterPos2f(rpt2[0]+15, rpt2[1]+15) 
  	Draw.Text("C")
	
	glBegin(GL_LINE_LOOP)
	glVertex2f(*rpt0) 
	glVertex2f(*rpt1)	
	glVertex2f(*rpt2)
	glEnd()	
	
	
	
Draw.Register(gui, event,None)