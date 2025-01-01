using System;
using static ULuaJIT.LowLevel.lauxlib;
using static ULuaJIT.LowLevel.lua;

namespace ULuaJIT.LowLevel
{
    public static class customlib
    {
        public static void luaL_registerlib(IntPtr L, string libname, string globalname, luaL_Reg[] l)
        {
            luaL_newlib(L, l);
    
            // Add to 'package.loaded'
            lua_getglobal(L, "package");
            lua_getfield(L, -1, "loaded");
            lua_pushvalue(L, -3);
            lua_setfield(L, -2, libname);
            lua_pop(L, 2); // Pop 'package' and 'package.loaded'
            
            // Set to global
            lua_setglobal(L, globalname);
        }
    }
}
