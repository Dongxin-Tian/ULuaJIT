using System;
using System.Runtime.InteropServices;
using System.Security;
using static ULuaJIT.LowLevel.lua;

namespace ULuaJIT.LowLevel
{
    /// <summary>
    /// luajit.h
    /// </summary>
    public static class luajit
    {
        public const string LUAJIT_VERSION = "LuaJIT 2.1.ROLLING";
        public const string LUAJIT_COPYRIGHT = "Copyright (C) 2005-2023 Mike Pall";
        public const string LUAJIT_URL = "https://luajit.org/";

        /* Modes for luaJIT_setmode. */
        public const int LUAJIT_MODE_MASK = 0x00ff;

        public const int LUAJIT_MODE_ENGINE     = 0x00;	  /* Set mode for whole JIT engine. */
        public const int LUAJIT_MODE_DEBUG      = 0x01;	  /* Set debug mode (idx = level). */

        public const int LUAJIT_MODE_FUNC       = 0x02;	  /* Change mode for a function. */
        public const int LUAJIT_MODE_ALLFUNC    = 0x03;	  /* Recurse into subroutine protos. */
        public const int LUAJIT_MODE_ALLSUBFUNC = 0x04;	  /* Change only the subroutines. */

        public const int LUAJIT_MODE_TRACE      = 0x05;	  /* Flush a compiled trace. */

        public const int LUAJIT_MODE_WRAPCFUNC  = 0x10;	  /* Set wrapper mode for C function calls. */

        public const int LUAJIT_MODE_MAX        = 0x11;

        /* Flags or'ed in to the mode. */
        public const int LUAJIT_MODE_OFF    = 0x0000;	/* Turn feature off. */
        public const int LUAJIT_MODE_ON     = 0x0100;	/* Turn feature on. */
        public const int LUAJIT_MODE_FLUSH  = 0x0200;	/* Flush JIT-compiled code. */

        /* LuaJIT public C API. */

        /* Control the JIT engine. */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern int luaJIT_setmode(IntPtr L, int idx, int mode);

        /* Low-overhead profiling API. */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void luaJIT_profile_callback(IntPtr data, IntPtr L, int samples, int vmstate);
        
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaJIT_profile_start(IntPtr L, string mode, luaJIT_profile_callback cb, IntPtr data);
      
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void luaJIT_profile_stop(IntPtr L);
      
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern IntPtr luaJIT_profile_dumpstack(IntPtr L, string fmt, int depth, ref nuint len);

        /* Enforce (dynamic) linker error for version mismatches. Call from main. */
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
#else
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), SuppressUnmanagedCodeSecurity]
#endif
        public static extern void LUAJIT_VERSION_SYM();
    }
}
