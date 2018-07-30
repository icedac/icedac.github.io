/****************************************************************************
 *  
 *      AppEchoServer.cpp
 *          ($\BB\AppEchoServer)
 * 
 *      by icedac@gmail.com
 *
 */
#include "stdafx.h"
#include "AppEchoServer.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif//_DEBUG

using namespace corn;
using namespace basic;

namespace internal {
    static class Perf : public corn::PerfHelper
    {
    public:
        Perf() :
            corn::PerfHelper( (corn::PerfDef*) &m_perfData )
        {
            PerfDef perfData[] =
            {
                { L"PERF_SERVER_SESSION_CONNECTED", 0 },
                { L"PERF_SERVER_SESSION_DISCONNECTED", 0 },
                { L"PERF_SERVER_SESSION_ROOM_ENTERED", 0 },
                { L"PERF_SERVER_SESSION_ROOM_LEAVED", 0 },
            };

            COMPILER_ASSERT( arrsize( perfData ) == arrsize( m_perfData ) );
            corn::MemCopy( &m_perfData, &perfData, sizeof(m_perfData) );
            bool regResult = corn::PerfRegisterPerfData( (corn::PerfDef*) &m_perfData, arrsize( m_perfData ) );
            ASSERT( regResult );
        }
    private:
        corn::PerfDef     m_perfData[NUM_PERF_SERVER];
    } s_Perf;
};
corn::PerfHelper& g_PerfServer = internal::s_Perf;

//==========================================================================
CAppEchoServer::CAppEchoServer( int argc, _TCHAR* argv[] )
    : AppType( argc, argv, UPDATE_LOOP_INTEVAL, "./bb.config.toml" )
    , SuperType()
    , m_counterIocpIoCompleted( g_PerfThread, PERF_THREAD_IOCP_IO_COMPLETED )
    , m_counterIocpIoTaskCalled( g_PerfThread, PERF_THREAD_IOCP_IO_TASK_CALLED )
    , m_counterIocpIoGqcsCalled( g_PerfThread, PERF_THREAD_IOCP_GQCS_CALLED )
    , m_counterSocketSendCompletd( g_PerfNet, PERF_NET_SOCKET_SEND_PACKET_COMPLETED_COUNT )
    , m_counterSocketRecvCompletd( g_PerfNet, PERF_NET_SOCKET_RECV_PACKET_COMPLETED_COUNT )
{ 
    const auto& config = AppType::GetConfig();

    double main_pool_multiplier = config.GetAs<double>("system.main_pool_multiplier", 1.5f);
    double worker_pool_multiplier = config.GetAs<double>("system.worker_pool_multiplier", 1.5f);

    unsigned coreCount = ThreadPoolGetCoreCount();

    m_threadPool = ThreadPoolCreate(
        nullptr,
        (unsigned)((double)coreCount*main_pool_multiplier),
        "MainThreadPool");

    unsigned idxServerThreadPool = 0;
    m_workerPool = ThreadPoolCreate(
        &idxServerThreadPool,
        (unsigned)((double)coreCount*worker_pool_multiplier),
        "ServerWorkerThreadPool");
}

//==========================================================================
CAppEchoServer::~CAppEchoServer()
{

}

//==========================================================================
void CAppEchoServer::Init()
{
    const auto& config = AppType::GetConfig();

    auto inteval_cleanup_session = config.GetAs<std::uint32_t>("system.inteval_cleanup_session", 60);
    auto inteval_pull_request_rank = config.GetAs<std::uint32_t>("system.inteval_pull_request_rank", 600);
    CSession::SetDefaultSocketTimeout(config.GetAs<std::uint32_t>("system.session_timeout_default", 120));
    CSession::SetLoggedinSocketTimeout(config.GetAs<std::uint32_t>("system.session_timeout_loggedin", 300));

    m_clientMessageHandler.Init( OnCLToES, 1 );

    // m_debugAllPacket = config.GetAs<bool>("debug_packet", false);

    auto listenPort = config.GetAs<std::uint32_t>("port", 1818 );
    auto threadCountForPool = config.GetAs<std::uint32_t>( "threadCountForPool", 1 );
    auto maxSessionCount = config.GetAs<std::uint32_t>( "max_session_count", 5000 );

    TServer::Init(
        listenPort,
        threadCountForPool,
        maxSessionCount,
        &m_clientMessageHandler,
        false /* enable nagling */
    );
}

