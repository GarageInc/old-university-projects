# -*- coding: utf-8 -*-
"""
Created on Thu Nov 19 12:35:37 2015

@author: NigmatullinR
"""

import numpy as np
import scipy.signal as sgn
import pylab as plb

a=0.6

b_coef=np.float32([a, -1.0])
a_coef=np.float32([1.0, -a])

w, h = sgn.freqz(b_coef,a_coef)
 
plb.plot(w/np.pi,abs(h))

hn=h/(abs(h))

ha=np.angle(hn)
gd=np.diff(ha)/np.diff(w)

plb.figure()
plb.plot(w/np.pi,ha/w)

plb.figure()
plb.plot(w[:-1]/np.pi,gd)