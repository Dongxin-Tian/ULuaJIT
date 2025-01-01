namespace ULuaJIT.LowLevel
{
    public readonly struct LuaRef
    {
        public readonly int Ref;

        internal LuaRef(int @ref) {
            Ref = @ref;
        }
    }
}
