#nullable enable

using ULuaJIT.LowLevel;
using ULuaJIT.LowLevel.Extended;

namespace ULuaJIT
{
    public class LuaXRuntime<MRegistry> : LuaRuntime where MRegistry : IManagedRegistry, new()
    {
        /* ===== Managed Registry ===== */
        
        protected readonly MRegistry mRegistry = new MRegistry();
        
        
        
        /* ===== Extended State ===== */
        
        protected readonly LuaXState X;
        
        
        
        /* ===== Constructor ===== */
        
        public LuaXRuntime(LuaAllocatorBase? allocator = null) : base(allocator)
            => X = new LuaXState(L, mRegistry);
        
        
        
        /* ===== Library ===== */

        public void OpenXLibrary(ILuaXLibrary library)
            => library.Open(X);

        public void OpenXLibrary<T>(T library) where T : ILuaXLibrary
            => library.Open(X);
    }
}
