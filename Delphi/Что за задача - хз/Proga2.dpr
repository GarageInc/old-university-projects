program Proga2;

{$APPTYPE CONSOLE}

uses
  SysUtils;

type
  Exam = record
    subject:string;
    size:Integer;
    name:array of string;
    mark:array of Integer;
  end;

procedure read(var e:Exam);
var
  i:Integer;
  f:TextFile;
begin
    AssignFile(f, 'input2.txt');
    reset(f);
    Readln(f, e.subject);
    Readln(f, e.size);
    SetLength(e.name, e.size);
    SetLength(e.mark, e.size);
    for i := 0 to e.size - 1 do
       Readln(f, e.name[i]);
    for i := 0 to e.size - 1 do
       Readln(f, e.mark[i]);
end;

procedure write(e:Exam);
var
  i:Integer;
begin
    Writeln('Name of subject: ', e.subject);
    Writeln('Number of students: ', e.size);
    for i := 0 to e.size - 1 do
    begin
       Writeln('Name of ', i + 1, ' student: ', e.name[i]);
       Writeln('Mark of ', i + 1, ' student: ', e.mark[i]);
    end;
end;

procedure threeofbest(e:Exam; var res:array of Integer);
var i:Integer;
    max:array [0..2] of Integer;
begin
    res[0] := -1;
    res[1] := -1;
    res[2] := -1;
    max[0] := 0;
    max[1] := 0;
    max[2] := 0;
    for i := 0 to e.size - 1 do
    begin
       if e.mark[i] > max[0] then
       begin
          max[2] := max[1];
          max[1] := max[0];
          max[0] := e.mark[i];
          res[2] := res[1];
          res[1] := res[0];
          res[0] := i + 1;
       end
       else
       begin
          if e.mark[i] > max[1] then
          begin
             max[2] := max[1];
             max[1] := e.mark[i];
             res[2] := res[1];
             res[1] := i + 1;
          end
          else
          begin
             if e.mark[i] > max[2] then
             begin
                max[2] := e.mark[i];
                res[2] := i + 1
                ;
             end;
          end;
       end;
    end;
end;

var e:Exam;
    result:array [1..3] of Integer;
    i:Integer;
    c:string;

begin
   read(e);
   threeofbest(e, result);
   write(e);
   Writeln('The best students are:');
   for i := 1 to 3 do
      writeln(result[i]);
   Readln(c);
end.

