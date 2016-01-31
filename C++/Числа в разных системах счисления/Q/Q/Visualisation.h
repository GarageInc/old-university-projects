#include<fstream>
#include<stdio.h>
#include<iostream>
#include<conio.h>
#include<vector>
#include<string>
#include<algorithm>

;
int initialize();
int Choise();
void Read(Scalenum &sc, ifstream &rd);
void Write(Scalenum sc, ofstream &wr);

	int Initialize()//«адание системы счислени€:
        {
            cout<<"¬ведите основание системы счислени€:"<<endl;
            int  t;
			cin>>t;

            return t;
        };

	int Choice()//¬ыбор действи€:
        {
            cout<<"¬ведите номер дл€ действи€:"<<endl;
            cout<<"1 - дл€ суммировани€ двух чисел;"<<endl;
            cout<<"2 - дл€ умножени€ двух чисел;"<<endl;
            cout<<"3 - дл€ нахождени€ остатка от делени€;"<<endl;
            cout<<"4 - exit(выход)."<<endl;
			int t;
			cin>>t;
            return t;
        };

	void Read(Scalenum &sc, ifstream &rd)//—читывание числа из файла:
        {//sc - число в которое считываем;rd - файловый поток откуда считываем
			int t;
			rd>>t;
            sc = Coding(t, sc.q);
        };


	void Write(Scalenum sc, ofstream &wr)//¬ывод данных о числе 
        {//sc - число;wr - файловый поток в который записываем
            wr<<"ѕредставление числа в дес€тичной системе:" << Decoding(sc)<<endl;//10-ична€ система счислени€
            wr<<"ѕредставление числа в q-ичной системе счислени€:\r\n" << sc.ToString()<<endl;//q-ична€ система счислени€
        };