#include "Expression.h"
#include <math.h>

Expression::Expression(String^ str)//вычисление выражения
{
	s = str;//строка из которой считываем
	vars = gcnew ArrayList;//список переменных
}
bool Expression::FindVars(){//метод,который ищет переменные
	vars->Clear();//очищаем,если что-то было
	bool b = false;//наличие переменных изначале считаем их нет
	array <String^>^ arr = s->Split(' ');//разбиваем строку по пробелам	
	for each (String^ s in arr){	// для каждой строки в этом массиве пытаемся преобразовать ее в число		
		try{
			double k = Double::Parse(s);
		}
		catch(...){//если эс не равно чему-то из этого,значит у нас есть переменные какие-то
			if(s!="+"&&s!="-"&&s!="*"&&s!="/"&&s!="^"&&s!=""&&s!="x"&&s!="-x"){
				b = true;//да,они есть
				String^s1;//создаем новую строку эс1
				if(s[0]!='-'&&s[0]!='+')s1 = s;//если первый символ не мину  и не плюс,то эс1 равно эс
				else s1 = s->Remove(0,1);//иначе мы этот символ минус или плюс удаляем
				if(!vars->Contains(s1))vars->Add(s1);//если переменной в списке не было,то добавляем ее в список переменных
			}
		}
		
	}	
	return b;
}

double Expression::Calculate(double x){//вычисляем значение этого выражения от переменной икс
	String^ str = s->Replace("x",x.ToString());//заменаем все вхождения икс на ее значение
	array<String^>^ symb = str->Split(' ');//опять разбиваем по пробелам строки
				 Stack^ st = gcnew Stack;//в стек будем помещать значения символов
				 double a,b;//нужно будет
				 double k;
				 for(int i=0;i<symb->Length;i++){//проверяем,чтобы два минуса подряд не шли
					 if(symb[i]->Length>1&&symb[i][0]=='-'&&symb[i][1]=='-')symb[i]=symb[i]->Remove(0,2);
					 if(symb[i]->Length>1&&symb[i][0]=='+'&&symb[i][1]=='+')symb[i]=symb[i]->Remove(0,2);
					 if(symb[i]->Length>1&&symb[i][0]=='-'&&symb[i][1]=='+')symb[i]=symb[i]->Remove(1,1);
					 if(symb[i]->Length>1&&symb[i][0]=='+'&&symb[i][1]=='-')symb[i]=symb[i]->Remove(0,1);
					if(String::Compare(symb[i],"")){//если не пустая строка 
					 try{
						 k = Double::Parse(symb[i]);//пытаемся преобразовать в число
						 st->Push(k);//и помещаем на вершину
					 }
					 catch(...){//если это не число
						 int count = st->Count;//кол-во элементов в строке
						 if(count)//если это не 0,то мы снимаем верхний элемент стека
						 a = (double)st->Pop();	//как раз снимаем
						 count = st->Count;//снова считаем кол-во элементов в стеке
						 if(count){//если не 0,снимаем еще один элемент
						b = (double)st->Pop();//как раз это оно и есть
						 }
						 			 
							 else break;//если было 0,то прерываемся
						if(symb[i]=="+"){//выполняем действие
							
							st->Push(a+b);
						}
						if(symb[i]=="-"){
							
							st->Push(b-a);
						}
						if(symb[i]=="*"){
							
							st->Push(a*b);
						}
						if(symb[i]=="/"){
							try{
							st->Push(b/a);
							}
							catch(...){
								return 0;
							}

						}
						if(symb[i]=="^"){							
							st->Push(pow(b,a));
						}
						 
					 }
					}
				 }
				 int con = st->Count;//считает кол-во элементов стека
				 if(con!=1)throw "er";//если не 1,то ошибка
				
				double ans = (double)st->Pop();//если один элемент,то его забираем из стека
				return ans;//возвращаем ответ
}
ArrayList^ Expression::GetVars(){//просто возращаем список элементов
	return vars;
}


	
		
bool Expression::IsCorrect(){//проверяет корректность выражения
	array<String^>^ symb = s->Split(' ');//разбиваем на пробелы
	int kol=0;//условное количество элементов стека
	for each(String^ s in symb){//для каждой строки разрыва,если мы встречаем знак операции,то мы его уменьшаем,если что-то другое,то увеличиваем
		if(s=="+"||s=="-"||s=="*"||s=="/"||s=="^")
			kol--;
		else if(s!="") kol++;
	}
	if(kol==1) return true;//если кол =1,то все верно
	else return false;
}
String^ Expression:: GetS(){//просто возвращаем строку
	return s;
}
