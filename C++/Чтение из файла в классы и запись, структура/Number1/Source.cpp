
#define _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_DEPRECATE

#include <iostream>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <stdio.h>

using namespace std;

int const N = 194;
struct person{

	int no;
	string fname;
	string lname;
	string mname;
	string num;
	char gender;
	string st;
	int n1;
	int n2;
	string s;
	char c;
	int num1;
	int num2;
} myPers[N];

int StrToInt(string &str){
	return atoi(str.c_str());
}

void sort(person *S, int N)
{
	person Y;
	for (int i = 0; i < N - 1; i++)
		for (int j = i + 1; j < N; j++){
			if (strcmp(S[i].lname.c_str(), S[j].lname.c_str())>0)
			{
				Y = S[i];
				S[i] = S[j];
				S[j] = Y;
			}
			swap(S[i], S[j]);
		}
}

void ShowData(person*myPers, int N)
{
	for (int i = 0; i<N; i++)
	{
		cout << myPers[i].no << " " << myPers[i].fname << " " << myPers[i].lname << " " << myPers[i].mname << " " << myPers[i].num << " " << myPers[i].gender;
		cout << " " << myPers[i].st << " " << myPers[i].n1 << " " << myPers[i].n2 << " " << myPers[i].s << " " << myPers[i].c << myPers[i].num1 << " " << myPers[i].num2 << endl;
	}
}

int main(int argc, char** argv) {
	freopen("SPISOK_2.txt", "w", stdout);
	freopen("SPISOK_1.txt", "r", stdin);
	string S, X[20];
	
	char *Ptr = NULL;
	int j = 0;
	for (int i = 1; i <= N; i++){
		myPers[i].no=i;
		cin>> myPers[i].fname >> myPers[i].lname >> myPers[i].mname >> S;
		Ptr = strtok((char *)S.c_str(), ":");
		j = 0;
		X[0] = Ptr;
		while (j<8){
			Ptr = strtok(0, ":");
			j++;
			X[j] = Ptr;
		}
		X[j] = Ptr;

		myPers[i].num = StrToInt(X[0]);
		myPers[i].gender = X[1][0];
		myPers[i].st = X[2].c_str();
		myPers[i].n1 = StrToInt(X[3]);
		myPers[i].n2 = StrToInt(X[4]);
		myPers[i].s = X[5].c_str();
		myPers[i].c = X[6][0];
		myPers[i].num1 = StrToInt(X[7]);
		myPers[i].num2 = StrToInt(X[8]);
	}

	sort(myPers, N);
	ShowData(myPers, N);

	cin.get();

	return 0;
}
