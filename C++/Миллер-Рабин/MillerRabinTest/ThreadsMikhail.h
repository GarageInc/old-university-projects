#include <thread>

class ThreadsMikhail
{

public:
	ThreadsMikhail();
	virtual ~ThreadsMikhail();

	typedef void(callback)();

	void parallel(int threads_count, callback f);
	void parallel_by_cores(callback f, bool max = false);
	void wait_for_end();

protected:

	int THREADS_COUNT;
	std::thread *THREADS;
	//atomic<bool> *COMPLETED_THREADS;

};
