function [mmax] = prog8(A)
    mmax = 0;
    n = 5;
    for i = 2:n
        if abs(A(i - 1, i - 1) - A(i, i)) > mmax then
            mmax = abs(A(i - 1, i - 1) - A(i, i));
        end
    end
endfunction
