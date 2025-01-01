using System.Runtime.InteropServices;

namespace ULuaJIT.LowLevel
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LuaCFunc(LuaState L);
}
