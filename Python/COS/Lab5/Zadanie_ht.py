# -*- coding: utf-8 -*-
"""
HW7_1
Написать польз фкц  л.29.10.2015
"""

import numpy as np
import pylab as plb

def ht(t):
     ht=np.sin(np.pi*t/T)/(t/np.pi)
     return ht


fr=2#частота сигнала
n=6020 #частота стробирования
T=np.float32(1)/fr# не получается, тк округл

t=np.float32(range(-n/2,n/2,10))/1000#10- чтобы 0 не попал
#конвертируем вещ-ый формат
print t

s=np.sin(2*fr*np.pi*t)
plb.figure()
plb.plot(t,s)

h=ht(t)
plb.figure()
plb.plot(h)




7