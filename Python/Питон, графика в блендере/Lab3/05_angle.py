# The least angle containing all points
import Blender
from Blender.BGL import *
from Blender import Draw
from Blender.Mathutils import *

lst=[(40,20),(60,40),(50,140),(124,200),(300,260)] # given points
# center of angle
max1=max2=0
for item in lst:
	if item[0]>max1:max1=item[0]
	if item[1]>max2:max2=item[1]
cnt=Vector(2*max1,max2/2) 

# max angle
ang=0
pnt1=lst[0]
pnt2=lst[0]
ln=len(lst)
for i in xrange(ln):
	for j in xrange(i+1,ln):
		v1=Vector(lst[i])
		v2=Vector(lst[j])
		av=AngleBetweenVecs(v1-cnt,v2-cnt)
		if av>ang:
			ang=av
			pnt1=lst[i]
			pnt2=lst[j]		
		
def gui():
	glClearColor(1,1,0,1)
	glClear(GL_COLOR_BUFFER_BIT)
	glColor3f(0,0,1)
	
	glPointSize(5)
	glBegin(GL_POINTS)
	for item in lst:
		glVertex2f(*item)
	glColor3f(0,1,0)
	glVertex2f(*cnt)	
	glEnd()
	glBegin(GL_LINES)
	glVertex2f(*cnt)
	glVertex2f(*pnt1)
	glVertex2f(*cnt)
	glVertex2f(*pnt2)
	glEnd()
	
def key(evt,val):
	if evt==Draw.ESCKEY:Draw.Exit()

Draw.Register(gui,key,None)