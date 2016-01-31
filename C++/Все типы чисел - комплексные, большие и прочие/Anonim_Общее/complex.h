

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
};
