

# fir filter design # как сконстр FIR фильтр

import numpy as np 
import scipy.signal as sgn
import pylab as plb


#-----------------СИГНАЛЫ-------------------------#

fr0=15 #частота сигнала 0
fr1=40 #частота сигнала 1
fr2=60 #частота сигнала 2
fr3=90 #частота сигнала 3
Fr=200

t=np.float32(range(Fr))/Fr#10- чтобы 0 не попал
#конвертируем вещ-ый формат
print t

s0=np.sin(2*fr0*np.pi*t)
s1=np.sin(2*fr1*np.pi*t)
s2=np.sin(2*fr2*np.pi*t)
s3=np.sin(2*fr3*np.pi*t)

plb.figure()

#plb.plot(t,s0)
#plb.plot(t,s1)
#plb.plot(t,s2)
#plb.plot(t,s3)

# ВЫВОДИМ - ДО ФИЛЬТРАЦИИ
resultSum = s1+s2+s0+s3
plb.plot(t,resultSum)

#-----------------ФИЛЬТР--------------------------#
order=55;# кол коэф, чем больше, тем круче
freq=[0.3,0.7]#частота отчесения

b=sgn.firwin(order, freq, pass_zero=False) #true--->false ФНЧ

w, h = sgn.freqz(b) #передаточн фкц
 
plb.plot(w/np.pi,abs(h))#модуль нужен т.к.вещ число

#--------------ФИЛЬТРУЕМ СИГНАЛ------------------#
sig_ff = sgn.filtfilt(b, 1, resultSum)

# ВЫВОДИМ - ЧТО ДОЛЖНЫ ПОЛУЧИТЬ
plb.figure()
plb.plot(t,s1+s2)

# ВЫВОДИМ - РЕЗУЛЬТАТ ФИЛЬТРАЦИИ
plb.figure()
plb.plot(t,sig_ff)