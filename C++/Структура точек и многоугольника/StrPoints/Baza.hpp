int const N=2;

struct Point
{
	int x,y;
	Point(){x=0; y=0;}
	Point(int a,int b){x=a;y=b;}
	void input();
	void print();
	void dlina();
};

void Point::input()
{
	cout<<"Vvod koordinat: ";
	cin>>x>>y;
}

void Point::print()
{
	cout<<"Vivod koordinat 1oy tochki"<<"X= "<<x<<" & "<<"Y="<<y;
}

void Point::dlina();
{
	int c, d, l;
	for (int i=1; i<N; i++)
	{
		c=x;d=y;
		point();
		print();
		l=sqrt((x-c)(x-c)+(y-d)(y-d));
		cout<<"Dlina "<<i<< " = "<<l;
	}
}