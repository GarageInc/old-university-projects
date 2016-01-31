program V3;
uses crt;
const N = 2;
type
	Student = record
		familia: string; //Фамилия
		imya: string; //Имя
		otchestvo: string;//Отчество
		telefon: integer;  //Номер телефона
	end;
var
   //Массив студентов
	a: array [1..N] of Student;
	i: integer;

    //Вводим студентов: Ф+И+О+рост.
begin
	for i := 1 to N do
	begin
		writeln('Введите  Фамилию студента №', i);
		readln(a[i].familia);
		writeln('Введите  Имя студента', i);
		readln(a[i].imya);
		writeln('Введите  Отчество студента', i);
		readln(a[i].otchestvo);
		writeln('Введите  номер телефона студента(0 - если нет номера)', i);
		readln(a[i].telefon);
	end;
	
	//Выводим тех студентов без номера телефона
	for i := 1 to N do
	begin
	     if a[i].telefon=0
	     then Writeln(i,'. ',a[i].familia,' ',a[i].imya,' ',a[i].telefon);
	end;
	
end.
