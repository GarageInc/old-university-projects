function [imax] = prog3(A)
    n = 4;
    mmax = -1;
    imax = 0;
    for i = 1:n
        summ = 0;
        kol = 0;
        for j = 1:n
            if A(i, j) > 0 then
                summ = summ + A(i, j);
                kol = kol + 1;
            end
        end
        med = 0
        if kol <> 0 then med = summ / kol;
        end
        if med > mmax then
            mmax = med;
            imax = i;
        end
    end
endfunction
