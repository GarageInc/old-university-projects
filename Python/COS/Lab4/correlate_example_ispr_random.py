# -*- coding: utf-8 -*-
"""
Created on Thu Oct 15 12:58:51 2015

@author: NigmatullinR
"""

import numpy as np
import pylab as plb

w=5  #частота
Fs=100  #част стробир

t=np.float32(range(100))/Fs
s=np.random.randn(100) 
plb.figure()
plb.plot(s)

x=np.random.randn(1000) #сигнал из 1000 случ чисел
x[500:600]=s  #внедряем в синусоиду с 500 до 600 эл-та
plb.figure()
plb.plot(x)

c=np.correlate(x,s,'full') #вычисляем корреляцию
plb.figure()
plb.plot(c)
print np.argmax(c) #индекс места. где пслдв макс-но похожи