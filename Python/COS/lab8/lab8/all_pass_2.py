# -*- coding: utf-8 -*-
"""
Created on Wed Nov 25 14:40:40 2015

@author: NigmatullinR
"""

import numpy as np
import scipy.signal as sgn
import pylab as plb

a=0.6#параметр для коэф фильтра

b_coef=np.float32([a, -1.0])
a_coef=np.float32([1.0, -a])

w=np.float32(range(0,1024))/1024

num=b_coef[0]+b_coef[1]*np.e**(-2*np.pi*1j*w)
denum=a_coef[0]+a_coef[1]*np.e**(-2*np.pi*1j*w)

hr=num/denum

plb.plot(w,abs(hr))