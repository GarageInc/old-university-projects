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
plb.figure()

plb.plot(t,s)

