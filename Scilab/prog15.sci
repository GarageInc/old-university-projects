function [r] = multmaxmin(A)
    n = length(A(1,:))
    amin = A(1,1)
    amax = A(1,1)
    for i = 1:n
        for j = 1:n
            if amin > A(i,j) then
                amin = A(i,j)
            end
            if amax < A(i,j) then
                amax = A(i,j)
            end
        end
    end
    r = amax * amin
endfunction
