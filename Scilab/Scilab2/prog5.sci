function [r, imax] = prog5(A)
    n = 13;
    i = 1;
    while (i <= n) & (A(i) > 0)
        i = i + 1;
    end
    if i <= n then r = A(i);
    else
        r = 0;
    end
    imax = modulo(i, n + 1);
    while (i < n)
        i = i + 1;
        if (A(i) < 0) & (A(i) > r) then 
            r = A(i);
            imax = i;
        end
    end
endfunction
