# -*- coding: utf-8 -*-
"""
Created on Thu Oct 15 13:11:49 2015

@author: NigmatullinR
"""
from scipy.signal import * #
import scipy.io.wavfile as sw
import numpy as np
import pylab as plb

N=1024

h=hann(N) #окно хеминга
print len(h) #отображ его
plb.figure()
plb.plot(h)

name = 'voice.wav'

f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

s= dti[40000:41024] #берем учасок с индексами



plb.figure()

plb.plot(s)


y=s*h  #окно умнож на соотв-ий участок
'''f=nf.fft(s)
print f
print abs(f)
plb.figure()
plb.plot(abs(f))

f1=nf.fft(y)
print f1
print abs(f1)
plb.figure()
plb.plot(abs(f1))
'''
plb.figure()

plb.plot(y)
