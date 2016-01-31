#pragma once
using namespace System;
using namespace System::Collections;



ref class Expression//класс
{
	String^ s;//строка,в которой будет выражение
	ArrayList^ vars;//список переменных
public:
	Expression(String^ str);//конструктор
	bool FindVars();//ищет переменную
	double Calculate(double x);//считает значение
	ArrayList^ GetVars();//возвращает список значений
	
	bool IsCorrect();//проверяет правильность
	String^ GetS();//возвращает строку
};

