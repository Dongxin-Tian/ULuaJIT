using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using static ULuaJIT.LowLevel.lua;

namespace ULuaJIT.LowLevel
{
    /// <summary>
    /// lauxlib.h
    /// </summary>
    public static class lauxlib
    {
        /* extra error code for `luaL_load' */
        public const int LUA_ERRFILE = (LUA_ERRERR+1);

        [StructLayout(LayoutKind.Sequential)]
        public struct luaL_Reg
        {
            public string name;
            public IntPtr func;

            public luaL_Reg(string name, IntPtr func)
            {
                this.name = name;
                this.func = func;
            }

            public readonly static luaL_Reg Sentinel = new luaL_Reg(null, IntPtr.Zero);
        }

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_openlib(IntPtr L, string libname, luaL_Reg[] l, int nup);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_register(IntPtr L, string libname, luaL_Reg[] l);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_getmetafield(IntPtr L, int obj, string e);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_callmeta(IntPtr L, int obj, string e);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_typerror(IntPtr L, int narg, string tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_argerror(IntPtr L, int numarg, string extramsg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_checklstring(IntPtr L, int numArg, ref nuint l);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "luaL_checklstring"), SuppressUnmanagedCodeSecurity]
#endif
        public unsafe static extern IntPtr luaL_checklstring_unmanaged(IntPtr L, int numArg, nuint* l);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_optlstring(IntPtr L, int numArg, IntPtr def, ref nuint l);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "luaL_optlstring"), SuppressUnmanagedCodeSecurity]
