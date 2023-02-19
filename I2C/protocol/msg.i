%module msgMod

%{
    #include "msg.h"
    #include <stdint.h>
%}

%include "stdint.i"
%include "carrays.i" //https://www.swig.org/Doc1.3/Library.html#Library_carrays
%array_functions(uint8_t, uint8_t_arr);



%include "msg.h"