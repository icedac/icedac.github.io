#pragma once
/****************************************************************************
 * 	
 *		CommonPacket.h
 *			($\BB\AppEchoServer)
 * 
 *		by icedac@gmail.com
 *
 */
#ifndef _VS__COMMONPACKET_H_
#define _VS__COMMONPACKET_H_

#include <cstdint>

#pragma pack(push)
#pragma pack(1)

namespace Common
{
    struct Vector
    {
        float   v[3];
    };
    struct Point
    {
        float   v[3];
    };

    //////////////////////////////////////////////////////////////////////////
    // struct Header
    //////////////////////////////////////////////////////////////////////////
    struct Header
    {
        uint16_t    sizeWhole;
        uint16_t    typeBody;
    };

    /***************************************************************************
    *
    *   enum EGeneralResult
    *
    ***/

    enum EGeneralResult
    {
        RESULT_OK = 0,
        RESULT_ERROR = 1,
    };

};//namespace Common

#pragma pack(pop)

#endif//#define _VS__COMMONPACKET_H_
