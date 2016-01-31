# -*- coding: utf-8 -*-
"""
Created on Thu Oct 08 14:41:24 2015

@author: Сабина
"""
import numpy as np
import pylab as plb
s=np.sin(2*fr*np.pi*t)
h,b=np.histogram(s,256)
plb.figure()

#plb.plot(b[:-1],h)#в виде графика
plb.hist(s,32)#в виде столбиков

