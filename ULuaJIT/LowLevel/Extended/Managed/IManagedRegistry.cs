namespace ULuaJIT.LowLevel.Extended
{
    public interface IManagedRegistry
    {
        ManagedRef Ref(object obj);
        
        void Unref(ManagedRef mf);
        
        object Get(ManagedRef mf);
    }
}
