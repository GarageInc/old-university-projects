
import numpy as np
import pylab as plb
import scipy.signal as sgn

# функция вывод передаточной функции для различных N
def differentN(s,N):
    # Выводим коэффициенты
    b_1=sgn.hilbert(s,N)
    
    w_1, h_1 = sgn.freqz(b_1) #передаточн фкц   
    
    plb.figure()
    plb.plot(w_1/np.pi,abs(h_1))#модуль нужен т.к.вещ число
    plb.title("Hilbert N = " + str(N))
    
Fs=100
w=5
t=np.float64(range(0,Fs))/Fs
s=np.sin(2*np.pi*w*t)

s_ort=np.imag(sgn.hilbert(s))#извлекаем вторую часть, получем мним часть от аналитич сигнала

plb.figure()
plb.plot(t,s, color='blue', label='Original')
plb.plot(t,s_ort, color='red', label='Shifted')#синусоида после преобраз Гильберта сдвинута на пи пополам
plb.plot(t,s*s_ort, color='green', label='Product')#сумма всех элем

print 'sum: ', sum(s*s_ort)#близка к 0

# Вывод передаточных функций фильтра Hilbert для различных N
mass=[None,1,5,10,100]
for n in mass:
    differentN(s,n)
    
# Фазовая и групповая задержки, N=10
b=sgn.hilbert(s,10)    
w, h = sgn.freqz(b) #передаточн фкц   
hn=h/(abs(h)) #нормируем

plb.figure()
plb.plot(w, np.angle(hn)) #угол соотв фазе
plb.plot(w, np.angle(hn)/w) #фазовая задержка
plb.title("Hilbert N (phase) = " + str(10))

hn=h/(abs(h))
ha=np.angle(hn)
gd=np.diff(ha)/np.diff(w) #групповая задержка, разность от фазы и от частот

plb.figure()
plb.plot(w[:-1],gd)
plb.title("Hilbert N (group) = " + str(10))