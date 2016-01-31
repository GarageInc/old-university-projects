#include "stdafx.h"


//Класс - комплексные числа
class Complex
	{
	private:
		double real;
		double imag;
	public:
		Complex();
		Complex(double real, double imag);
		~Complex();
		
		void read();
		void output();
		double getreal();
		double getimag();
		
		Complex ZvMinusPervoiStepeni();//Комплексное число в минус первой степени
		
	};

	Complex operator+(Complex a, Complex b);
	Complex operator-(Complex a, Complex b);
	Complex operator*(Complex a, Complex b);//Умножение комплексных чисел
	Complex operator*(int a, Complex b);//Умножение целого числа на комплексное
	Complex operator/(Complex a, int b);//Деление комплексного числа на целое обычное
	

	//Конструкторы
	Complex::Complex()
	{
		real = 0;
		imag = 0;
	}

	Complex::Complex(double real, double imag)
	{
		this->real = real;
		this->imag = imag;
	}
	//Деструктор
	Complex::~Complex()
	{
		real = 0;
		imag = 0;
	}
	/*
	void Complex::output()
	{
		if(real && imag)
		{
			cout << real;
			if(imag > 0)
				cout << '+';
			if((imag != 1) && (imag != -1))
				cout << imag << 'i' << endl;
			if(imag == 1)
				cout << 'i' << endl;
			if(imag == -1)
				cout << "-i" << endl;
			return;
		}
		if(real)
		{
			cout << real << endl;
			return;
		}
		if(imag)
		{
			if((imag != 1) && (imag != -1))
				cout << imag << 'i' << endl;
			if(imag == 1)
				cout << 'i' << endl;
			if(imag == -1)
				cout << "-i" << endl;
			return;
		}
		cout << 0 << endl;
	}*/

	//Получение действительной части
	double Complex::getreal()
	{
		return real;
	}
	//Получение вещественной части числа
	double Complex::getimag()
	{
		return imag;
	}

	//Функции сложения, вычитания и умножения
	Complex operator+(Complex a, Complex b)
	{
		Complex c(a.getreal() + b.getreal(), a.getimag() + b.getimag());
		return c;
	};

	Complex operator-(Complex a, Complex b)
	{
		Complex c(a.getreal() - b.getreal(), a.getimag() - b.getimag());
		return c;
	}

	Complex operator*(Complex a, Complex b)
	{
		Complex c(a.getreal() * b.getreal() - a.getimag() * b.getimag(), a.getreal() * b.getimag() + a.getimag() * b.getreal());
		return c;
	}

	Complex operator*(int a, Complex b)
	{
		Complex c(a * b.getreal(), a* b.getimag());
		return c;
	}

	Complex operator/(Complex a, int b)
	{
		Complex c(a.getreal()/b, a.getimag()/b);
		return c;
	}
	//Комплексное число в минус первой степени
	Complex Complex::ZvMinusPervoiStepeni()
	{
		Complex c(real/(real*real+imag*imag),(-1)*imag/(real*real+imag*imag));
		return c;
	}

	