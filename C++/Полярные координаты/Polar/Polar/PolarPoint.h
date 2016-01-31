#pragma once
ref class PolarPoint
{
	double radius;
	double angle; //in degrees (0-360)
public:
	PolarPoint(double radius, double angle);
	double distance(PolarPoint^ other);
	double distanceToLine(double a, double b, double c); //ax+by+c=0
	static const double PI = 3.14159265358;
};

