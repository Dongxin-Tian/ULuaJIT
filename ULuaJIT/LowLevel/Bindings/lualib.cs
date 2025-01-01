using System;
using System.Runtime.InteropServices;
using System.Security;
using static ULuaJIT.LowLevel.lua;

namespace ULuaJIT.LowLevel
{
    /// <summary>
    /// lualib.h
    /// </summary>
    public static class lualib
    {
        public const string LUA_FILEHANDLE	= "FILE*";

        public const string LUA_COLIBNAME	= "coroutine";
        public const string LUA_MATHLIBNAME	= "math";
        public const string LUA_STRLIBNAME	= "string";
        public const string LUA_TABLIBNAME	= "table";
        public const string LUA_IOLIBNAME	= "io";
        public const string LUA_OSLIBNAME	= "os";
        public const string LUA_LOADLIBNAME	= "package";
        public const string LUA_DBLIBNAME	= "debug";
        public const string LUA_BITLIBNAME	= "bit";
        public const string LUA_JITLIBNAME	= "jit";
        public const string LUA_FFILIBNAME	= "ffi";

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_base(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_math(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_string(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_table(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_io(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_os(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_package(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_debug(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_bit(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_jit(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_ffi(IntPtr L);

#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaopen_string_buffer(IntPtr L);


#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaL_openlibs(IntPtr L);
    }
}
