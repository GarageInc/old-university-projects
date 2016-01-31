#pragma once
#include "Converter.h"
#include "DecartPainter.h"
#include "Expression.h"
#include <math.h>

using namespace System::Drawing;//используем графикс,поэтому




ref class Function//класс функция
{
protected:
PanelParameters^ pp;//параметры окна
Expression^ ex;//вызываем тип класса 
double inf,sup;//поля класса
public:
	Function(Expression^ ex){//конструктор присваивает полю ех какое-то значение
		this->ex = ex;		
		
	}
	double Calc(double x){//подсчет значений функции от параметра х,для рисования
		return ex->Calculate(x);
	}
	void FindInfSup(double xmin,double xmax);//метод,ищет минимум и максимум
	double GetSup();//возвращает значение супремума и инфимума
	double GetInf();
};



ref class FunctionPainter:DecartPainter//классрисует функцию на панели
{
protected:
	Function^ p;//поле класса
	
public:
	FunctionPainter(Function^ p,PanelParameters^pan);//конструктор
	void DrawFunc(Graphics^ g,double min,double max);//метод,который рисует функцию

};

