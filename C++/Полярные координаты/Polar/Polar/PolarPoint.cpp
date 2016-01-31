#include "PolarPoint.h"
#include <math.h>

PolarPoint::PolarPoint(double radius, double angle) : radius(radius), angle(angle)
{
}

double PolarPoint::distance(PolarPoint^ other)
{
	return sqrt(radius*radius + other->radius*other->radius - 2 * radius*other->radius*cos((angle - other->angle) * PI / 180.0));
}

double PolarPoint::distanceToLine(double a, double b, double c)
{
	return abs(a*radius*cos(angle * PI / 180.0) + b*radius*sin(angle * PI / 180.0) + c) / sqrt(a*a + b*b);
}