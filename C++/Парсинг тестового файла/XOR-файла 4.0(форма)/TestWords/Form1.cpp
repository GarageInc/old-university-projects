#include "Form1.h"

#include <Windows.h>
using namespace TestWords;

[STAThreadAttribute]
int WINAPI WinMain(HINSTANCE, HINSTANCE, LPSTR, int)
{
	// Включение визуальных эффектов Windows XP до создания каких-либо элементов управления
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);

	// Создание главного окна и его запуск
	Application::Run(gcnew Form1());
	return 0;
}
