namespace ULuaJIT.LowLevel.Extended
{
    public sealed record LuaXState(LuaState L, IManagedRegistry ManagedRegistry)
    {
        public LuaState L { get; } = L;

        public IManagedRegistry ManagedRegistry { get; } = ManagedRegistry;

        public static implicit operator LuaState(LuaXState X)
            => X.L;
    }
}
