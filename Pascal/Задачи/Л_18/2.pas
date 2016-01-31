
uses crt;

const
n=20;
//Переменные для файла с целыми числами
var f:file of integer;
    i,j,buf,s:integer; //buf - для работы с файлом(чтение-запись элементов)
    a: array [1..n] of integer; // 2 массива
    b: array [1..n] of integer;
begin

assign(f,'file');
rewrite(f);

//Вводим целые числа. Окончание - ноль
writeln('Введите в файл целые числа, окончание ввода ноль:');
repeat
      readln(buf);
      write(f,buf);{создание A файла}
      s:=s+1;
until buf=0;

//Содержание нашего файла выводим на экран
writeln('Содержание исходного файла:');
reset(f);

i:=1;
j:=1;
while not eof(f) do   //Считываем с A файла,пока не дойдем до его конца
 begin
  read(f,buf);
  write(buf,' ');
  //И одновременно копируем в наши массивы (делим на 2 части!)
  if (i<(s div 2))
     then
         begin
          a[i]:=buf; i:=i+1;
         end
     else
         begin
          b[j]:=buf; j:=j+1;
         end
     
 end;

close(f);//Закрываем файл

//Печатаем оба полученных массива

  writeln('Массив №1(А):');
  //Выводим первый массив А
  for i:=1 to i-1 do
  begin
  write(a[i], ' ') ;
  end;

  writeln(' ');
  writeln('Массив №2(Б):');
  //Выводим первый массив Б
  for i:=1 to j-1 do
  begin
  write(b[i], ' ') ;
  end;

readln;
end.
