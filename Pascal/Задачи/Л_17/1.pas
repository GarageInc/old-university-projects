program V3;
uses crt;
const N = 2;
type
	Car = record
		brand: string; //Марка машины
		power: integer; //Лошадиные силы
	end;
var
   //Массив машин
	a: array [1..N] of Car;
	sum, i: integer;

    //Вводим машины: название машины И лошадиные силы
begin
	for i := 1 to N do
	begin
		writeln('Введите  ', i, ' марку машины');
		readln(a[i].brand);
		writeln('Введите количество лошадиных сил ', i, '  машины');
		readln(a[i].power);
	end;
	sum := 0;
	
	//Суммируем(см условие задачи)
	for i := 1 to N do
	begin
		sum := sum + a[i].power;
	end;
	//Выводим среднюю сумму
	Writeln(sum / N);
end.
