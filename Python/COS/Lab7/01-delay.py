# -*- coding: utf-8 -*-
"""
Created on Thu Nov 19 12:20:29 2015

@author: NigmatullinR
"""
 #что такое задержка и как зависит от фильтра, кот применяем
import numpy as np #
import pylab as plb
import scipy.signal as sgn

w=5 #частота
Fs=100 #частота дискр

t=np.float32(range(100))/Fs
s=np.sin(2*w*np.pi*t)
print t
plb.figure()
plb.plot(t,s)


order=20; #констр фильтр ФИР, коэф 10
freq=0.9 #от 0 до 45 Гц пропускает, на гр 45- проа-подавл не оч хорошо

b=sgn.firwin(order, freq, pass_zero=True) #ФНЧ

s1 = sgn.lfilter(b, 1, s) #примен коэф
plb.figure()

plb.plot(s, color='blue', label='Original') #синий оригинальный
plb.plot(s1, color='red', label='filtered') #после фильтрации
