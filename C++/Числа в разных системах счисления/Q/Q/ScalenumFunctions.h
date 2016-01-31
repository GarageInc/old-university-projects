#include<fstream>
#include<stdio.h>
#include<iostream>
#include<conio.h>
#include<vector>
#include<string>
#include<cstring>
#include<algorithm>

using namespace std;

//Структура - список(элементы, который "цепляются" друг за друга адресами next)
typedef struct Node {
    int value;
    struct Node *next;
} Node;


//Вставка в конец списка
void push_back(Node* &first, int int1) 
{
	//Если не нулл
if (first==NULL) 
{
   first=new Node;
   first->next=NULL;
   first->value=int1;
   return;
}
else
{
	Node* last=first;

	while (last->next!=NULL) 
		last=last->next;
;
	last->next=new Node;


	last->next->value=int1;
	last->next->next=NULL;
	return;
}
}

int size(Node* head)
{
	int s=0;
	Node*temp=head;
	while(temp!=NULL)
	{
		temp=temp->next;
		
		s++;
	}

	return s;
}
//Возвращает элемент данного порядка
int element(Node* head, int n)
{
	int nomer=0;
	Node*temp=head;

	while(nomer<n)
	{
		temp=temp->next;
		nomer++;
	}

	return temp->value;
}
//Изменить элемент данного номера
void change(Node*&head, int n)
{
	int nomer=0;
	Node*temp=head;

	while(temp!=NULL && nomer<n)
	{
		temp=temp->next;
		nomer++;
	}

	temp->value=temp->value+1;

}

class Scalenum//Класс для работы с q-ичной системой счисления
    {
	public: int q;//Основание системы счисления
	public: Node* digits;//Список ненулевых цифр в q-ичном представлении числа
	public: Node* degrees;//Список степеней соответствующих цифр

	public: Scalenum(int q = 10)//Конструктор класса с заданным основанием системы счисления:
        {//q - Основание системы счисления
            this->q = q;
			digits=NULL;
			degrees=NULL;
        }
			
	public: string ToString()//Преобразование в строку(переопределение object.ToString())
        {
            string s;//Строка-результат
			Node *temp=digits;
            for (int i = 0; i < size(digits); i++)
			{
				char buffer1[20];
				itoa(element(digits,i),buffer1,10);

				s.append(buffer1);
				s.append(" ");

				char buffer2[20];
				itoa(element(degrees,i),buffer2,10);

				s.append(buffer2);

				s.append("\r\n");

				temp=temp->next;
			}
			return s;
		}
	/*
	public: void Insert(Scalenum s)//Вставка одного числа в центральную часть другого числа:
        {//s - число, которое вставляем
            if (s.q != q)
                cout<<"Числа должны иметь одинаковое основание системы счисления."<<endl;
            Node* digits1;//Список ненулевых цифр результата
            Node* degrees1;//Список соответствующих степеней результата

            for (int i = 0; i < size(digits); i++)//Перебираем все цифры исходного числа
            {
                if (element(degrees,i) <= element(degrees,(size(digits) - 1)) / 2)// Если цифра в первой половине
                {
					push_back(digits1,element(digits,i));//Добавляем ее
                    push_back(degrees1,element(degrees,i));
                    if (element(degrees,i + 1) > element(degrees,(size(degrees) - 1)) / 2)//Если это последняя цифра в первой половине
                    {
						for (int j = 0; j < size(s.degrees); j++)//Вставляем число s
                        {
                            push_back(digits1, element(s.digits,j));
                            push_back(degrees1,element(s.degrees,j) + element(degrees,(size(degrees) - 1)) / 2 + 1);//Так как степень увеличивается на degrees[degrees.Count - 1] / 2 + 1
                        }
                    }
                }
                else//Если цифра во второй половине
                {
                    push_back(digits1,element(digits,i));//Добавляем ее
                    push_back(degrees1,element(degrees,i) + element(s.degrees,size(s.degrees) - 1) + 1);//Так как смещено на s.degrees[s.degrees.Count - 1] + 1
                }
				
            }
            digits=NULL;
            digits = digits1;//Сохранение в исходный
            degrees=NULL;
            degrees = degrees1;//Сохранение в исходный
        }
		*/
};
	

	Scalenum Coding(int n, int q = 10)//Построение числа в q-ичной системе счисления по заданному числу в 10-тичной системе:
        {//n - преобразуемое число;q - основание системы счисления
            Scalenum s(q);//Переменная для результата
            for (int i = 0; n != 0; i++, n /= q)
            {
                if (n % q != 0)
                {
					push_back(s.digits,n % q);
                    push_back(s.degrees,i);
                }
            }
            return s;
        }

	int Decoding(Scalenum &sc)//Построение числа в 10-ичной системе счисления по заданному числу в q -ичной системе:
        {//sc - преобразуемое число
            int n = 0;//Переменная для результата
			for (int i = 0, j = 0, q = 1; i < size(sc.degrees); i++)
            {
                for (; j < element(sc.degrees,i); j++, q *= sc.q) ;
                n += element(sc.digits,i) * q;
            }
            return n;
        }

	void Delete(Scalenum &sc)//Уничтожение списков:
        {//sc - Освобождаемый класс
			sc.digits=NULL;
            sc.degrees=NULL;

        }

	void QMult(Scalenum &sc)//Умножение числа на q:
        {//sc - число которое умножаем
            for (int i = 0; i < size(sc.degrees); i++)
                change(sc.degrees,i);
        }

	void Sum(  Scalenum &sc, int n)//Увеличения числа на на константу 
        {//sc - число;n - константа
            int m = Decoding(sc) + n;
            sc = Coding(m, sc.q);
        }

	int MED(Scalenum &sc)//Нахождение цифры, которая встречается в представлении числа максимальное число раз:
        {//sc - число
            int* a = new int[sc.q];//Количество нахождений цифр в числе
			for (int i = 0; i < size(sc.digits); i++)
                a[element(sc.digits,i)]++;
            a[0] = element(sc.degrees,size(sc.degrees) - 1);
            for(int i = 1;i < sc.q;i++)
                a[0]-= a[i];
            int max = -1, imax = -1;
            for(int i = 0;i < sc.q;i++)
                if (a[i] > max)
                {
                    max = a[i];
                    imax = i;
                }
            return imax;
        }

