# -*- coding: utf-8 -*-
"""
Created on Thu Oct 15 12:48:01 2015

@author: NigmatullinR
"""

import numpy as np
import pylab as plb

a=[2,4,4,2]  #залаем пслд
print 'a: ', a
plb.figure()
plb.plot(a)

b=[4,-1,2,0,2,4,4,2,3,2,1,3] #
print 'b: ', b
plb.figure()
plb.plot(b)

c=np.convolve(b,a,'valid')  #свертка пслдв-ей, третий пар-р - как вычислятся,
 # это разме выходного значения(valid)
print 'c:', c
plb.figure()
plb.plot(c)