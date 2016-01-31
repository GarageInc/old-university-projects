function [r] = neighmin(A)
    n = length(A)
    r = abs(A(2) - A(1))
    for i = 3:n
        if r > abs(A(i) - A(i - 1)) then
            r = abs(A(i) - A(i - 1))
        end
    end
endfunction
