#pragma once

#include <time.h>
#include <stdio.h>


class TimeManager
{
public:
	TimeManager();
	~TimeManager();

	typedef void(*callback)();
	void run(callback func);
};

