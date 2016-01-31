function [p] = prog6(A)
    n = 4;
    p = 1;
    for i = 1:n
        q = A(i, 1);
        for j = 2:n
            if q > A(i, j) then q = A(i,j);
            end
        end
        p = p * q;
    end
endfunction
