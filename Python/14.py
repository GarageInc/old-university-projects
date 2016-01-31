

import scipy.io.wavfile as sw 
from scipy.signal import * 
import numpy as np
import pylab as plb

# функция децимации
def doDecimation(s,t,M):
    Fs=len(t)
    N=200#миллисекунды сигнал
    
    # сигнал оригинальный
    plb.figure()
    plb.plot(t[0:N],s[0:N])
    plb.title('Original signal')
    
    # ПФ для оригинального сигнала
    fs=np.fft.fft(s[:N])#преобр Ф
    
    plb.figure()
    plb.plot(abs(fs))
    #plb.plot(abs(fs[:N/8]))
    plb.title('FFT of original signal. Fs=' + str(Fs))
    
    # проводим децимацию
    t2=range(0,len(s),M)#оъявляем диапазон
    
    #y=np.delete(s,t2) #- функция работает неправильно, удаляет каждый M-ый элемент
    y=s[t2]#потерял сэмплы и гладкость
    print s[:10]
    print y[:10]

    # сигнал после децимации    
    plb.figure()
    plb.plot(y[0:N])
    plb.title('Signal after decimation') # сигнал "уплотнился" в M-раз
        
    # ПФ после децимации
    fs2=np.fft.fft(y[:N])
    
    plb.figure()
    plb.plot(abs(fs2))
    plb.title('Fs=' + str(Fs/np.float64(M)))
    

# Считываем участок с голосом и выводим его
# ОТКРЫВАЕМ ФАЙЛ ДЛЯ ЧТЕНИЯ
name = 'voice.wav'
f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

t=range(55000-30000)
s= dti[30000:55000] #берем учасок с голосом

M=2 # decimation factor

doDecimation(s,t,M)



