/****************************************************************************
*
*       main.cpp
*           ($\BB\AppEchoServer)
*
*       by icedac@gmail.com
*
*/
#include "stdafx.h"
#include "AppEchoServer.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif//_DEBUG

using namespace corn;

int _tmain(int argc, _TCHAR* argv[])
{
    corn::MinidumpInstall(true);
    corn::CGenericLogHandler::Install();
    corn::CSubjectLogHandler::Install("debug_packet", nullptr);

    if ( argv[1] && (StrCmp( argv[1], L"/crash" ) == 0) )
    {
        CORN_ERROR( "/crash - test crash!" );
        int* p = nullptr;
        *p = 10;
    }

    PROFILE_FUNCTION_RELEASE();

    try {
        // it's singleton explicit creation
        CAppEchoServer& app = *(new CAppEchoServer(argc, argv));

        app.Init();

        app.Run();
    }
    catch (const corn::runtime_error_with_callstack& e) {
        CORN_FATAL("exception: '%s'\ncallstack:\n%s", e.what(), e.callstack());
    }

    return 0;
}

