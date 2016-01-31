# -*- coding: utf-8 -*-
"""
Написать польз фкц л.29.10.2015
"""
import numpy as np
import pylab as plb

def ht(t,T):
    res=np.sin(np.pi*t/T)/(t/np.pi)
    ht=res
    return ht


fr=10 #частота сигнала
n=6020 #частота стробирования
T=np.float64(1.0)/fr #период

t=np.float32(range(-n/2,n/2,10))/1000#10- чтобы 0 не попал
#конвертируем вещ-ый формат
print t

s=np.sin(2*fr*np.pi*t)
plb.figure()
plb.plot(t,s)


# создаем передаточную функцию sinc=hT
h=[]

for el in t:
    res = ht(el,T)
    h.append(res)

# выводим результат
plb.figure()
plb.plot(t,h)


# восстановление аналоговой формы(считаем, что будто стробировали сигнал)
k=100
ya=[]
# http://bourabai.ru/signals/img/Image795.gif
for el in t:
    sum = 0.0
    for i in range(k):
        if el-i==0:
           sum = sum + 1
        else:            
            sum = sum + ht(el-i,T)
    ya.append(sum)
    

plb.figure()
plb.plot(t,ya)