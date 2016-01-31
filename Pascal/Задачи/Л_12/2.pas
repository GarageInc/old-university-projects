
uses crt;
//Переменные для файлов с целыми числами
var A2,B2:file of integer;
    a:integer; //a - переменная для сравнения(четное или нечетное всё же число)
    
begin

assign(A2,'A2');
rewrite(A2);
assign(B2,'B2');
rewrite(B2);

//Вводим целые числа. Окончание - ноль
writeln('Введите в файл целые числа, окончание ввода ноль:');
repeat
      readln(a);
      write(A2,a);{создание A файла}
until a=0;

writeln('Содержание исходного файла:');
reset(A2);

while not eof(A2) do   //Считываем с A файла,пока не дойдем до его конца
 begin
  read(A2,a);
  write(a,' ');
  if a mod 2=0 then write(B2,a){создание B файла}
 end;

writeln;
close(A2);//Закрываем A
reset(B2);

//Выводим на экран - что же записали мы во второй файл?
writeln('Содержание второго файла:');
reset(B2);

while not eof(B2) do
 begin
  read(B2,a);
  write(a,' ');
 end;
writeln;
close(B2);

readln;
end.
