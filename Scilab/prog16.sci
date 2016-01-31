function [r] = nummin(A, B)
    n = length(A)
    mmin = A(1)
    for i = 2:n
        if A(i) < mmin then
            mmin = A(i)
        end
    end
    j = 1
    for i = 1:n
        if A(i) == mmin then
            r(1,j) = i
            j = j + 1
        end
    end
    n = length(B)
    mmin = B(1)
    for i = 2:n
        if B(i) < mmin then
            mmin = B(i)
        end
    end
    j = 1
    for i = 1:n
        if B(i) == mmin then
            r(2,j) = i
            j = j + 1
        end
    end
endfunction
