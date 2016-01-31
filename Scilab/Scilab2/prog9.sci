function [s] = prog9(A, B)
    sum1 = 0;
    sum2 = 0;
    n = 7;
    m = 8;
    for i = 1:n
        if (A(i) >= -4) & (A(i) <= 5) then sum1 = sum1 + A(i);
        end
    end
    for i = 1:m
        if (B(i) >= -4) & (B(i) <= 5) then sum2 = sum2 + B(i);
        end
    end
    if sum1 > sum2 then
        s = 'Первый массив';
    else
        s = 'Второй массив';
    end
endfunction
