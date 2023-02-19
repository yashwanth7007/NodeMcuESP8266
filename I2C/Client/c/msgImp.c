#include <msg.h>

int getMsgSize()
{
    return sizeof(struct Msg);
}

uint8_t* typeCastMsgToUint8(struct Msg* msg)
{
    return (uint8_t *) msg;
}