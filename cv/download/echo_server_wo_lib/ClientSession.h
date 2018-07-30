#pragma once
/****************************************************************************
 *  
 *      ClientSession.h
 *          ($\BB\AppEchoServer)
 * 
 *      by icedac@gmail.com
 *
 */
#ifndef _VS__CLIENTSESSION_H_
#define _VS__CLIENTSESSION_H_

#include <BasicNetModel/BasicNetModel.h>
#include "CommonPacket.h"
using Point = Common::Point;
using Vector = Common::Vector;
#include "../Packet/ES_CL_Interface.h"
#include "../Packet/ES_CL_Process.h"

using corn::TRefPtr;
using corn::CThreadPool;
using corn::CSocketStream;
using basic::CSession;
using basic::CIocpSocketStream;

#if defined( _DEBUG ) && defined( DEBUG_MEMORY )
#define new DEBUG_NEW
#endif//_DEBUG && DEBUG_MEMORY

using namespace cl_es;

/****************************************************************************
 *  
 *  
 */
class CClientSession : 
    public basic::CSession, // feature: send/read, manage socket
    public ICLToESHandler   // generated protocol(msg) dispatcher interface
{
public:
    typedef TRefPtr< CClientSession >   RefPtr;
    typedef basic::CSession             SuperType;

    CSocketStream* AllocStream(SOCKET s, CThreadPool* pool) EXPLICIT_OVERRIDE;

    void OnStreamDisconnect(void* object) EXPLICIT_OVERRIDE;

    bool OnStreamRead(void* object, unsigned dataBytes, byte* data, unsigned* bytesProcessed) EXPLICIT_OVERRIDE;

    /* ClientNotify */
    void OnSessionConnected() EXPLICIT_OVERRIDE;

    void OnSessionDisconnected() EXPLICIT_OVERRIDE;

    /****************************************************************************
     *  
     *  
     */
    int On_CE_HelloReq( const Recv_CE_HelloReq *packet ) EXPLICIT_OVERRIDE;


public:
    const char* GetAcctName() const { return m_acctName.empty() ? nullptr : m_acctName.c_str(); }

protected:
    template < typename SessionType >
    friend class basic::TSessionManager;
    template < typename T, typename IdType = unsigned /* or u can use unsigned64 */ >
    friend class basic::TMwmrFixedQueue;

    std::string     m_acctName;

private:
    std::string     m_acctId;
};

/****************************************************************************
*
*   NPC Session
*
*/
class CNPCSession : public CClientSession
{
public:
    CNPCSession(int npc_ai_level)
    {
        const char* npc_names[] = {
            "Luna", "Jervis", "Chris", "TJ", "Aiden", "Luke", "Irene", "Jin",
            "Joy", "Denis", "Kijeo", "Ian", "Coho", "Journey", 0
        };
        static int s_cur_name = 0;
        const char* n = npc_names[s_cur_name++];
        if (npc_names[s_cur_name] == 0) s_cur_name = 0;

        m_acctName = corn::FormatStringA("[%d]%s", npc_ai_level, n).AsString();
        SetDestroyer_(DeleteThis);
    }

    static void DeleteThis(CSession* s)
    {
        CNPCSession* npcs = static_cast<CNPCSession*>(s);
        delete npcs;
    }

    bool QueueSend(unsigned /*bytes*/, const corn::byte* /*data*/) EXPLICIT_OVERRIDE
    {
        return false;
    }
};

#if defined( _DEBUG ) && defined( DEBUG_MEMORY )
#undef new
#endif//_DEBUG && DEBUG_MEMORY

#endif // _VS__CLIENTSESSION_H_

