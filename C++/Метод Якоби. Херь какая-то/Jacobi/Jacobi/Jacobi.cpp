// Jacobi.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <math.h>
#include <iostream>
using namespace std;
const double eps = 0.001;
 
void Jacobi (int N, double **A, double *F, double *X)
{
	double * TempX = new double[N];
	double norm; 
 
	do {
		for (int i = 0; i < N; i++) 
		{
			TempX[i] = F[i];
			for (int g = 0; g < N; g++)
			{
				if (i != g)
					TempX[i] -= A[i][g] * X[g];
			}
			TempX[i] /= A[i][i];
		}
                norm = fabs(X[0] - TempX[0]);
		for (int h = 0; h < N; h++) 
		{
			if (fabs(X[h] - TempX[h]) > norm)
				norm = fabs(X[h] - TempX[h]);
			       X[h] = TempX[h];
		}
	} 
	  while (norm > eps);
	  delete[] TempX;

	  for (int i=0; i<N; i++){
          for (int j=0; j<N; j++)
		  {
			cout<<A[i][j]<<" ";
		  }
		  cout<<endl;
	}
	  
}


int _tmain(int argc, _TCHAR* argv[])
{   
	int n;
	cout<<"vvedi n"<<endl;
	cin>>n;
	double **a=new double *[n];
	for (int i=0; i<n; i++)
		a[i]=new double[n];
	double *f=new double [n];
	double *x=new double [n];
	for (int i=0; i<n; i++){
		f[i]=rand()%(10-5+1)+5;
          for (int j=0; j<n; j++)
			  if(i==j)
                    a[i][j]=rand()%(100-70+1)*n+70*n;
			  else
				    a[i][j]=rand()%(15-10+1)+10;
	}
    Jacobi (n,a,f,x);
	for (int i=0;i<n;i++)
	{cout<<"x["<<i<<"]="<<x[i]<<endl;}
	system ("pause");
	return 0;
}

