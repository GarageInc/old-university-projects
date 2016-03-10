#pragma once
#include <boost/math/special_functions/log1p.hpp>
#include <boost/math/special_functions/pow.hpp>
#include<boost\multiprecision\cpp_int.hpp>
#include<boost\any.hpp>

#include <mutex>
#include <thread>
#include <future>

#include"primegen.h"

class TimeManager
{
public:
	TimeManager();
	~TimeManager();

	typedef void(*callback)();
	void run_simples(callback func);
};

