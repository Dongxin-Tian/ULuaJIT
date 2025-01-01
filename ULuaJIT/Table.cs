using System;
using ULuaJIT.LowLevel;

namespace ULuaJIT
{
    // TODO
    public sealed class Table : IDisposable
    {
        /* ===== Data ===== */
        
        private LuaRuntime runtime;
        private readonly LuaRef @ref;
        
        
        
        /* ===== Constructor & Disposing ===== */
        
        public Table(LuaRuntime runtime)
        {
            this.runtime = runtime;
            Lua.NewTable(runtime.L);
            @ref = Lua.RegistryRef(runtime.L);
        }

        private bool isDisposed = false;

        private void DisposeHelper()
        {
            // Unref table in the registry
            if (!runtime.IsDisposed) {
                Lua.RegistryUnref(runtime.L, @ref);
            }
        }
        
        public void Dispose()
        {
            if (isDisposed) {
                return;
            }
            isDisposed = true;

            DisposeHelper();
            GC.SuppressFinalize(this);
        }

        ~Table()
            => DisposeHelper();
    }
}
