program V3;
uses crt;
const N = 2;
type
	Student = record
		familia: string; //Фамилия
		imya: string; //Имя
		otchestvo: string;//Отчество
		rost: integer;
	end;
var
   //Массив студентов
	a: array [1..N] of Student;
	sum, i: integer;

    //Вводим студентов: Ф+И+О+рост.
begin
	for i := 1 to N do
	begin
		writeln('Введите  Фамилию студента', i);
		readln(a[i].familia);
		writeln('Введите  Имя студента', i);
		readln(a[i].imya);
		writeln('Введите  Отчество студента', i);
		readln(a[i].otchestvo);
		writeln('Введите  рост студента', i);
		readln(a[i].rost);
	end;
	sum := 0;
	
	//Суммируем(см условие задачи)
	for i := 1 to N do
	begin
		sum := sum + a[i].rost;
	end;
	
	//Выводим среднюю сумму(ростов студентов)
	Writeln('Средний рост студентов = ',sum / N);
end.
