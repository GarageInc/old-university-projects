#include <iostream>
#include <conio.h>

using namespace std;
const int deg=100;

template <class T>
class poly{
public:
	poly();
	poly(int s);
	poly operator *(poly a);
	poly operator +(poly a);
	void get(int deg, T val);
	T set(int deg);
	void Out();
private:
	T p[deg];
	int degree;
};

template <class T>
poly<T>::poly(){
	for(int j=0;j<=deg;j++)
		{p[j]=0;};
};

template <class T>
poly<T>::poly(int s){
	degree =s;
	p[degree]=rand()%10+1;
	for(int i=0;i<degree;i++){
		p[i]=rand()%10;
	}
}
template <class T>
poly<T> poly<T>::operator *(poly a){
	poly temp;
	temp.degree = degree+a.degree; 
	for(int i=0;i<=degree; i++){
		for(int j=0; j<=a.degree; j++){
			int t=0;
			t=p[i]*a.p[j];
			temp.p[i+j]=temp.p[i+j]+t;
		}
	}
	return temp;
}



template <class T>
poly<T> poly<T>::operator +(poly a){
		poly temp;
		if(degree >a.degree){temp.degree=degree;}
		else{temp.degree=a.degree;};
		for(int i=0;i<=temp.degree; i++){
			temp.p[i]=p[i]+a.p[i];
		};
		return temp;
}
template <class T>
void get(int deg, T val){
	p[deg]=val;
}
template <class T>
T poly<T>::set(int deg){
	return p[deg];
}
template <class T>
void poly<T>::Out(){
	for(int i=0;i<=degree;i++)
		cout << p[i] << ' ';
	cout << endl;
}
int main(){
	poly<int> q(2);
	q.Out();
	cout << endl;
	poly<int> q1(3);
	q1.Out();
	cout << endl;
	poly<int> q2;
	q2=q+q1;
	cout << endl;
	q2.Out();

	_getch();
	return 0;
}
