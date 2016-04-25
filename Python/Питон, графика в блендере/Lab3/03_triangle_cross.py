import Blender
from Blender.Mathutils import *
# Даны три точки на плоскости
pt0=Vector([100,100,0])
pt1=Vector(100,200,0)  
pt2=Vector(10,300,0)
# стороны треугольника
dpt01=pt1-pt0
dpt02=pt2-pt0
# векторное произведение
crss=CrossVecs(dpt01,dpt02)# Это устаревшая версия функции
# crss=dpt02.cross(dpt01)  годится для новых версий
# длина вектора
ln=crss.length
# А теперь площадь треугольника с помощью встроенной функции
sq=TriangleArea(pt0,pt1,pt2)
print 'ln',ln/2
print 'sq',sq