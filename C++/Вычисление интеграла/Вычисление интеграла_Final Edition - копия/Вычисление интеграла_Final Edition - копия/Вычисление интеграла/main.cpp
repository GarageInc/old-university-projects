#include "MyForm.h"
using namespace  Вычисление_математического_выражения;

int main()
{
    using namespace System;
    using namespace System::Windows::Forms;
    
    Application::EnableVisualStyles();
    Application::SetCompatibleTextRenderingDefault(false);
    Application::Run(gcnew MyForm());
}