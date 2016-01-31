#
import numpy as np
import pylab as plb
import scipy.signal as sgn
import math

Fs=100
w1=10
w2=30

# функция получения аналитического сигнала
def showAnalytic(t,S, name):
    
    S_a=sgn.hilbert(S)#берем аналитич сигнал
    
    plb.figure()
    plb.plot(t,S, color='blue', label='Original')
    plb.plot(t,abs(S_a), color='red', label='Analytic')#взяли модель= мгновенная амплитуда каждый раз=1
    plb.title("Result: " + name)
    plb.legend()
    
    fs=np.fft.fft(S)#пр Ф от Ориг
    fa=np.fft.fft(S_a)#пр Ф от аналитич сигн-удваение спектра, обнуляетс сигнал во второй поовине
    
    plb.figure()
    plb.plot(abs(fs), color='blue', label='Original')
    plb.plot(abs(fa), color='red', label='Analytic')
    plb.title("FFT: " + name)
    plb.legend()
    
# для суммы синусоидальных сигналов
t=np.float32(range(0,Fs))/Fs

S1=np.sin(2*np.pi*w1*t)
S2=np.sin(2*np.pi*w2*t)

S=S1+S2
showAnalytic(t,S, "sum sin")

# для chirp-сигнала
chirpSig = sgn.chirp(t,f0=100,t1=0.5,f1=200)
showAnalytic(t,chirpSig, "chirp-signal")

# для всплеска Гаусса
i, q, e = sgn.gausspulse(t, fc=Fs, retquad=True, retenv=True)
showAnalytic(t,i+q, "gausspulse-signal")# реальная и имагин-части

# модулированный экспонентой  exp(-A*t)sin(f0+B*t^2)
esig = []
A=10
B=10
f0=10
for x in t:
    res = math.exp(-A*x) * np.sin(f0 + B*(x**2))
    esig.append(res)
    
showAnalytic(t,esig,"exp-signal")