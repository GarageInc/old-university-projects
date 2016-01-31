#pragma once
using namespace System;
using namespace System::Collections;
using namespace System::Windows::Forms;
ref class PolandStr
{
private:
	String^s;
	Boolean good;
	String^err;
	Boolean IsOperator(Char a){
		if (a == '+' || a == '-' || a == '*' || a == '/' || a == '^'){
			return true;
		}
		else{
			return false;
		}
	}
	Boolean IsOpen(Char a){
		if (a == '('){
			return true;
		}
		else{
			return false;
		}
	}
	Boolean IsClose(Char a){
		if (a == ')'){
			return true;
		}
		else{
			return false;
		}
	}
	Int32 Priority(Char a){
		if (a == '+' || a == '-'){
			return 1;
		}
		if (a == '*' || a == '/'){
			return 2;
		}
		if (a == '^'){
			return 3;
		}
		return -1;
	}
	Boolean IsGood();
public:
	PolandStr(String^s);
	String^GetPolandStr();

};

