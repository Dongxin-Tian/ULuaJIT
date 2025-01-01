#nullable enable

using System;
using System.IO;
using System.Runtime.CompilerServices;
using ULuaJIT.LowLevel;

namespace ULuaJIT
{
    public class LuaRuntime : IDisposable
    {
        /* ===== State ===== */

        public LuaState L { get; protected set; }



        /* ===== Constructor & Disposing ===== */

        public LuaRuntime(LuaAllocatorBase? allocator = null)
        {
            // Create new Lua state
            L = allocator is null ? Lua.NewState() : Lua.NewState(allocator);

            // Open all standard Lua libraries
            Lua.OpenLibs(L);
        }

        public bool IsDisposed { get; protected set; } = false;

        protected virtual void Dispose(bool isDisposing)
            => Lua.Close(L);
        
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfDisposed()
        {
            if (IsDisposed) {
                throw new ObjectDisposedException(GetType().FullName);   
            }
        }

        ~LuaRuntime()
            => Dispose(false);
        
        
        
        /* ===== Compilation & Execution ===== */

        public virtual byte[] CompileString(string code)
        {
            ThrowIfDisposed();
            
            if (Lua.LoadString(L, code) != LuaStatus.Ok)
            {
                string message = Lua.ToStringNonNull(L, -1);
                Lua.Pop(L, 1);
                throw new LuaException(message);
            }

            // Compile the code and dump the binary chunk
            MemoryStream stream = new MemoryStream();
            if (Lua.Dump(L, stream) != LuaStatus.Ok) {
                throw new LuaException("Dumping binary chunk failed");
            }
            return stream.ToArray();
        }

        public virtual byte[] CompileFile(string path)
        {
            ThrowIfDisposed();

            if (Lua.LoadFile(L, path) != LuaStatus.Ok)
            {
                string message = Lua.ToStringNonNull(L, -1);
                Lua.Pop(L, 1);
                throw new LuaException(message);
            }

            // Compile the file and dump the binary chunk
            MemoryStream stream = new MemoryStream();
            if (Lua.Dump(L, stream) != LuaStatus.Ok) {
                throw new LuaException("Dumping binary chunk failed");
            }
            return stream.ToArray();
        }
        
        public virtual void DoString(string code)
        {
            ThrowIfDisposed();

            if (Lua.DoString(L, code) != LuaStatus.Ok)
            {
                string message = Lua.ToStringNonNull(L, -1);
                Lua.Pop(L, 1);
                throw new LuaException(message);
            }
        }
        
        public virtual void DoFile(string path)
        {
            ThrowIfDisposed();

            if (Lua.DoFile(L, path) != LuaStatus.Ok)
            {
                string message = Lua.ToStringNonNull(L, -1);
                Lua.Pop(L, 1);
                throw new LuaException(message);
            }
        }

        public virtual void DoChunk(byte[] chunk)
        {
            ThrowIfDisposed();

            // Load chunk as a buffer
            if (Lua.LoadBuffer(L, chunk) != LuaStatus.Ok)
            {
                string message = Lua.ToStringNonNull(L, -1);
                Lua.Pop(L, 1);
                throw new LuaException(message);
            }

            // Execute chunk
            if (Lua.PCallMultiRet(L, 0, 0) != LuaStatus.Ok)
            {
                string message = Lua.ToStringNonNull(L, -1);
                Lua.Pop(L, 1);
                throw new LuaException(message);
            }
        }
        
        
        
        /* ===== JIT ===== */

        public void TurnOnJIT()
        {
            ThrowIfDisposed();
            LuaJIT.TurnOnJIT(L);
        }
        
        public void TurnOffJIT()
        {
            ThrowIfDisposed();
            LuaJIT.TurnOffJIT(L);
        }

        public void FlushJIT()
        {
            ThrowIfDisposed();
            LuaJIT.FlushJIT(L);
        }
        
        
        
        /* ===== Library ===== */

        public void OpenLibrary(ILuaLibrary library)
            => library.Open(L);

        public void OpenLibrary<T>(T library) where T : ILuaLibrary
            => library.Open(L);
    }
}
