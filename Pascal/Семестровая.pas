Const e:Real = 0.0000001;
Var n, i, j:Integer;
    x, y, y1:Real;
    
function f(x:Real):Real;
begin
     f := 3 + x * x;
end;

function l(x:Real):Real;
begin
     l := 2 * sin(1 + x);
end;

function l1(x:Real):Real;
begin
     l1 := 2 * cos(1 + x);
end;

function q(x:Real):Real;
begin
     q := sqrt(1 + x * x) / (1 + 0.4 * x * x);
end;

Begin
     Read(n);
     For i := 1 to n do
     begin
          Read(x);
          y := - q(x) / f(x);
          y1 := y - (y * f(x) + q(x) + l(y)) / (f(x) + l1(y));
          While(abs(y1 - y) >= e) do
          begin
               y := y1;
               y1 := y - (y * f(x) + q(x) + l(y)) / (f(x) + l1(y));
          end;
          Writeln(x, ' ', y, ' ', y * f(x) + q(x) + l(y));
     end
End.