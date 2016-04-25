import Blender
from Blender.Mathutils import *
import math
# Даны три точки на плоскости
pt0=Vector([100,100,0])
pt1=Vector(300,100,0)  
pt2=Vector(200,100+math.sqrt(3)*100,0)
# стороны треугольника
dpt01=pt1-pt0
dpt02=pt2-pt0
# векторы ортогональные к сторонам треугольника
ortdpt01=Vector(dpt01[1],-dpt01[0],0)
ortdpt02=Vector(dpt02[1],-dpt02[0],0)
#Точка пересечения. Каждая прямая определяется двумя точками
pt=LineIntersect(pt2,pt2+ortdpt01,pt1,pt1+ortdpt02)
print 'pt',pt
print 'middle',(pt0+pt1+pt2)/3