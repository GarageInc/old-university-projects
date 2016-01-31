# -*- coding: utf-8 -*-
"""
Created on Thu Oct 08 14:41:24 2015

@author: Сабина
"""
import numpy as np
import pylab as plb
fr=10
t=np.float32(range(1000))/1000
s=np.sin(2*fr*np.pi*t)

h,b=np.histogram(s,256)


#plb.plot(b[:-1],h)#в виде графика
plb.hist(s,32)#в виде столбиков
plb.figure()

plb.plot(t,s)

