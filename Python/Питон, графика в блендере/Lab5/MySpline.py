import Blender
from Blender import BGL
from Blender.BGL import *
from Blender import Draw
from Blender.Mathutils import *
import os
os.system('cls')

lst=[[100,100],[150,200],[200,150],[300,300]]
#z=[10, 5, -5, 10]
z=[0, 0, 0, 0]

def event(evt, val):    
 
  if evt == Draw.ESCKEY :	
    Draw.Exit()         
    return


def gui():              
	glClearColor(0,0,0,1) 
 	glClear(GL_COLOR_BUFFER_BIT) 	
	glLineWidth(1)	
	glColor3f(1.0,0,0)
	print len(lst)-1
	
	for i in range(0, len(lst)-1):
		mtr=Matrix([1, lst[i][0], lst[i][0]**2, lst[i][0]**3],
				   [1, lst[i+1][0], lst[i+1][0]**2, lst[i+1][0]**3],
				   [0, 1, 2*lst[i][0], 3*lst[i][0]**2],
				   [0, 1, 2*lst[i+1][0], 3*lst[i+1][0]**2])
		b=Vector([lst[i][1], lst[i+1][1], z[i], z[i+1]])
		
		a=mtr.invert()*b
		
		print a
		print mtr
		
		
		glBegin(GL_LINE_STRIP)
		
		for x in range(lst[i][0], lst[i+1][0]):
			y=a[0]+x*a[1]+(x**2)*a[2]+(x**3)*a[3]
			print "x=", x, " y=", y
			glVertex2f(x,y)
		
		glEnd()	
	
Draw.Register(gui, event,None)
