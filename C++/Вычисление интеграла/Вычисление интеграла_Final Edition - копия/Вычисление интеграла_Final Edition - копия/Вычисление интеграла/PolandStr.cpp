#include "PolandStr.h"


PolandStr::PolandStr(String^s)
{
	this->s = s;
	good = IsGood();
}

Boolean PolandStr::IsGood(){
	
	Boolean norm=true;
	int umn;
	int del;
	int sloz;
	int minus;
	int oper;
	Boolean flag;//улавливает есть ли в конце оператора числа
	Boolean ishaveoper=false;//улавливает есть ли вообще операторы в выражении
	
	Generic::Stack<Char>^st = gcnew Generic::Stack<Char>();
	for (int i = 0; i < s->Length; i++){
		if (s[i] == '{' || s[i] == '}' || s[i] == '[' || s[i] == ']'
			|| s[i] == '<' || s[i] == '>'){
			this->err = "¬ выражении содержитс€ ошибка нужно ставить ( или ). ";
			norm = false;
			return norm;
		}
		if (s[i] == '!' || s[i] == '@' || s[i] == '#' || s[i] == '%' || s[i] == '"'
			|| s[i] == ':' || s[i] == '$' || s[i] == ';' || s[i] == '~'|| s[i]=='&'
			|| s[i]=='?' || s[i] =='|'||s[i]=='='){
			this->err = this->err + "¬ выражении содержатс€ не математические символы. ";
			norm = false;
			return norm;
		}
		if (s[0] == '*' || s[0] == '/'||(s[0]=='+'&&s[1]=='+')||(s[0]=='-'&&s[1]=='-')){
			this->err = this->err + "¬ыражение не может начинатьс€ с умножени€ или делени€ или с двух плюсов или с двух минусов. ";
			norm = false;
			return norm;
		}
		s[i] == '*' ? umn++ : umn = 0;
		s[i] == '/' ? del++ : del = 0;
		s[i] == '+' ? sloz++ : sloz = 0;
		s[i] == '-' ? minus++ : minus = 0;
		if (s[i] == '+' || s[i] == '-' || s[i] == '*' || s[i] == '/'||s[i]=='^'){
			oper++;
			ishaveoper=true;
		}
		else{
			oper = 0;
		}
		if (oper == 3 || umn == 2 || del == 2 || sloz == 3 || minus == 3){
			norm = false;
			this->err = this->err + "¬ыражение содержит слишком много операторов наход€щихс€ друг за другом. ";
			return norm;
		}
		if (i != s->Length - 1 && ((s[i] == '+'&&s[i + 1] == '*') || (s[i] == '+'&&s[i + 1] == '/') || (s[i] == '-'&&s[i + 1] == '*')
			|| (s[i] == '-'&&s[i + 1] == '/') || (s[i] == '*'&&s[i + 1] == '/') || (s[i] == '/'&&s[i + 1] == '*'))){
			norm = false;
			this->err = this->err + "ќшибка вида +* или -/. ";
			return norm;
		}
		if (IsOperator(s[i])){
			flag = false;
		}
		else{
			flag = true;
		}
		if (IsOpen(s[i])){
			st->Push(s[i]);
		}
		if (IsClose(s[i])){
			if (st->Count == 0){
				this->err = this->err + "¬ выражении неверно расставлены скобки. ";
				norm = false;
				return norm;
			}
			else{
				st->Pop();
				}
			}
		

	}
	if (flag == false){
		this->err = this->err + "ѕосле оператора нет значени€. ";
		return flag;
	}
	//if (ishaveoper==false){
	//	this->err = this->err + "¬ выражении нет операторов. ";
	//	return ishaveoper;
	//}
	if (st->Count != 0){
		this->err = "¬ выражении много открывающихс€ скобок ";
		return false;
	}
	return norm;
}

String^PolandStr::GetPolandStr(){
	if (good == false){
		MessageBox::Show(err);
		return "";
	}
	else{
		Int32 len = s->Length;
		Generic::Stack<Char>^st = gcnew Generic::Stack<Char>();
		String^result;
		Boolean flag = true;
		Boolean close = false;
		for (int i = 0; i < s->Length; i++){
			if (s[i] == ' '&&i!=s->Length-1){
				i++;
			}
			if (!IsOperator(s[i]) && !IsClose(s[i]) && !IsOpen(s[i])){//если это символ
				result = result + s[i];
				flag = false;
				close = false;
			}
			else{//если это оператор
				if ((s[i] == '-' || s[i] == '+') && flag == true && close == false){//если оператор минус
					result = result + s[i];
					flag = false;
				}
				else{//если оператор не минус
					if (st->Count == 0 || s[i] == '('){//если это первый элемент в стеке или это открывающа€с€ скобка
						close = false;
						st->Push(s[i]);
						if (flag == false){
							result = result + " ";
						}
						flag = true;
					}
					else{
						if (IsClose(s[i])){
							close = true;
							while (st->Peek() != '('){
								result = result + " ";
								result = result + st->Pop();
							}
							result = result + " ";
							flag = true;
							st->Pop();
						}
						else{
							close = false;
							if (Priority(s[i]) > Priority(st->Peek())){
								st->Push(s[i]);
								if (flag == false){
									result = result + " ";
								}
								flag = true;
							}
							else{
								if (flag == false){
									result = result + " ";
								}
								while (st->Count > 0 && (Priority(s[i]) <= Priority(st->Peek()))){
									result = result + st->Pop();
									result = result + " ";
								}
								flag = true;
								st->Push(s[i]);

							}
						}
					}

				}
			}
		}
		while (st->Count != 0){
			result = result + " ";
			result = result + st->Pop();
		}
		return result;
	}
}
