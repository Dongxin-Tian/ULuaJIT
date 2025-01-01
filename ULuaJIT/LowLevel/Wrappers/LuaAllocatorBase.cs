using System;

namespace ULuaJIT.LowLevel
{
    public abstract class LuaAllocatorBase
    {
        public abstract IntPtr Allocate(nuint size);
        public abstract IntPtr Reallocate(IntPtr L, nuint oldSize, nuint newSize);
        public abstract void Free(IntPtr ptr);
        
        public IntPtr LuaAlloc(IntPtr _, IntPtr ptr, nuint oldSize, nuint newSize)
        {
            // Free
            if (newSize == 0)
            {
                Free(ptr);
                return IntPtr.Zero;
            }
            
            // Allocate
            if (ptr == IntPtr.Zero && oldSize == 0 && newSize > 0) {
                return Allocate(newSize);
            }
            
            // Reallocate
            return Reallocate(ptr, oldSize, newSize);
        }
    }
}
