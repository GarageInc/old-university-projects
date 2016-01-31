

# fir filter design # как сконстр FIR фильтр

import numpy as np 
import scipy.signal as sgn
import pylab as plb


#-----------------СИГНАЛЫ-------------------------#

fr0=10 #частота сигнала 0
fr1=30 #частота сигнала 1
fr2=60 #частота сигнала 2
Fr=200

t=np.float32(range(Fr))/Fr#10- чтобы 0 не попал
#конвертируем вещ-ый формат
#print t

# 3 сигнала и шум с рандомным коэффицентом
s0=np.sin(2*fr0*np.pi*t)
s1=np.sin(2*fr1*np.pi*t)
s2=np.sin(2*fr2*np.pi*t)
coef=0.2
shum = coef * np.random.randn(Fr)

#-----------------ФИЛЬТР IIR--------------------------#
order=1;#порядок
# Fr=200 => берем в 2 раза меньше: 100
# надо подавить 30
# итого: подавить на границе [0.15,0.50] - на всякий случай такой широкий диапазон

freq=[0.25,0.55]


b, a=sgn.iirfilter(order, freq, btype='bandstop')#bandstop

w, h = sgn.freqz(b,a)

 #выводим на один график сумму, шум и функцию фильтра
plb.figure()
resultSum = s0+s1+s2+shum
plb.plot(t,resultSum)
plb.plot(t,shum)
plb.plot(w/np.pi,abs(h))

#--------------ПОДАВЛЯЕМ СИГНАЛ с ЧАСТОТОЙ 30------------------#
#sig_ff_1 = sgn.filtfilt(b, 1, resultSum)# в чем разница - непонятно
sig_ff_2 = sgn.lfilter(b, 1, resultSum)

# ВЫВОДИМ - ЧТО ДОЛЖНЫ ПОЛУЧИТЬ
plb.figure()
plb.plot(t,s0+s2+shum)

# ВЫВОДИМ - РЕЗУЛЬТАТ ФИЛЬТРАЦИИ(подавление сигнала с частотой 30) - масштаб чуть увеличивается
#plb.figure()
#plb.plot(t,sig_ff_1)

plb.figure()
plb.plot(t,sig_ff_2)


#-----------ПОДАВЛЯЕМ СЛУЧАЙНЫЙ ШУМ------------------#
order=1
freq=[0,0.1]


b, a=sgn.iirfilter(order, freq, btype='bandstop')#bandstop

w, h = sgn.freqz(b,a)

 #выводим на один график сумму, шум и функцию фильтра
plb.figure()
resultSum = s0+s1+s2+shum
plb.plot(t,resultSum)
plb.plot(t,shum)
plb.plot(w/np.pi,abs(h))

sig_ff_1 = sgn.lfilter(b, 1, resultSum)# в чем разница - непонятно

# ВЫВОДИМ - ЧТО ДОЛЖНЫ ПОЛУЧИТЬ
plb.figure()
plb.plot(t,s0+s2+s1)

# ВЫВОДИМ - РЕЗУЛЬТАТ ФИЛЬТРАЦИИ(подавление сигнала с частотой 30)
plb.figure()
plb.plot(t,sig_ff_1)
