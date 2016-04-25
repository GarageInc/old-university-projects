import Blender
from Blender import Draw, BGL 
from Blender.BGL import *
import time
from Blender.Mathutils import *
import math
from math import sin,cos,sqrt,fabs

#points
xy0=Vector([0,0]) # two types of constructors
xy1=Vector([0,0])  #

#line
point0=Vector([0,0])
point1=Vector([0,0])

shag=0

yes=0

a=0
b=0

def getCenter():
	global a
	global b
	global mddl
	global xy0
	global xy1
		
	Xcenter=0
	Ycenter=0
	radius=100
	y=0
	i = 0
	while (i < 1000):
		y=i*a+b
		i+=1
		if (fabs(sqrt((i-xy0[0])**2+(y-xy0[1])**2)-sqrt((i-xy1[0])**2+(y-xy1[1])**2))<10):
			radius=sqrt((i-xy0[0])**2+(y-xy0[1])**2)
			Xcenter=i
			Ycenter=y
			break
	
	#D=4*(radius**2)-4*(b**2)+4*(a**2)*(radius**2)
	
	#x1=((-1)*a*b*2-math.sqrt(D))/(2*(1+a**2))
	#x2=((-1)*a*b*2+math.sqrt(D))/(2*(1+a**2))
	
	#Xcenter=(x1+x2)*0.5
	#Ycenter=Xcenter*a+b
	
	makeCircle(360.0,radius,Xcenter,Ycenter)

def changeXY1():
	global shag
	global xy1
	
	n=1000
	shag+=1	
	
	newVect = (point1-point0)/100
	xy1=xy1+newVect
	
	time.sleep(0.1)
	if shag > n : Draw.Exit()
	Draw.Redraw(1)	

def event(evt, val):    
  if evt == Draw.ESCKEY :	
    Draw.Exit()         
    return

def makeCircle(passedNumberOfPoints, passedRadius, XX,YY):
	angleStep = 360.0/ passedNumberOfPoints
	for ang in range(0, 359,angleStep):
		x = passedRadius * math.cos(math.radians(ang))+XX
		y = passedRadius * math.sin(math.radians(ang))+YY
		glVertex2i(x,y)

def gui():
	Draw.PushButton("ADD LINE?",1,50,40,200,20,"Add point for line")
	
	global yes
	if(yes==1):		         
		glClearColor(0,0,0,1) 
	 	glClear(GL_COLOR_BUFFER_BIT) 
		glLineWidth(2)
		
		glColor3f(1.0,0,0)
		glBegin(GL_LINE_LOOP)		
		glVertex2f(*point0)	
		glVertex2f(*point1)
		glEnd()
		
		#points
		glPointSize(10)
		glColor3f(0,1.0,0)
		glBegin(GL_POINTS)
		glVertex2f(*xy0)	
		glVertex2f(*xy1)	
		glEnd()
		
		glColor3f(0,0,1.0)
		glBegin(GL_LINE_LOOP)
		changeXY1()	
		getCenter()
		glEnd()
		

	
def button_event(evt):
  	if evt == 1:
		global yes	
		  
		global xy0
		global xy1
		global point0
		global point1
		global mddl
		
		global a
		global b
		
		X0=Draw.Create(0)
		Y0=Draw.Create(0)		
		X1=Draw.Create(0)
		Y1=Draw.Create(0)
		A=Draw.Create(0)
		B=Draw.Create(0)
		
		block=[]			
		block.append(("X1= ",X0,0,1000)) 
		block.append(("Y1= ",Y0,0,1000))
		block.append(("X2= ",X1,0,1000)) 
		block.append(("Y2= ",Y1,0,1000))
		block.append(("A= ",A,-1000,1000)) 
		block.append(("B= ",B,-1000,1000))
		retVal=Draw.PupBlock("LINE'S POINTS",block)
		
		xy0[0]=X0.val
		xy0[1]=Y0.val
		xy1[0]=X1.val
		xy1[1]=Y1.val
		a=A.val
		b=B.val
		
		#LINE		
		point0[0]=1
		point0[1]=a*point0[0]+b
		
		point1[1]=1000
		point1[0]=(point1[1]-b)/a
		
		yes=1
		return

Draw.Register(gui, event, button_event)