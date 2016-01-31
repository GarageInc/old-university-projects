function [r] = prog1(d, O, y)
    i = 1;
    for a = (d + 2):0.5:(d + 4)
        r(i) = sin(a + O + y) * sin(a + O + y) + sin((O + y) / a);
        i = i + 1;
    end
endfunction
