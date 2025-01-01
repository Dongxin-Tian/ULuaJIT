namespace ULuaJIT.LowLevel.Extended
{
    public readonly struct ManagedRef
    {
        public readonly int Ref;

        internal ManagedRef(int @ref) {
            Ref = @ref;
        }
    }
}
