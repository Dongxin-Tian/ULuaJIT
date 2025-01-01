using static ULuaJIT.LowLevel.lua;

namespace ULuaJIT.LowLevel
{
    public enum LuaStatus : int
    {
        Ok                 = LUA_OK,
        Yield              = LUA_YIELD,
        ErrorRun           = LUA_ERRRUN,
        ErrorSyntax        = LUA_ERRSYNTAX,
        ErrorMemory        = LUA_ERRMEM,
        ErrorErrorHandler  = LUA_ERRERR,
        ErrorFile          = LUA_ERRFILE
    }
}
