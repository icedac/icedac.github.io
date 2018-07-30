/****************************************************************************
 *  
 *      ClientSession.cpp
 *          ($\BB\AppEchoServer)
 * 
 *      by icedac@gmail.com
 *
 */
#include "stdafx.h"
#include "ClientSession.h"
#include "AppEchoServer.h"

using cl_es::Error;

//==========================================================================
// for memory leak detect!
#ifdef _DEBUG
#define new DEBUG_NEW
#endif//_DEBUG

void CClientSession::OnSessionConnected()
{
    g_PerfServer.IncCounter(PERF_SERVER_SESSION_CONNECTED);
    corn::TcpNetAddress address(GetNetAddress());

    CORN_DEBUG("<CONNECT> from %s:%d", address.GetIpAddress(), address.GetPort());
}

void CClientSession::OnSessionDisconnected()
{
    corn::TcpNetAddress address(GetNetAddress());
    CORN_DEBUG("<DISCONNECT> from %s:%d", address.GetIpAddress(), address.GetPort());

    g_PerfServer.IncCounter(PERF_SERVER_SESSION_DISCONNECTED);

}

// this callback function enable us to select socket engine(iocp, select ...),
// and choose which theead pool to use, configure send buffer
CSocketStream* CClientSession::AllocStream(SOCKET s, CThreadPool* pool)
{
    CIocpSocketStream* ss = new CIocpSocketStream();
    ss->IncRef();
    if (!ss->Start(s, pool, this, 8192 * 10/*maxSendPendingBytesThreshold*/, 8192))
    {
        ss->DecRef();
        return nullptr;
    }
    return ss;
}

void CClientSession::OnStreamDisconnect(void* object)
{
    SuperType::OnStreamDisconnect(object);
}

bool CClientSession::OnStreamRead (
    void*       object,
    unsigned    dataBytes,
    byte*       data,          // safe to modify the data in place
    unsigned*   bytesProcessed
    )
{
    UNUSED_ARG( object );
#if 1
    // the dataBytes should be bigger than default header
    if ( dataBytes < sizeof(Common::Header) )
        return true; // need to read more

    // parse header
    const Common::Header* h = reinterpret_cast<const Common::Header*>(data);

    // check body size
    if ( dataBytes < h->sizeWhole )
        return true; // need to read more

    *bytesProcessed = h->sizeWhole;

    return m_server->OnDispatchMessage( 
        h->typeBody,
        static_cast<ICLToESHandler*>(this), /* important! */
        *bytesProcessed, data );

#else // flash policy filer server
    if ( std::string( (const char*) data ) == "<policy-file-request/>" )
    {
        std::string sendmsg;
        sendmsg = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> ";
        sendmsg += "<cross-domain-policy> ";
        sendmsg += "<allow-access-from domain=\"*\" secure=\"false\" to-ports=\"*\"/> ";
        sendmsg += "</cross-domain-policy>\0";
            
        return QueueSend(corn::assert_if_data_loss_cast<unsigned int>(sendmsg.size() + 1), (const byte*)sendmsg.c_str());
    }

    return SuperType::OnStreamRead( 
        object,
        dataBytes,
        data,
        bytesProcessed );
#endif// #if 0
}

int CClientSession::On_CE_HelloReq( const Recv_CE_HelloReq *packet )
{
    // protocol deserializing from ProtocolParserGenerator gen codes

    basic::TPacketSendBuffer< Send_EC_HelloAck > buf;
    buf.pk.result = Error::ERR_SUCCESS;
    buf.pk.acct_id = packet->acct_id;

    // buf will be serializing from ProtocolParserGenerator gen codes
    QueueSend( &buf );
    //return RESULT_NOT_IMPLEMENTED;
    return RESULT_TRUE;
}
