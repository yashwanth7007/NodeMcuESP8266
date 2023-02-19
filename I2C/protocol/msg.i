%module msgMod
//https://www.swig.org/Doc1.3/Library.html#Library_carrays

%include "carrays.i"
%array_functions(uint8_t, uint8_t_arr);

%{
    #include "msg.h"
    #include <stdint.h>
%}

%include "msg.h"