using System;
using static ULuaJIT.LowLevel.lua;
using static ULuaJIT.LowLevel.luajit;

namespace ULuaJIT.LowLevel
{
    public static class LuaJIT
    {
        public static void TurnOnJIT(LuaState L)
        {
            if (luaJIT_setmode(L.L, 0, LUAJIT_MODE_ENGINE | LUAJIT_MODE_ON) == 0) {
                throw new LuaException("Turning on JIT failed");
            }
        }

        public static void TurnOffJIT(LuaState L)
        {
            if (luaJIT_setmode(L.L, 0, LUAJIT_MODE_ENGINE | LUAJIT_MODE_OFF) == 0) {
                throw new LuaException("Turning off JIT failed");
            }
        }

        public static void FlushJIT(LuaState L)
        {
            if (luaJIT_setmode(L.L, 0, LUAJIT_MODE_ENGINE | LUAJIT_MODE_FLUSH) == 0) {
                throw new LuaException("Flushing JIT cache failed");
            }
        }
    }
}
