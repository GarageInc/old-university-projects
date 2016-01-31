# -*- coding: utf-8 -*-
"""
HW 1.1
Реализовать функция для вычисления 
числа e с точностью до 7 знаков после запятой.
Использовать расложение фукнции e(x) в ряд.

"""
#import math
def E():
    n=10    
    exp=1
    factorial=1
    for i in range(1,n):
        factorial=factorial*i
        exp+=1.0/factorial
    return round(exp,7)
    
print E()

