program Proga1;

{$APPTYPE CONSOLE}

uses
  SysUtils;

const n = 4;
const m = 3;
var a:array [1..n] of array [1..m] of Real;
    i, j:Integer;
    v:array [1..n] of Real;
    c:string;

function f(a:array of Real):Real ;
var
  i:Integer;
  p:Real;
begin
   p := 1;
   for i := 0 to m - 1 do
      p := p * Cos(a[i] * a[i] - 3.5);
   f := p;
end;

function g(a:array of Real):Real ;
var
  i:Integer;
  p:Real;
begin
   p := 1;
   for i := 0 to m - 1 do
      Writeln((a[i] + 1.9));
   for i := 0 to m - 1 do
      p := p * Sin(a[i] + 1.9);
   g := p;
end;

function p(a:array of Real):Boolean ;
var
  i:Integer;
  b:Boolean;
begin
   b := True;
   for i := 0 to m - 1 do
      if a[i] < 0 then
        b := False;
   p := b;
end;

begin
   Writeln('Enter matrix:');
   for i := 1 to n do
      for j := 1 to m do
         read(a[i, j]);
   for i := 1 to n do
      if P(a[i]) then
         v[i] := f(a[i])
      else
         v[i] := g(a[i]);
   Readln(c);
   for i := 1 to n do
     writeln(v[i]);
   Readln(c);
end.
