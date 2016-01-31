# -*- coding: utf-8 -*-
"""
Created on Thu Sep 10 11:27:28 2015

@author: NigmatullinR
"""

import scipy.io.wavfile as sw
import numpy as np
import pylab as plb



name = 'voice.wav'

f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

outN = dti[5:16000]

print 'length', len(dti)

print 'sampling frequency', fr

N=len(dti)
t=range(N)
t=np.float32(t)/16000
plb.figure()

plb.plot(t,dti)

