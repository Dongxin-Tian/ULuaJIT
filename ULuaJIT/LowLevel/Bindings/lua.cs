using System;
using System.Runtime.InteropServices;
using System.Security;
using static ULuaJIT.LowLevel.lauxlib;

namespace ULuaJIT.LowLevel
{
    /// <summary>
    /// lua.h
    /// </summary>
    public static class lua
    {
        public const string LibraryName = "lua51";

        public const string LUA_VERSION = "Lua 5.1";
        public const string LUA_RELEASE = "Lua 5.1.4";
        public const int LUA_VERSION_NUM = 501;
        public const string LUA_COPYRIGHT = "Copyright (C) 1994-2008 Lua.org, PUC-Rio";
        public const string LUA_AUTHORS = "R. Ierusalimschy, L. H. de Figueiredo & W. Celes";

        
        public static string LUA_QL(string s) 
            => $"'{s}'";
        

        /* mark for precompiled code (`<esc>Lua') */
        public const string LUA_SIGNATURE = "\033Lua";

        /* option for multiple returns in `lua_pcall' and `lua_call' */
        public const int LUA_MULTRET = -1;


        /*
         ** pseudo-indices
         */
        public const int LUA_REGISTRYINDEX = -10000;
        public const int LUA_ENVIRONINDEX = -10001;
        public const int LUA_GLOBALSINDEX = -10002;

        public static int lua_upvalueindex(int i)
            => LUA_GLOBALSINDEX - i;


        /* thread status */
        public const int LUA_OK = 0;
        public const int LUA_YIELD = 1;
        public const int LUA_ERRRUN = 2;
        public const int LUA_ERRSYNTAX = 3;
        public const int LUA_ERRMEM = 4;
        public const int LUA_ERRERR = 5;
        public const int LUA_ERRFILE = LUA_ERRERR + 1;


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int lua_CFunction(IntPtr L);


        /*
         ** functions that read/write blocks when loading/dumping Lua chunks
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr lua_Reader(IntPtr L, IntPtr ud, ref nuint sz);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int lua_Writer(IntPtr L, IntPtr p, nuint sz, IntPtr ud);


        /*
         ** prototype for memory-allocation functions
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr lua_Alloc(IntPtr ud, IntPtr ptr, nuint osize, nuint nsize);


        /*
         ** basic types
         */
        public const int LUA_TNONE = -1;

        public const int LUA_TNIL = 0;
        public const int LUA_TBOOLEAN = 1;
        public const int LUA_TLIGHTUSERDATA = 2;
        public const int LUA_TNUMBER = 3;
        public const int LUA_TSTRING = 4;
        public const int LUA_TTABLE = 5;
        public const int LUA_TFUNCTION = 6;
        public const int LUA_TUSERDATA = 7;
        public const int LUA_TTHREAD = 8;



        /* minimum Lua stack available to a C function */
        public const int LUA_MINSTACK = 20;