;

	Scalenum operator *(Scalenum s1, Scalenum s2)//Произведение двух чисел в q-ичной системе счисления:
        {//s1, s2 - числа в q-ичной системе счисления
            if (s1.q != s2.q)
            {
                cout<<"Числа должны иметь одинаковое основание системы счисления."<<endl;
                return NULL;
            }
            Scalenum s(s1.q);//Переменная для результата
            Scalenum q(s1.q);//Переменная для степени q
            Sum(q, Decoding(s1));
			for (int i = 0, j = 0; i < size(s2.degrees); i++)//Перебираем все значащие цифры во втором числе
            {
                for (; j < element(s2.degrees,i); j++, QMult(q)) ;//Доходим до нужной степени
                Sum(s, Decoding(q) * element(s2.digits,i));//Прибавление к результату s1 * q^s2.degrees[i] * s2.digits[i]
            }
            return s;
        }
	;
	Scalenum operator +( Scalenum s1,  Scalenum s2)//Сумма двух чисел в q-ичной системе счисления:
        {//s1, s2 - числа в q-ичной системе счисления
            if (s1.q != s2.q)
            {
				cout<<"Числа должны иметь одинаковое основание системы счисления."<<endl;
                return NULL;
            }
            Scalenum s(s1.q);//Переменная для результата
            Sum(s, Decoding(s1));
            Sum(s, Decoding(s2));
            return s;
        }
	;
	Scalenum operator %(Scalenum s1, Scalenum s2)//Остаток двух чисел в q-ичной системе счисления:
        {//s1, s2 - числа в q-ичной системе счисления
            if (s1.q != s2.q)
            {
                cout<<"Числа должны иметь одинаковое основание системы счисления."<<endl;
                return NULL;
            }
            Scalenum s; s=Coding(Decoding(s1) % Decoding(s2), s1.q);//Переменная для результата
            return s;
        }
	;