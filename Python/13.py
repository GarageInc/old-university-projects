
 #групповая задержка
import numpy as np
import scipy.signal as sgn
import pylab as plb


# создаем IIR-фильтр, частота отсечения 0.8
order=13;
freq=0.8

b, a=sgn.iirfilter(order, freq, btype='lowpass')#bandstop

print 'Коэффициенты:', b

w, h = sgn.freqz(b)
 
plb.title("FILTER:")
plb.plot(w/np.pi,abs(h))

hn=h/(abs(h))

ha=np.angle(hn)

gd=np.diff(ha)/np.diff(w) #групповая задержка, разность от фазы и от частот

plb.figure()
plb.plot(w[:-1],gd)

# СОЗДАЕМ CHIRP-СИГНАЛ И ВЫВОДИМ ЕГО ДО ФИЛЬТРАЦИИ
Fr=200
t=np.float32(range(Fr))
print t
chirpSig = sgn.chirp(t,f0=100,t1=0.5,f1=200)
plb.figure()
plb.title("Before filtering")
plb.plot(t,chirpSig)

result = sgn.lfilter(b,1,chirpSig)
plb.figure()
plb.title("After filtering(lowpass)")
plb.plot(t,result)