//==========================================================================
void CAppEchoServer::Run( bool consoleMode /*= true */ )
{
    if ( consoleMode )
        TApplication::RunAsConsole();
    else
        TApplication::RunAsService<corn::Service>();
}

//==========================================================================
void CAppEchoServer::Destory()
{
    ASSERT( false );
}

//==========================================================================
bool CAppEchoServer::OnStart()
{
    // pass cleaning up session to workerpool
    m_workerPool->QueueTask([this](void*) {
        ScheduleCleanupSession();
    }, 0);

    return TServer::Start(); // start listen
}

//==========================================================================
void CAppEchoServer::OnStop()
{
    corn::AtomicSet(&m_shutdown, 1);

    // m_dbConnector.Destroy();

    TServer::Stop();
}

template < typename AppServer >
void ShowServerStatus( AppServer* _this )
{
    // show server performance counter, so no logging just print to console

    static qword lastTime = TimeGetMsFast();
    qword curTime = TimeGetMsFast();
    qword elapsedTime = curTime - lastTime;
    lastTime = curTime;
    if ( elapsedTime == 0 ) elapsedTime = 1;

    wprintf( L"-------- perf elapsed[%04d] --------\n", elapsedTime );

    unsigned        perfId = 0;
    const wchar_t*  perfName = nullptr;
    while ( nullptr != (perfName = PerfGetCounterName( perfId )) )
    {
        unsigned64 perfData = PerfGetCounterSummary( perfId++ );

        wprintf( L"%10I64d='%s'\n", perfData, perfName );
    }

    wprintf( L"%10d='%s'\n",
             _this->m_counterIocpIoCompleted.GetPerfCounterPerSec( elapsedTime ),
             L"**IO_COMPLETED_PER_SEC**" );
    wprintf( L"%10d='%s'\n",
             _this->m_counterIocpIoTaskCalled.GetPerfCounterPerSec( elapsedTime ),
             L"**IO_TASK_CALLED_PER_SEC**" );
    wprintf( L"%10d='%s'\n",
             _this->m_counterIocpIoGqcsCalled.GetPerfCounterPerSec( elapsedTime ),
             L"**IO_GQCS_CALLED_PER_SEC**" );
    wprintf( L"%10d='%s'\n",
             _this->m_counterSocketSendCompletd.GetPerfCounterPerSec( elapsedTime ),
             L"**SOCKET_SEND_COMPLETED_PER_SEC**" );
    wprintf( L"%10d='%s'\n",
             _this->m_counterSocketRecvCompletd.GetPerfCounterPerSec( elapsedTime ),
             L"**SOCKET_RECV_COMPLETED_PER_SEC**" );
}

//==========================================================================
void CAppEchoServer::OnUpdate( unsigned elapsedTime )
{
    TServer::OnUpdate( elapsedTime );
}

//==========================================================================
void CAppEchoServer::OnProcessCommand( int inputChar )
{
    // working only for console

    UNUSED_ARG( inputChar );
#ifdef USE_BASE_PROFILER
    if ( inputChar == 'l' )
    {
        ProfilerListRunningThreads();
    }
    else
#endif
    if ( inputChar == 's' )
    {
        ShowServerStatus( this );
    }
}

//==========================================================================
bool CAppEchoServer::OnDispatchMessage( MessageType_t type, void* client, unsigned bytes, corn::byte* data )
{
    ASSERT( client );
    return m_clientMessageHandler.Call( type, client, bytes, data );
    // or pass to worker thread
    // return m_mainMessageHandler->PostCall( m_workerPool, type, ct, bytes, data );
}

bool CAppEchoServer::OnSessionAccepted( SessionRefPtr session, const corn::SocketAcceptData& acceptData )
{
    UNUSED_ARG(session); UNUSED_ARG(acceptData);

    corn::TcpNetAddress addr = acceptData.address;
    
    // if addr.GetIpAddress() match blacklists 
    //  return false to refuse connection

    return true;
}

void CAppEchoServer::OnSessionDisconnected(SessionRefPtr session)
{
    UNUSED_ARG(session);
}


void CAppEchoServer::ScheduleCleanupSession()
{
    TLTimerEnqueue( 60 * 1000, [this]() {
        if (m_shutdown) return;


        ScheduleCleanupSession();
    });
}

