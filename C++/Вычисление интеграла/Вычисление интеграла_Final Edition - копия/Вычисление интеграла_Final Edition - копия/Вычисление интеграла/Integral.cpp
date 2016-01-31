#include "Integral.h"
#include <time.h>
#include <stdlib.h>
#include"PolandStr.h"
#include"Expression.h"
#include<iostream>
using namespace std;
using namespace System::Threading;//для потока оба
using namespace System::Threading::Tasks;


Integral::Integral(double min,double max){//конструктор с параметрами
	minlim=min;
	maxlim=max;
	
}

String^Pervoobr(String^ s){     //находим первообразную полинрма  
	String^r=")";//в скобку заключаем в скобку
	String^r1="(";
	r1=r1+s;//строки
	s=r1;//строку добавляем в конец или в начало
	s=s+r;//тут уже сзади
	String^k="";//создаем пустую строку,чтобы первообразную туда написать
	for(int i=0;i<s->Length;i++){//по длине запускаем цикл,ищем первообразную
		if(isdigit(s[i])&&(s[i+1]=='+'||s[i+1]=='-'||s[i+1]==')')&&!(s[i-1]=='^')){//если цифра,константа на икс
		k=k+s[i]+"*x";
		}
		else{
			if((isdigit(s[i])||s[i]=='*'||s[i]=='/')&&!(s[i-1]=='^')){//умножение,деление
				k=k+s[i];
		}
			else{if(s[i]=='x'&&(s[i+1]=='+'||s[i+1]=='-'||s[i+1]==')')){//икс
				k=k+s[i]+"*x/2";
			}
			else{
				if(s[i]=='x'&&s[i+1]=='^'){//икс в какой-то степени
					k=k+s[i]+"^"+s[i+2]+"*x/"+"("+s[i+2]+"+1)";//если икс в квадрате,то превращем в икс в кубе деленный на три
				
				}
				else{
					if(s[i]=='+'||s[i]=='-'){//просто плюс записываем,просто минус тоже
					k=k+s[i];
					}
					else{
						}
					   }
					   }
				       }
				       }
	                   }
	return k;//возвращаем
	                  }
		

String^Integral::CalcK(String^ a){//метод высчитывания первообразной
	/*{Первооборазная}*/
	String^perv=Pervoobr(a);//а закидываем в первообразную 
	PolandStr^первообораз=gcnew PolandStr(perv);//эту строчку переводит в польскую запись,чтоб считать,создаем объект класса польская запись
	String^ pols=первообораз->GetPolandStr();//высчитываем от какой-то точки
	Expression^znach=gcnew Expression(pols);//создаем объект класса экспрешн
	double verh=znach->Calculate(maxlim);//верний и нижний предел интегрирования 
	double vniz=znach->Calculate(minlim);
	Double integral=verh-vniz;//вычитаем 

	return integral.ToString();//переводим обратно в строку
}

