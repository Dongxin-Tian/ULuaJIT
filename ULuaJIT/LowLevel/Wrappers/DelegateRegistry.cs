using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ULuaJIT.LowLevel
{
    internal static class DelegateRegistry
    {
        private static readonly ConcurrentDictionary<LuaState, Dictionary<Delegate, IntPtr>> registry = new();

        internal static IntPtr GetFunctionPointer(LuaState L, Delegate del)
        {
            // Get delegate to function pointer map
            Dictionary<Delegate, IntPtr> map = registry.GetOrAdd(L, static _ => new Dictionary<Delegate, IntPtr>());
            
            // Get function pointer
            if (!map.TryGetValue(del, out IntPtr fp))
            {
                fp = Marshal.GetFunctionPointerForDelegate(del);
                map.Add(del, fp);
            }
            return fp;
        }

        internal static void Release(LuaState L)
            => registry.TryRemove(L, out _);
    }
}
