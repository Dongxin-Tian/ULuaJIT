using System;
using System.Runtime.InteropServices;

namespace ULuaJIT.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct LuaState : IEquatable<LuaState>
    {
        public readonly IntPtr L;

        internal LuaState(IntPtr L)
            => this.L = L;

        public bool Equals(LuaState other)
            => L.Equals(other.L);

        public override bool Equals(object obj)
            => obj is LuaState other && Equals(other);

        public override int GetHashCode()
            => L.GetHashCode();

        public static bool operator ==(LuaState left, LuaState right)
            => left.Equals(right);

        public static bool operator !=(LuaState left, LuaState right)
            => !left.Equals(right);
    }
}
