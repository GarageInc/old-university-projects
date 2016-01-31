

from scipy.signal import *
import scipy.io.wavfile as sw
import numpy as np
import pylab as plb
import numpy.fft as nf


name = 'voice.wav'

f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

N=5250 # больше 20мс

print len(dti)

s= dti[23500:34000]

plb.figure()
plb.plot(s)

tiho=s[:N]
slovo = s[N:]

plb.figure()
plb.plot(tiho)

plb.figure()
plb.plot(slovo)

h=hann(N)
print len(h)
plb.figure()
plb.plot(h)

y_slovo=slovo*h
y_tiho=tiho*h

plb.figure()
plb.plot(y_slovo)

plb.figure()
plb.plot(y_tiho)


y_slovo_fft = nf.fft(y_slovo)
plb.figure()
plb.plot(abs(y_slovo_fft))

y_tiho_fft = nf.fft(y_tiho)
plb.figure()
plb.plot(abs(y_tiho_fft))


#s_fft = nf.fft(s)
#plb.figure()
#plb.plot(abs(s_fft))

#h_fft = nf.fft(h)
#plb.figure()
#plb.plot(abs(h_fft))