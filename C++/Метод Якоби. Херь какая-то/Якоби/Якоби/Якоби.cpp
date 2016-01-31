// якоби.cpp: определ€ет точку входа дл€ консольного приложени€.
//

#include "stdafx.h"
#include "math.h"
#include "conio.h"
#include "stdio.h"
#include<fstream>
#include<iostream>

using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
const int N=2;
double A[9][9], f[3], y[3];
int i,j;
double norma,E;
double xn[9], x[9];
	printf("E=");
	scanf("%lf", &E);

for (i=0;   i<=N;i++)
   {   for (j=0;j<=N;j++)
       { cout<<"vvedite element A[i][j]="<<endl;}
   }
  
  for (i=0;i<=N;i++)
{     for (j=0;j<=N;j++)
        scanf("%lf", &A[i][j]);
       
}
     for (i=0;   i<N;i++)
{        for (j=0;j<=N;j++);
    cout<<"vvedite element y[j]="<<endl;
}
  
  for (i=0;i<N;i++)
{    for (j=0;j<=N;j++)
    { scanf("%lf", &y[i]);};
       
}
  for (i=0;   i<=N;i++)
{  for (j=0;j<N;j++)
   {   printf("vvedite element f[i]=");}
}
  
  for (i=0;i<=N;i++)
{ for (j=0;j<N;j++)
    { scanf("%lf", &f[i]);}
       
}
 
do {
     norma=0;
       for(i=0;i<N; i++)
{
   	xn[i]=-f[i];

   	for(j=0;j<N;j++)
{

{
    			if(i!=j)
     			xn[i]+=A[i][j]*y[j];
}
	
		xn[i]/=-A[i][i];
}
  for(i=0;i<N;i++){
		if(fabs(x[i]-xn[i]) > norma)
		norma=fabs(x[i]-xn[i]); 
		x[i]=xn[i];
}
}
} while(norma>E); 

cout<<"x[%i]=%5.2f\n", i+1, x[i]);
return 0;

}   system ("pause"); 
	return 0;
}

