function [b, c] = prog7(A)
    n = 5;
    j = 1;
    for i = 1:n
        if A(i) > 0 then
            b(j) = A(i);
            c(j) = i;
            j = j + 1;
        end;
    end
endfunction
