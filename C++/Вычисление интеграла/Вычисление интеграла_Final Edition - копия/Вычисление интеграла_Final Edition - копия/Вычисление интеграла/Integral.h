#pragma once
#include "FunctionPainter.h"

ref class Integral//класс интеграл
{

	double minlim;//нижний и верхний пределы интегрирования
	double maxlim;

public:
	Integral(double min,double max);//конструктор
	

	String^CalcK(String^ a);//метод,который считает значение интеграла и возвращает значение в стринг
	
};