#endif
        public unsafe static extern IntPtr luaL_optlstring_unmanaged(IntPtr L, int numArg, IntPtr def, nuint* l);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern double luaL_checknumber(IntPtr L, int numArg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern double luaL_optnumber(IntPtr L, int nArg, double def);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern long luaL_checkinteger(IntPtr L, int numArg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern long luaL_optinteger(IntPtr L, int nArg, long def);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checkstack(IntPtr L, int sz, string msg);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checktype(IntPtr L, int narg, int t);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_checkany(IntPtr L, int narg);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_newmetatable(IntPtr L, string tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_checkudata(IntPtr L, int ud, string tname);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_where(IntPtr L, int lvl);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_error(IntPtr L, string fmt);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_checkoption(IntPtr L, int narg, string def, string[] lst);
        
        /* pre-defined references */
        public const int LUA_NOREF  = (-2);
        public const int LUA_REFNIL = (-1);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_ref(IntPtr L, int t);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_unref(IntPtr L, int t, int @ref);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadfile(IntPtr L, string filename);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadbuffer(IntPtr L, IntPtr buff, nuint sz, string name);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadstring(IntPtr L, string s);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "luaL_loadstring"), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadstring_unmanaged(IntPtr L, IntPtr s);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_newstate();


#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_gsub(IntPtr L, string s, string p, string r);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_findtable(IntPtr L, int idx, string fname, int szhint);

        /* From Lua 5.2. */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_fileresult(IntPtr L, int stat, string fname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_execresult(IntPtr L, int stat);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadfilex(IntPtr L, string filename, string mode);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaL_loadbufferx(IntPtr L, byte[] buff, nuint sz, string name, string mode);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_traceback (IntPtr L, IntPtr L1, string msg, int level);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_setfuncs(IntPtr L, luaL_Reg[] l, int nup);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_pushmodule(IntPtr L, string modname, int sizehint);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaL_testudata(IntPtr L, int ud, string tname);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_setmetatable(IntPtr L, string tname);


        /*
         ** ===============================================================
         ** some useful macros
         ** ===============================================================
         */

        public static void luaL_argcheck(IntPtr L, int cond, int numarg, string extramsg)
        {
            if (cond == 0)
                luaL_argerror(L, numarg, extramsg);
        }

        public unsafe static IntPtr luaL_checkstring(IntPtr L, int n)
            => luaL_checklstring_unmanaged(L, (n), null);

        public unsafe static IntPtr luaL_optstring_unmanaged(IntPtr L, int n, IntPtr d)
            => luaL_optlstring_unmanaged(L, (n), (d), null);

        public unsafe static IntPtr luaL_optstring(IntPtr L, int n, string d)
        { 
            IntPtr def = Marshal.StringToHGlobalAnsi(d);
            IntPtr result = luaL_optlstring_unmanaged(L, n, def, null);
            Marshal.FreeHGlobal(def);
            return result;
        }

        public static int luaL_checkint(IntPtr L, int n)
            => (int)luaL_checkinteger(L, (n));

        public static int luaL_optint(IntPtr L, int n, int d)
            => (int)luaL_optinteger(L, (n), (d));

        public static long luaL_checklong(IntPtr L, int n)
            => (long)luaL_checkinteger(L, (n));

        public static long luaL_optlong(IntPtr L, int n, long d)
            => (long)luaL_optinteger(L, (n), (long)(d));


        public static IntPtr luaL_typename(IntPtr L, int i)
            => lua_typename(L, lua_type(L, (i)));


        public static int luaL_dofile(IntPtr L, string fn)
        {
            int loadfile = luaL_loadfile(L, fn);
            return loadfile != 0 ? loadfile : lua_pcall(L, 0, LUA_MULTRET, 0);
        }


        public static int luaL_dostring(IntPtr L, string s)
        {
            int loadstring = luaL_loadstring(L, s);
            return loadstring != 0 ? loadstring : lua_pcall(L, 0, LUA_MULTRET, 0);
        }

        public static int luaL_dostring_unmanaged(IntPtr L, IntPtr s)
        {
            int loadstring = luaL_loadstring_unmanaged(L, s);
            return loadstring != 0 ? loadstring : lua_pcall(L, 0, LUA_MULTRET, 0);
        }

        public static void luaL_getmetatable(IntPtr L, string n)
            => lua_getfield(L, LUA_REGISTRYINDEX, (n));

        public static T luaL_opt<T>(IntPtr L, Func<IntPtr, int, T> f, int n, T d) where T : unmanaged
            => lua_isnoneornil(L, (n)) != 0 ? d : f.Invoke(L, (n));
        
        
        /* From Lua 5.2. */
        public static void luaL_newlibtable(IntPtr L, luaL_Reg[] l)
            => lua_createtable(L, 0, l.Length - 1);

        public static void luaL_newlib(IntPtr L, luaL_Reg[] l)
        {
            luaL_newlibtable(L, l);
            luaL_setfuncs(L, l, 0);
        }
        
        
        /*
         ** =======================================================
         ** Generic Buffer manipulation
         ** =======================================================
         */
        
        // typedef struct luaL_Buffer {
        //     char *p;			/* current position in buffer */
        //     int lvl;  /* number of strings in the stack (level) */
        //     IntPtr *L;
        //     char buffer[LUAL_BUFFERSIZE];
        // } luaL_Buffer;

        // #define luaL_addchar(B,c) \
        //     ((void)((B)->p < ((B)->buffer+LUAL_BUFFERSIZE) || luaL_prepbuffer(B)), \
        //     (*(B)->p++ = (char)(c)))

        // /* compatibility only */
        // #define luaL_putchar(B,c)	luaL_addchar(B,c)

        // #define luaL_addsize(B,n)	((B)->p += (n))

        // LUALIB_API void (luaL_buffinit) (IntPtr *L, luaL_Buffer *B);
        // LUALIB_API char *(luaL_prepbuffer) (luaL_Buffer *B);
        // LUALIB_API void (luaL_addlstring) (luaL_Buffer *B, const char *s, nuint l);
        // LUALIB_API void (luaL_addstring) (luaL_Buffer *B, const char *s);
        // LUALIB_API void (luaL_addvalue) (luaL_Buffer *B);
        // LUALIB_API void (luaL_pushresult) (luaL_Buffer *B);
        
        
        /*
         ** extensions
         */

        public static IntPtr luaL_tolstring(IntPtr L, int idx, ref nuint len)
        {
            if (luaL_callmeta(L, idx, "__tostring") != 0) /* metafield? */
            {
                if (lua_isstring(L, -1) == 0)
                    luaL_error(L, "'__tostring' must return a string");
            }
            else 
            {
                switch (lua_type(L, idx))
                {
                    case LUA_TNUMBER:
                        lua_pushfstring(L, lua_tonumber(L, idx).ToString(CultureInfo.InvariantCulture));
                        break;

                    case LUA_TSTRING:
                        lua_pushvalue(L, idx);
                        break;

                    case LUA_TBOOLEAN:
                        lua_pushstring(L, (lua_toboolean(L, idx) != 0 ? "true" : "false"));
                        break;

                    case LUA_TNIL:
                        lua_pushstring(L, "nil");
                        break;

                    default:
                        lua_pushfstring(L, $"{luaL_typename(L, idx)}: {lua_topointer(L, idx)}");
                        break;
                }
            }
            return lua_tolstring(L, -1, ref len);
        }
    }
}
