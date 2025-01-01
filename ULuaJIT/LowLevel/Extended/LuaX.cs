// ReSharper disable ArrangeStaticMemberQualifier

using System;

namespace ULuaJIT.LowLevel.Extended
{
    public static class LuaX
    {
        /// <summary>
        /// Try to create a "managed" metatable if the C registry doesn't already have the key 'typeName'.<br/>
        /// A managed metatable is a metatable for managed C# objects. The difference is that managed metatable occupies the '__gc'
        /// metatable for unreferencing the C# object from the managed registry. It will also automatically call the C# 'Dispose'
        /// method if the C# object implements 'IDisposable' (this behavior can be turned off by passing the 'dispose' argument as
        /// false).<br/>
        /// It's important to not overwrite the '__gc' metamethod for managed metatables, otherwise the C# managed object will
        /// stay in the managed registry.<br/>
        /// In cases where the C# object requires additional clean up that cannot be done in 'IDisposable.Dispose', a new special
        /// metamethod '__dispose' can be defined to do so. '__dispose' will be called before 'IDisposable.Dispose'.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="typeName"></param>
        /// <param name="dispose">Whether to call 'Dispose' if the managed object implements 'IDisposable'.</param>
        /// <returns>False if 'typeName' is already occuipied in the C registry.</returns>
        public static bool TryNewManagedMetatable(LuaXState X, string typeName, bool dispose = true)
        {
            if (!Lua.TryNewMetatable(X.L, typeName)) {
                return false;
            }
            
            Lua.SetField(X.L, -1, "__gc", (L) => {
                // Get the managed reference
                ManagedRef mRef = Lua.CheckUserdata<ManagedRef>(L, 1, typeName);
                
                try
                {
                    // Call "__dispose" if exist
                    if (Lua.TryGetMetaField(L, 1, "__dispose"))
                    {
                        if (Lua.PCall(L, 0, 0, 0) != LuaStatus.Ok)
                        {
                            string message = Lua.ToStringNonNull(L, -1);
                            Lua.Pop(L, 1);
                            throw new LuaException(message);
                        }
                    }

                    // Dispose if needed
                    object obj = X.ManagedRegistry.Get(mRef);
                    if (dispose && obj is IDisposable disposable) {
                        disposable.Dispose();
                    }
                }
                finally
                {
                    // Unref the managed reference
                    X.ManagedRegistry.Unref(mRef);   
                }
                
                return 0;
            });

            return true;
        }
        
        public static ManagedRef NewManagedUserdata(LuaXState X, object obj, string typeName)
            => Lua.NewUserdata<ManagedRef>(X.L, X.ManagedRegistry.Ref(obj), typeName);

        public static ManagedRef NewManagedUserdata(LuaXState X, object obj)
            => Lua.NewUserdata<ManagedRef>(X.L, X.ManagedRegistry.Ref(obj));
        
        public static ManagedRef CheckManagedRef(LuaXState X, int index, string typeName)
            => Lua.CheckUserdata<ManagedRef>(X.L, index, typeName);
        
        public static object CheckManagedUserdata(LuaXState X, int index, string typeName)
            => X.ManagedRegistry.Get(LuaX.CheckManagedRef(X, index, typeName));

        public static T CheckManagedUserdata<T>(LuaXState X, int index, string typeName) where T : class
            => (T)LuaX.CheckManagedUserdata(X, index, typeName);



        public static void PushCClosure(LuaXState X, LuaXFunc xFunc, int n)
            => Lua.PushCClosure(X, (_) => xFunc(X), n);
        
        
        
        public static void SetField(LuaXState X, int index, string key, LuaXFunc xFunc)
            => Lua.SetField(X.L, index, key, (_) => xFunc(X));
    }
}
