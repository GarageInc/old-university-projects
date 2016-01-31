# -*- coding: utf-8 -*-
"""
HW7
Написать польз фкц  л.29.10.2015/
"""
import numpy as np
import pylab as plb

def ht(t):
     ht=np.sin(np.pi*t/T)/(t/np.pi)
     return ht

m=7
fr=15#частота сигнала
n=60200 #частота стробирования
T=np.float32(1)/fr# не получается, тк округл

t=np.float32(range(-n/2,n/2,10))/10000#10- чтобы 0 не попал
#конвертируем вещ-ый формат
print t


hm=[]
for i in t:
    hm.append(ht(i))
t1=np.float32(range(-n/2,n/2,10))/10000
def x(t):
    return np.sin(2*np.pi*m*t)
  
plb.figure()
plb.plot(t,x(t))  
    
    
    
y=[]
for i in t1:
    sum=0
    for j in range (-100,100):
        sum+=x(j*T)*ht(i-j*T)
    y.append(sum)

plb.figure()
plb.plot(t1,y)