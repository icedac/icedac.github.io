#pragma once
/****************************************************************************
 *  
 *      AppEchoServer.h
 *          ($\BB\AppEchoServer)
 * 
 *      by icedac@gmail.com
 *
 */
#ifndef _VS__APPECHOSERVER_H_
#define _VS__APPECHOSERVER_H_

#include <BasicNetModel/BasicNetModel.h>
#include "CommonPacket.h"
#include "ClientSession.h"

class CClientSession;
using corn::CPerfSummaryCounter;
using corn::TSingleton;
using corn::byte;
using basic::MessageType_t;

enum
{
    PERF_SERVER_SESSION_CONNECTED = 0,
    PERF_SERVER_SESSION_DISCONNECTED,
    PERF_SERVER_SESSION_ROOM_ENTERED,
    PERF_SERVER_SESSION_ROOM_LEAVED,
    NUM_PERF_SERVER,
};
extern corn::PerfHelper& g_PerfServer;

/****************************************************************************
 *  
 *  
 */
typedef basic::TServer< 
    CClientSession, 
    basic::TSessionManager< CClientSession >,
    basic::TMessageHandlerForPacketCompiler< CClientSession >
    > BaseServer;

/****************************************************************************
 *  
 *  template server
 *
 */
class CAppEchoServer :
    public basic::TApplication<CAppEchoServer>,     // features: console/service, console input, config file loading, start, stop, update flow
    public BaseServer,                              // features: accept sessions, manage sessions, boardcast
    public TSingleton< CAppEchoServer, SINGLETON_HAS_NOT_DEFAULT_CONSTRUCTOR >
{
public:
    typedef TApplication<CAppEchoServer>        AppType;
    typedef BaseServer                          SuperType;
    typedef SuperType::SessionRefPtr            SessionRefPtr;
    typedef SuperType::CMessageHandler          ClientMessageHandler;

    CAppEchoServer( int argc, _TCHAR* argv[] );
    ~CAppEchoServer();

    void Init();

    void Destory();

    void Run(bool consoleMode = true );

protected:
    /***************************************************************************
    *
    *   implementation: TApplication<>
    *
    ***/
    bool OnStart() EXPLICIT_OVERRIDE;

    void OnStop() EXPLICIT_OVERRIDE;

    void OnUpdate( unsigned elapsedTime ) EXPLICIT_OVERRIDE;

    void OnProcessCommand( int inputChar ) EXPLICIT_OVERRIDE;

    /***************************************************************************
    *
    *   implementation: TServer::OnDispatchMessage
    *
    ***/
    bool OnDispatchMessage(
        MessageType_t type,
        void* client,
        unsigned bytes,
        corn::byte* data ) EXPLICIT_OVERRIDE;

    bool OnSessionAccepted(SessionRefPtr session, const corn::SocketAcceptData& acceptData) EXPLICIT_OVERRIDE;

    void OnSessionDisconnected(SessionRefPtr session) EXPLICIT_OVERRIDE;

    FORCEINLINE CThreadPool* OnSessionGetWorkerThreadPool()
    {
        return m_workerPool;
    }

    void ScheduleCleanupSession();

protected:
    template < typename AppServer >
    friend void ShowServerStatus( AppServer* );

    CPerfSummaryCounter         m_counterIocpIoCompleted;
    CPerfSummaryCounter         m_counterSocketSendCompletd;
    CPerfSummaryCounter         m_counterSocketRecvCompletd;
    CPerfSummaryCounter         m_counterIocpIoTaskCalled;
    CPerfSummaryCounter         m_counterIocpIoGqcsCalled;

    ClientMessageHandler        m_clientMessageHandler;
    CThreadPool*                m_workerPool = nullptr;

    volatile long               m_shutdown = 0;

    std::uint32_t               m_serverId = 0;
};


#endif // _VS__APPECHOSERVER_H_

