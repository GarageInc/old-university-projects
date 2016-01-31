# -*- coding: utf-8 -*- 
""" 
Created on Thu Oct 15 12:30:54 2015 

@author: NigmatullinR 
""" 

import numpy as np 
import pylab as plb 
import math as math
import scipy.fftpack as scfft 

# функция вывода
def printSignal(signal, t = None): 
    if (t is None):   
        plb.figure() 
        print "t is None"
        plb.plot(signal)
    else:   
        plb.figure() 
        print "->"
        plb.plot(t,signal)

    
# генерируем и выводим 2 сигнала
w=10 
T=1000
t=np.float32(range(T))/T 
sinus=np.sin(2*w*np.pi*t) 
#printSignal(t,sinus)

cosinus = np.sin(2*w*np.pi*t) 
#printSignal(t,cosinus)

# находим преобразования Фурье двух сигналов
fft_sinus=scfft.fft(sinus) 
fft_cosinus=scfft.fft(cosinus) 

# обратное преобразование Фурье
ifff_sinus = scfft.ifft(fft_sinus) 
ifff_cosinus = scfft.ifft(fft_cosinus) 

# проводим свертку двух преобразований фурье
# проверка: свертка двух последовательностей равна обратному ПФ от произведения ПФ двух последовательностей 
H = fft_sinus*fft_cosinus # произведение преобразований
# возьмём обратное преобразование Фурье:
h = scfft.ifft(H) 


# теперь проведем собственную свертку в цикле 
index = 0 # просто итератор
h_new = [] # вторая свертка(как исскуственное произведение

sum1 = 0 
sum2 = 0

while(index < len(sinus)): 
    sum1 = sum1 + H[index]/T
    
    mult_value = sinus[index] * cosinus[T - 1 - index] 
    h_new.append( mult_value ) 

    sum2 = sum2 + mult_value 
    
    index += 1 

#printSignal(h,t)
#printSignal(h_new,t)
# plb.plot(t,h2) 

# ВЫВОД СРАВНЕНИЙ ДВУХ ПРОИЗВЕДЕНИЙ
print 'Сумма первого(библиотечное) = ',math.fabs(sum1)
print 'Сумма второго(посчитан руками) = ',math.fabs(sum2)
print 'ПРИБЛИЗИТЕЛЬНО РАВНЫ! преобразование Фурье от свертки двух последовательностей есть произведение соответвующих образов Фурье исходных последовательностей'

print 'Сравнение двух значений(библиотечного и посчитанного руками)'
c=np.convolve(h,h_new)
print 'c:', c, 'len = ', len(c)
printSignal(c)