        /*
         ** state manipulation
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_newstate(lua_Alloc f, IntPtr ud);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_close(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_newthread(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_atpanic(IntPtr L, IntPtr panicf);


        /*
         ** basic stack manipulation
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gettop(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_settop(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushvalue(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_remove(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_insert(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_replace(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_checkstack(IntPtr L, int sz);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_xmove(IntPtr from, IntPtr to, int n);


        /*
         ** access functions (stack -> C)
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isnumber(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isstring(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_iscfunction(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isuserdata(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_type(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_typename(IntPtr L, int tp);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_equal(IntPtr L, int idx1, int idx2);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_rawequal(IntPtr L, int idx1, int idx2);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_lessthan(IntPtr L, int idx1, int idx2);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern double lua_tonumber(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern long lua_tointeger(IntPtr L, int idx);
  
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_toboolean(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_tolstring(IntPtr L, int idx, ref nuint len);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "lua_tolstring"), SuppressUnmanagedCodeSecurity]
#endif
        public unsafe static extern IntPtr lua_tolstring_unmanaged(IntPtr L, int idx, nuint* len);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern nuint lua_objlen(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_tocfunction(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_touserdata(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_tothread(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_topointer(IntPtr L, int idx);


        /*
         ** push functions (C -> stack)
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushnil(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushnumber(IntPtr L, double n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushinteger(IntPtr L, long n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushlstring(IntPtr L, IntPtr s, nuint l);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushstring(IntPtr L, string s);

        // public static extern const char *(lua_pushvfstring) (IntPtr L, const char *fmt, va_list argp);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_pushfstring(IntPtr L, string fmt);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushcclosure(IntPtr L, IntPtr fn, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushboolean(IntPtr L, int b);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_pushlightuserdata(IntPtr L, IntPtr p);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_pushthread(IntPtr L);


        /*
         ** get functions (Lua -> stack)
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_gettable(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_getfield(IntPtr L, int idx, string k);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, EntryPoint = "lua_getfield", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_getfield_unmanaged(IntPtr L, int idx, IntPtr k);
    
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawget(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawgeti(IntPtr L, int idx, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_createtable(IntPtr L, int narr, int nrec);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_newuserdata(IntPtr L, nuint sz);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getmetatable(IntPtr L, int objindex);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_getfenv(IntPtr L, int idx);


        /*
         ** set functions (stack -> Lua)
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_settable(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setfield(IntPtr L, int idx, string k);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawset(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_rawseti(IntPtr L, int idx, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_setmetatable(IntPtr L, int objindex);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_setfenv(IntPtr L, int idx);


        /*
         ** `load' and `call' functions (load and run Lua code)
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_call(IntPtr L, int nargs, int nresults);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_pcall(IntPtr L, int nargs, int nresults, int errfunc);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_cpcall(IntPtr L, IntPtr func, IntPtr ud);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_load(IntPtr L, lua_Reader reader, IntPtr dt, string chunkname);


#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_dump(IntPtr L, lua_Writer writer, IntPtr data);


        /*
         ** coroutine functions
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_yield(IntPtr L, int nresults);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_resume(IntPtr L, int narg);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_status(IntPtr L);


        /*
         ** garbage-collection function and options
         */
        public const int LUA_GCSTOP       = 0;
        public const int LUA_GCRESTART    = 1;
        public const int LUA_GCCOLLECT    = 2;
        public const int LUA_GCCOUNT      = 3;
        public const int LUA_GCCOUNTB     = 4;
        public const int LUA_GCSTEP       = 5;
        public const int LUA_GCSETPAUSE   = 6;
        public const int LUA_GCSETSTEPMUL = 7;
        public const int LUA_GCISRUNNING  = 9;

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gc(IntPtr L, int what, int data);


