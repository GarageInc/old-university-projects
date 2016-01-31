# -*- coding: utf-8 -*-
"""
Created on Sat Oct 10 23:03:12 2015

@author: 9-b-r_000
"""

import numpy as np
import pylab as plb
import math

fr=10
T=1000
t=np.float32(range(T))/T

# синусоидальный сигнал
s=np.sin(2*fr*np.pi*t)

# уровень
B = 16 # bits

e_sin=[]

###############################################################################

# шаг дискретизации, оно всегда остаётся постоянным - т.к. ... это шаг дискретизации 
step = 2/np.float32(2**B) # т.к. от -1 до 1 всего 2**B дискретных значений, берется "2" - т.к. [-1,1]
 

for element in t:
    point = np.sin(2*fr*np.pi*element)
    pointABS = math.fabs(point)

    # теперь найдем - к верхнему или нижнему краю ближе результат дискретизации    
    pointABScopy = pointABS % step # остаток-указатель

    middle = step/2 #срединное значение

    # найдем ошибку дискретизации с учётом близости к нижнему или верхнему краю
    error = 0.0

    if middle > pointABScopy: # если ближе к верхнему уровню,
        error = pointABScopy
    else:
        error = step - pointABScopy
    
    # добавляем ошибку дискретизации в список ошибок
    e_sin.append(error)

# считаем и выводим SNR
dev = np.var(s)/np.var(e_sin) #дисперсии
SNR = 10*math.log(dev,10)
print "SNR практический синусоидального сигнала  = ", SNR

SNR = 6*B -  7.2
print "SNR теоретический  синусоидального сигнала  = ", SNR
        
# вывод графика ошибок
plb.figure()
plb.plot(t,e_sin)

###############################################################################
# сигнал с равномерным распределением - хитрое получение
randnsignal = []
for i in range(T):
    value = np.random.rand(1)    
    randnsignal.append(value[0])

# посчитаем и для него SNR    
e_randn = []
for point in randnsignal:
    pointABS = math.fabs(point)

    # теперь найдем - к верхнему или нижнему краю ближе результат дискретизации    
    pointABScopy = pointABS % step # остаток-указатель

    middle = step/2 #срединное значение

    # найдем ошибку дискретизации с учётом близости к нижнему или верхнему краю
    error = 0.0

    if middle > pointABScopy: # если ближе к верхнему уровню,
        error = pointABScopy
    else:
        error = step - pointABScopy
    
    # добавляем ошибку дискретизации в список ошибок
    e_randn.append(error)

# считаем и выводим SNR
dev = np.var(randnsignal)/np.var(e_randn) #дисперсии    
SNR = 10*math.log(dev,10)
print "SNR практический случайного  сигнала  = ", SNR
#print randnsignal