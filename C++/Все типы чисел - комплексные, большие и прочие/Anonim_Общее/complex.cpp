#include <iostream>
#include <algorithm>
#include <math.h>

#include<complex.h>
using namespace std;


Complex sum(Complex a, Complex b);//Получаем сумму. Реализация - ниже main
Complex dif(Complex a, Complex b);//Получаем разность
Complex mult(Complex a, Complex b);//Получаем перемножение


Complex::Complex()//Конструктор по умолчанию
{
	real = 0;
	imag = 0;
}

Complex::Complex(double real, double imag)//Конструктор по полученным - если переданы значения действительной и мнимой частей
{
	this->real = real;
	this->imag = imag;
}

Complex::~Complex()//Деструктор
{
	real = 0;
	imag = 0;
}

void Complex::read()//Чтение комплексного числа - его элементов
{
	cout << "Действительная часть = ";
	cin >> real;
	cout << "Мнимая часть = ";
	cin >> imag;
}

void Complex::output()//Редактурем вывод числа
{
	if (real && imag)
	{
		cout << real;
		if (imag > 0)
			cout << '+';
		if ((imag != 1) && (imag != -1))
			cout << imag << 'i' << endl;
		if (imag == 1)
			cout << 'i' << endl;
		if (imag == -1)
			cout << "-i" << endl;
		return;
	}
	if (real)
	{
		cout << real << endl;
		return;
	}
	if (imag)
	{
		if ((imag != 1) && (imag != -1))
			cout << imag << 'i' << endl;
		if (imag == 1)
			cout << 'i' << endl;
		if (imag == -1)
			cout << "-i" << endl;
		return;
	}
	cout << 0 << endl;
}

double Complex::getreal()//Функция - получить действительную часть комплексного числа
{
	return real;
}

double Complex::getimag()// - получить мнимую часть
{
	return imag;
}

Complex sum(Complex a, Complex b)//Сумма
{
	Complex c(a.getreal() + b.getreal(), a.getimag() + b.getimag());
	return c;
};

Complex dif(Complex a, Complex b)//Разность
{
	Complex c(a.getreal() - b.getreal(), a.getimag() - b.getimag());
	return c;
}

Complex mult(Complex a, Complex b)//Умножение
{
	Complex c(a.getreal() * b.getreal() - a.getimag() * b.getimag(), a.getreal() * b.getimag() + a.getimag() * b.getreal());
	return c;
}