        /*
         ** miscellaneous functions
         */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_error(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_next(IntPtr L, int idx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_concat(IntPtr L, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Alloc lua_getallocf(IntPtr L, ref IntPtr ud);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setallocf(IntPtr L, lua_Alloc f, IntPtr ud);



        /*
         ** ===============================================================
         ** some useful macros
         ** ===============================================================
         */

        public static void lua_pop(IntPtr L, int n)
            => lua_settop(L, -(n) - 1);

        public static void lua_newtable(IntPtr L)
            => lua_createtable(L, 0, 0);

        public static void lua_register(IntPtr L, string n, IntPtr f)
        {
            lua_pushcfunction(L, (f));
            lua_setglobal(L, (n));
        }

        public static void lua_pushcfunction(IntPtr L, IntPtr f)
            => lua_pushcclosure(L, (f), 0);

        public static nuint lua_strlen(IntPtr L, int i)
            => lua_objlen(L, (i));

        public static int lua_isfunction(IntPtr L, int n)
            => lua_type(L, (n)) == LUA_TFUNCTION ? 1 : 0;
        
        public static int lua_istable(IntPtr L, int n)
            => lua_type(L, (n)) == LUA_TTABLE ? 1 : 0;

        public static int lua_islightuserdata(IntPtr L, int n)
            => lua_type(L, (n)) == LUA_TLIGHTUSERDATA ? 1 : 0;
        
        public static int lua_isnil(IntPtr L, int n)
            => lua_type(L, (n)) == LUA_TNIL ? 1 : 0;

        public static int lua_isboolean(IntPtr L, int n)
            => lua_type(L, (n)) == LUA_TBOOLEAN ? 1 : 0;
        
        public static int lua_isthread(IntPtr L, int n)
            => lua_type(L, (n)) == LUA_TTHREAD ? 1 : 0;
        
        public static int lua_isnone(IntPtr L, int n)
            => (lua_type(L, (n)) == LUA_TNONE) ? 1 : 0;
        
        public static int lua_isnoneornil(IntPtr L, int n)
            => lua_type(L, (n)) <= 0 ? 1 : 0;
        
        // lua_pushliteral(L, s)

        public static void lua_setglobal(IntPtr L, string s)
            => lua_setfield(L, LUA_GLOBALSINDEX, (s));

        public static void lua_getglobal(IntPtr L, string s)
            => lua_getfield(L, LUA_GLOBALSINDEX, (s));

        public unsafe static IntPtr lua_tostring(IntPtr L, int i)
            => lua_tolstring_unmanaged(L, (i), null);
        


        /*
         ** compatibility macros and functions
         */
        public static IntPtr lua_open()
            => luaL_newstate();
        
        public static void lua_getregistry(IntPtr L)
            => lua_pushvalue(L, LUA_REGISTRYINDEX);
        
        public static int lua_getgccount(IntPtr L)
            => lua_gc(L, LUA_GCCOUNT, 0);
        
        
        /* hack */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_setlevel(IntPtr from, IntPtr to);


        /*
         ** =======================================================================
         ** Debug API
         ** =======================================================================
         */
        

        /*
         ** Event codes
         */
        public const int LUA_HOOKCALL    = 0;
        public const int LUA_HOOKRET     = 1;
        public const int LUA_HOOKLINE    = 2;
        public const int LUA_HOOKCOUNT   = 3;
        public const int LUA_HOOKTAILRET = 4;


        /*
         ** Event masks
         */
        public const int LUA_MASKCALL  = 1 << LUA_HOOKCALL;
        public const int LUA_MASKRET   = 1 << LUA_HOOKRET;
        public const int LUA_MASKLINE  = 1 << LUA_HOOKLINE;
        public const int LUA_MASKCOUNT = 1 << LUA_HOOKCOUNT;
        
        
        /* Functions to be called by the debuger in specific events */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void lua_Hook(IntPtr L, IntPtr ar);


#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getstack (IntPtr L, int level, lua_Debug ar);
    
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getinfo(IntPtr L, string what, lua_Debug ar);
    
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_getlocal(IntPtr L, lua_Debug ar, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_setlocal(IntPtr L, lua_Debug ar, int n);
    
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_getupvalue(IntPtr L, int funcindex, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_setupvalue(IntPtr L, int funcindex, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_sethook(IntPtr L, lua_Hook func, int mask, int count);
    
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern lua_Hook lua_gethook(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gethookmask(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_gethookcount(IntPtr L);


        /* From Lua 5.2. */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_upvalueid(IntPtr L, int idx, int n);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_upvaluejoin(IntPtr L, int idx1, int n1, int idx2, int n2);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_loadx(IntPtr L, lua_Reader reader, IntPtr dt, string chunkname, string mode);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr lua_version(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void lua_copy(IntPtr L, int fromidx, int toidx);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern double lua_tonumberx(IntPtr L, int idx, ref int isnum);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern long lua_tointegerx(IntPtr L, int idx, ref int isnum);
        
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct lua_Debug
        {
            public int @event;
            public IntPtr name;
            public IntPtr namewhat;
            public IntPtr what;
            public IntPtr source;
            public int currentline;
            public int nups;
            public int linedefined;
            public int lastlinedefined;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
            public string short_src;

            public int i_ci;
        }
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getinfo(IntPtr L, string what, ref lua_Debug ar);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_getstack(IntPtr L, int level, ref lua_Debug ar);


/* From Lua 5.3. */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int lua_isyieldable(IntPtr L);
    }
}
