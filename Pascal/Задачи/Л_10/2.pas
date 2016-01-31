uses crt;
//Переменные - стороны прямоугольника и просто промежуточные величины для расчетов
var
Ax,Bx,Ay,By: integer; //s - длина,которую будем считать , x и y - координаты сторон
s: real;
begin
  writeln('Введите координаты точки A');
  writeln('Ax=');
  readln(Ax);
  writeln('Ay=');
  readln(Ay);
  
  writeln('Введите координаты точки B');
  writeln('Bх=');
  readln(Bx);
  writeln('By=');
  readln(By);

  //Считаем длину S: power(1,2) - возводит число 1 в степень 2
  s:=sqrt(power((Ax-Bx),2)+power((Ay-By),2)); //sqrt - берем корень от числа


  //Выводим
  writeln('Длина = ',s);

readln;

end.
