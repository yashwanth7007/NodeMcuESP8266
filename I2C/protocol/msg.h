#ifndef MSG_H
#define MSG_H


#include <stdint.h>


struct MsgHdr
{
    uint16_t  signature; // 0x1234
};

union MsgPayload
{
   
    union {
        union {
            union {
                struct {
                    uint16_t chipAddr;
                    uint16_t __reserved0;
                    uint16_t rdLen;
                    uint16_t wrLen;
                    uint8_t  wrBuff[4096];
                } req;

                struct {
                    uint16_t chipAddr;
                    uint16_t __reserved0;
                    uint16_t rdLen;
                    uint16_t __reserved1;
                    uint8_t  rdBuff[4096];
                } resp;
            } v1;
        } tran;
    } i2c;
};

struct Msg
{
    struct MsgHdr      hdr;
    union MsgPayload  payload;
};



#endif //MSG_H