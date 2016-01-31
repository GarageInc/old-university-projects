function [d] = prog4(A, B)
    n = 7;
    m = 8;
    dmax = A(1) + 1;
    for i = 1:n
        if (A(i) + i) > dmax then dmax = A(i) + i;
        end
    end;
    for i = 1:m
        if (B(i) + i) > dmax then dmax = B(i) + i;
        end
    end;
    j = 1;
    for i = 1:n
        if (A(i) + i) == dmax then
            d(j) = i;
            j = j + 1;
        end
    end;
    for i = 1:m
        if (B(i) + i) == dmax then
            d(j) = i;
            j = j + 1;
        end
    end;
endfunction
