# -*- coding: utf-8 -*-
"""
Created on Thu Oct 01 14:11:48 2015

@author"""
from numpy import matrix
from numpy.linalg import*
m=matrix([[1,2,3],[4,-5,3],[0,1,3]])
print 'm',m
m1=matrix([1,2,-7])
m2=m1*m
print 'm2',m2
m3=m*m1.T
a=det(m)
print 'det',a
n=inv(m)
print 'inv', n
c,d=eig(m)
print c,d