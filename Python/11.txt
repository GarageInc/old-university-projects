
import numpy as np #
import pylab as plb
import scipy.signal as sgn

# По условию задачи - нужно было сконструировать этот фильтр. 
# Теоретически - сконструирован, практически - не применён. Рябит в глазах от больших частот

w=5 #частота
Fs=2000 #частота дискр

t=np.float32(range(Fs))/Fs
s=np.sin(2*w*np.pi*t)

# ВЫВОД СГЕНЕРИРОВАННОГО СИГНАЛА
plb.figure()
plb.plot(t,s)

# функция собственного стоп-банд фильтра - глушение частот от 100 до 700 гц
def stopBandFilter(s,t):
    #----------------СОЗДАНИЕ ФИЛЬТРА ФИР------------------#
    order=50; #констр фильтр ФИР, коэф 10
    freq_1=0.1 
    freq_2=[0.7,0.99] 
    
    b_1=sgn.firwin(order, freq_1, pass_zero=True) # пропускаются все до 100гц
    b_2=sgn.firwin(order, freq_2, pass_zero=False) # пропускаются все с 700 гц
    w_1, h_1 = sgn.freqz(b_1) #передаточн фкц   
    w_2, h_2 = sgn.freqz(b_2) #передаточн фкц             
    s1 = sgn.lfilter(b_1, 1, s) #примен коэф        
    s2 = sgn.lfilter(b_2, 1, s) #примен коэф
    
    plb.figure()
    plb.plot(w_1/np.pi,abs(h_1))#модуль нужен т.к.вещ число
    plb.plot(w_2/np.pi,abs(h_2))#модуль нужен т.к.вещ число
    plb.plot(t,s, color='blue', label='Original') #синий оригинальный
    plb.plot(t,s1, color='red', label='filtered') #после фильтрации - виден сдвиг
    
    # РЕЗУЛЬТАТ:
    result = s1+s2
    print result
    
    return result
    
# вызывается функция стоп-банд фильтра
result = stopBandFilter(s,t)

plb.figure()
plb.plot(t,result)    

