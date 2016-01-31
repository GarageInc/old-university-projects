# -*- coding: utf-8 -*-
"""
Created on Thu Oct 15 12:58:51 2015

@author: NigmatullinR
"""
import scipy.io.wavfile as sw 
import numpy as np
import scipy as sp
import pylab as plb
import math

########################################
def generator(pol,Si):# полином(матрица А) и вектор Si
    
    # создаем сдвинутую диагональную матрицу с полиномом в нижней строке
    M=np.size(Si)
    a=np.diag(np.ones(M-1))
    #print a 
    zeros = np.zeros((M-1,1))
    #print zeros
    newM_1 = np.concatenate((zeros,a),axis=1)
    #print newM_1   
    #print pol 
    newM_2 = np.concatenate((newM_1,[pol]),axis=0)  
    
    # результат умножения матрицы A на вектор Si будет вектор 
    result = np.dot(newM_2,Si)
    # на выходе - псевдосл.пслдв
    return result;
    
#######################################
# перемножение векторов
def multVects(n,m):
    if (len(n)!=len(m)):
        print n,m
        print "DANGER!";

    sum = 0.0   
    for i in range(len(n)):
        sum = sum + n[i]*m[i]
        
    return math.fmod(int(sum),2)

#######################################
N=12#число элементов в векторах
# задаем свой неприводимый полином:
pol = [1,1,0,1,1,1,1,1,0,1,0,1]
# задаем наш вектор, который и будет WATERMARK'ом. Он равен y[i]=D*Si
y=[] # состоит из 3х элементов


# задаём свой вектор d для ЛПМ
D =[1,-1, 1,-1,1,-1, 1,-1,1,-1, 1,-1]
# начальный вектор S0
Si=[1,1,0,1,1,1,1,1,0,1,0,1]#[-1, 1,-1,-1,1, 1,-1,-1,1, 1,-1,-1]

# генерируем вектора s[i]
for i in range(N):
    res = multVects(Si,D)
    if res==0:
        multResult = 1
    else:
        multResult = multVects(Si,D)
    #print multResult
    y.append(multResult)
    Si = generator(pol,Si)
    

# задаем некоторый коэффициент coef
coef=120
index=0
for el in y:
    y[index]*=coef
    index+=1
# цифровая подпись
print "Массив 'y': ",y

# применим цифровую подпись для следующего сигнала:
# Считываем участок с голосом и выводим его
# ОТКРЫВАЕМ ФАЙЛ ДЛЯ ЧТЕНИЯ
name = 'voice.wav'
f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

t=range(55000-54900)
s= dti[54900:55000] #берем учасок с голосом

# выведем  последовательность-cbuyfk
print "Массив голоса: ",s
plb.figure()
plb.plot(s)
plb.title("Voice")

# наша подпись встраивается:
s[10:22] = s[10:22] + y

s[22:34] = s[22:34] + y

s[34:46] = s[34:46] + y
plb.figure()
plb.plot(s)
plb.title("Voice with watermark")

# корреляция - ясно показывает, где наш встроенный watermark
c=np.correlate(s,y,'full')
plb.figure()
plb.plot(c)
plb.title("Correlate")


print np.argmax(c)