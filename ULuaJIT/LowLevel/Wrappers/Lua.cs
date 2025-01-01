// ReSharper disable ArrangeStaticMemberQualifier

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static ULuaJIT.LowLevel.lauxlib;
using static ULuaJIT.LowLevel.lua;
using static ULuaJIT.LowLevel.lualib;

namespace ULuaJIT.LowLevel
{
    public static class Lua
    {
        /* ===== Errors ===== */
        
        /// <summary>
        /// Generates a Lua exception. The error message (which can actually be a Lua value of any type) must be on the
        /// stack top.
        /// </summary>
        public static int Error(LuaState L)
        {
            
            
            throw new LuaException();
        }

        /// <summary>
        /// Throws an exception. It also adds at the beginning of the message the file name and the line number where the
        /// error occurred, if this information is available.
        /// </summary>
        public static int Error(LuaState L, string message)
        {
            Lua.Where(L, 1);
            Lua.PushString(L, message);
            Lua.Concat(L, 2);
            return Lua.Error(L);
        }
        
        public static int ArgError(LuaState L, int index, string extraMessage)
        {
            lua_Debug debug = new lua_Debug();

            if (lua_getstack(L.L, 0, ref debug) == 0) {
                return Lua.Error(L, $"bad argument #{index} ({extraMessage})");
            }
            
            lua_getinfo(L.L ,"n", ref debug);
            if (Marshal.PtrToStringAnsi(debug.namewhat) == "method")
            {
                index--;
                if (index == 0) {
                    return Lua.Error(L, $"calling {LUA_QL(Marshal.PtrToStringAnsi(debug.name))} on bad self ({extraMessage})");
                }
            }

            string name = (debug.name == IntPtr.Zero) ? "?" : Marshal.PtrToStringAnsi(debug.name);
            return Lua.Error(L, $"bad argument #{index} to {LUA_QL(name)} ({extraMessage})");
        }
        
        public static int TypeError(LuaState L, int index, string typeName)
            => ArgError(L, index, $"{typeName} expected, got {Lua.TypeName(L, index)}");
        
        public static void CheckType(LuaState L, int index, LuaType type)
        {
            if (Lua.Type(L, index) != type) {
                TagError(L, index, type);
            }
        }

        public static void CheckAny(LuaState L, int index)
        {
            if (Lua.Type(L, index) == LuaType.None) {
                Lua.ArgError(L, index, "value expected");
            }
        }

        public static long CheckInteger(LuaState L, int index)
        {
            if (!Lua.TryToInteger(L, index, out long i)) {
                TagError(L, index, LuaType.Number);
            }
            return i;
        }
        
        public static int CheckInt(LuaState L, int index)
            => (int)Lua.CheckInteger(L, index);

        public static long CheckLong(LuaState L, int index)
            => Lua.CheckInteger(L, index);

        public static double CheckNumber(LuaState L, int index)
        {
            if (!Lua.TryToNumber(L, index, out double d)) {
                TagError(L, index, LuaType.Number);   
            }
            return d;
        }
        
        public static float CheckFloat(LuaState L, int index)
            => (float)Lua.CheckNumber(L, index);

        public static string CheckString(LuaState L, int index)
        {
            string s = Lua.ToString(L, index);
            if (s is null) {
                TagError(L, index, LuaType.String);
            }
            return s;
        }

        public static unsafe ref T CheckUserdata<T>(LuaState L, int index, string typeName) where T : unmanaged
        {
            void* p = Lua.ToUserdata(L, index).ToPointer();
            if (p != null)
            {
                if (Lua.TryGetMetatable(L, index))
                {
                    Lua.GetRegistryField(L, typeName);
                    if (Lua.RawEqual(L, -1, -2))
                    {
                        Lua.Pop(L, 2);
                        return ref *((T*)p);
                    }
                    Lua.Pop(L, 2);
                }
            }
            Lua.TypeError(L, index, typeName);
            throw new LuaException(); // Unreachable
        }
        
        public static long OptInteger(LuaState L, int index, long def)
            => Lua.IsNoneOrNil(L, index) ? def : Lua.CheckInteger(L, index);

        public static int OptInt(LuaState L, int index, int def)
            => (int)Lua.OptInteger(L, index, (long)def);

        public static long OptLong(LuaState L, int index, long def)
            => Lua.OptInteger(L, index, def);

        public static double OptNumber(LuaState L, int index, double def)
            => Lua.IsNoneOrNil(L, index) ? def : Lua.CheckNumber(L, index);

        public static float OptFloat(LuaState L, int index, float def)
            => (float)Lua.OptNumber(L, index, (double)def);

        public static string OptString(LuaState L, int index, string def)
            => Lua.IsNoneOrNil(L, index) ? def : Lua.CheckString(L, index);
        
        private static void TagError(LuaState L, int index, LuaType tag){
            Lua.TypeError(L, index, Lua.TypeName(L, tag));
        }
        
        
        
        /* ===== Direct Wrappers ===== */

        public static void Close(LuaState L)
        {
            DelegateRegistry.Release(L);
            lua_close(L.L);
        }

        public static void Concat(LuaState L, int n)
            => lua_concat(L.L, n);

        /// <summary>
        /// Calls 'luaL_dofile'.<br/>
        /// Loads and runs the given file
        /// </summary>
        public static LuaStatus DoFile(LuaState L, string fileName)
            => IntToLuaStatus(luaL_dofile(L.L, fileName));
        
        /// <summary>
        /// Calls 'luaL_dostring'.<br/>
        /// Loads and runs the given string.
        /// </summary>
        public static LuaStatus DoString(LuaState L, string str)
            => IntToLuaStatus(luaL_dostring(L.L, str));

        /// <summary>
        /// Calls 'luaL_dostring'.<br/>
        /// Loads and runs the given byte array as UTF-8 encoded string.
        /// </summary>
        public static unsafe LuaStatus DoStringUTF8(LuaState L, byte[] str)
        {
            fixed (byte* p = str) {
                return IntToLuaStatus(luaL_dostring_unmanaged(L.L, new IntPtr(p)));
            }
        }
        
        /// <summary>
        /// Calls 'lua_dump'.<br/>
        /// Dumps a function as a binary chunk. Receives a Lua function on the top of the stack and produces a binary chunk
        /// that, if loaded again, results in a function equivalent to the one dumped.<br/>
        /// This function does not pop the Lua function from the stack.
        /// </summary>
        public static LuaStatus Dump(LuaState L, Stream stream)
        {
            return IntToLuaStatus(lua_dump(L.L, Writer, IntPtr.Zero));

            int Writer(IntPtr L, IntPtr data, UIntPtr sz, IntPtr intPtr1)
            {
                try
                {
                    int size = (int)sz;
                    byte[] buffer = new byte[size];
                    Marshal.Copy(data, buffer, 0, size);
                    stream.Write(buffer, 0, size);
                }
                catch (Exception) {
                    return LUA_ERRRUN;
                }
                
                return LUA_OK;
            }
        }
        
        /// <summary>
        /// Calls 'lua_getfield'.<br/>
        /// Pushes onto the stack the value t[k], where t is the value at the given valid index. As in Lua, this function
        /// may trigger a metamethod for the "index" event
        /// </summary>
        public static void GetField(LuaState L, int index, string key)
            => lua_getfield(L.L, index, key);
        
        public static void GetRegistryField(LuaState L, string key)
            => Lua.GetField(L, LUA_REGISTRYINDEX, key);

        /// <summary>
        /// Calls 'lua_gettop'.<br/>
        /// Returns the index of the top element in the stack. Because indices start at 1, this result is equal to the number
        /// of elements in the stack (and so 0 means an empty stack).
        /// </summary>
        public static int GetTop(LuaState L)
            => lua_gettop(L.L);

        /// <summary>
        /// Calls 'lua_insert'.<br/>
        /// Moves the top element into the given valid index, shifting up the elements above this index to open space.
        /// Cannot be called with a pseudo-index, because a pseudo-index is not an actual stack position.
        /// </summary>
        public static void Insert(LuaState L, int index)
            => lua_insert(L.L, index);

        /// <summary>
        /// Calls 'lua_isboolean'.<br/>
        /// Returns true if the value at the given acceptable index has type boolean, and false otherwise.
        /// </summary>
        public static bool IsBoolean(LuaState L, int index)
            => lua_isboolean(L.L, index) != 0;
        
        /// <summary>
        /// Calls 'lua_iscfunction'.<br/>
        /// Returns true if the value at the given acceptable index is a C function, and false otherwise.
        /// </summary>
        public static bool IsCFunction(LuaState L, int index)
            => lua_iscfunction(L.L, index) != 0;
        
        /// <summary>
        /// Calls 'lua_isfunction'.<br/>
        /// Returns true if the value at the given acceptable index is a function (either C or Lua), and false otherwise.
        /// </summary>
        public static bool IsFunction(LuaState L, int index)
            => lua_isfunction(L.L, index) != 0;

        /// <summary>
        /// Calls 'lua_islightuserdata'.<br/>
        /// Returns true if the value at the given acceptable index is a light userdata, and false otherwise.
        /// </summary>
        public static bool IsLightUserdata(LuaState L, int index)
            => lua_islightuserdata(L.L, index) != 0;
        
        /// <summary>
        /// Calls 'lua_isnil'.<br/>
        /// Returns true if the value at the given index is 'nil', and false otherwise.
        /// </summary>
        public static bool IsNil(LuaState L, int index)
            => lua_isnil(L.L, index) != 0;

        /// <summary>
        /// Calls 'lua_isnone'.<br/>
        /// Returns true if the given index is not valid, and false otherwise.
        /// </summary>
        public static bool IsNone(LuaState L, int index)
            => lua_isnone(L.L, index) != 0;

        /// <summary>
        /// Calls 'lua_isnoneornil'.<br/>
        /// Returns true if the given acceptable index is not valid (that is, it refers to an element outside the current
        /// stack) or if the value at this index is 'nil', and false otherwise.
        /// </summary>
        public static bool IsNoneOrNil(LuaState L, int index)
            => lua_isnoneornil(L.L, index) != 0;

        /// <summary>
        /// Calls 'lua_isnumber'.<br/>
        /// Returns true if the value at the given acceptable index is a number or a string convertible to a number, and
        /// false otherwise.
        /// </summary>
        public static bool IsNumber(LuaState L, int index)
            => lua_isnumber(L.L, index) != 0;
        
        /// <summary>
        /// Calls 'lua_istable'.<br/>
        /// Returns true if the value at the given index is a table, and false otherwise.
        /// </summary>
        public static bool IsTable(LuaState L, int index)
            => lua_istable(L.L, index) != 0;

        /// <summary>
        /// Calls 'lua_isuserdata'.<br/>
        /// Returns true if the value at the given index is an userdata (either full or light), and false otherwise.
        /// </summary>
        public static bool IsUserdata(LuaState L, int index)
            => lua_isuserdata(L.L, index) != 0;

#nullable enable
        public static unsafe LuaStatus LoadBuffer(LuaState L, byte[] buffer, string? name = null)
        {
            fixed (byte* p = buffer) {
                return IntToLuaStatus(luaL_loadbuffer(L.L, new IntPtr(p), (nuint)(buffer.Length * sizeof(byte)), name));
            }
        }
#nullable restore
        
        /// <summary>
        /// Calls 'luaL_loadfile'<br/>
        /// Loads a file as a Lua chunk. This function uses 'lua_load' to load the chunk in the file named 'fileName'. If
        /// 'fileName' is null, then it loads from the standard input. The first line in the file is ignored if it starts with
        /// a #.<br/>
        /// This function returns the same results as 'lua_load', but it has an extra error code 'LuaStatus.ErrorFile' if it
        /// cannot open/read the file.<br/>
        /// As 'lua_load', this function only loads the chunk; it does not run it.
        /// </summary>
        public static LuaStatus LoadFile(LuaState L, string fileName)
            => IntToLuaStatus(luaL_loadfile(L.L, fileName));
        
        /// <summary>
        /// Calls 'luaL_loadstring'.<br/>
        /// Loads a string as a Lua chunk. This function uses 'lua_load' to load the chunk in the string 's'.<br/>
        /// This function returns the same results as 'lua_load'.<br/>
        /// Also as 'lua_load', this function only loads the chunk; it does not run it.
        /// </summary>
        public static LuaStatus LoadString(LuaState L, string s)
            => IntToLuaStatus(luaL_loadstring(L.L, s));
        
        public static LuaType Type(LuaState L, int index)
        {
            int typeCode = lua_type(L.L, index);
#if ULUAJIT_DEBUG
            if (!Enum.IsDefined(typeof(LuaType), typeCode)) {
                throw new NotImplementedException($"'{nameof(LuaType)}' member with int value of '{typeCode}' is not defined");
            }
#endif
            return (LuaType)typeCode;
        }

        /// <summary>
        /// Calls 'lua_typename'.<br/>
        /// Returns the name of the type encoded by the 'LuaType' enum instance type, which must be one of the values returned
        /// by 'Lua.Type'.
        /// </summary>
        public static string TypeName(LuaState L, LuaType type)
            => Marshal.PtrToStringUTF8(lua_typename(L.L, type.ToTypeCode()));
        
        /// <summary>
        /// Calls 'luaL_typename'.<br/>
        /// Returns the name of the type of the value at the given index.
        /// </summary>
        public static string TypeName(LuaState L, int index)
            => Marshal.PtrToStringUTF8(luaL_typename(L.L, index));

        public const int MultRet = LUA_MULTRET;

        public static bool TryPCall(LuaState L, int nArgs, int nResults, int errorFuncIndex, out LuaStatus status)
        {
            int top = Lua.GetTop(L);

            try
            {
                int statusCode = lua_pcall(L.L, nArgs, nResults, errorFuncIndex);
                if (statusCode == LUA_OK)
                {
                    // Succeeded without Lua errors or C# exceptions
                    status = LuaStatus.Ok;
                    return true;
                }

                // Lua error raised
                status = IntToLuaStatus(statusCode);
                return false;
            }
            catch (Exception e)
            {
                // C# exception thrown
                Lua.SetTop(L, top); // Restore Lua stack top
                Lua.PushString(L, e.Message); // Push exception message as error message

                // Call error handler if present
                if (errorFuncIndex != 0)
                {
                    if (!Lua.TryPCall(L, 1, 1, 0, out _))
                    {
                        status = LuaStatus.ErrorErrorHandler;
                        return false;
                    }
                }

                // Running failed
                status = LuaStatus.ErrorRun;
                return false;
            }
        }

        public static bool TryPCallMultiRet(LuaState L, int nArgs, int errorFuncIndex, out LuaStatus status)
            => Lua.TryPCall(L, nArgs, MultRet, errorFuncIndex, out status);
        
        /// <summary>
        /// Calls a function in protected mode.
        /// </summary>
        public static LuaStatus PCall(LuaState L, int nArgs, int nResults, int errorFuncIndex)
            => Lua.TryPCall(L, nArgs, nResults, errorFuncIndex, out LuaStatus status) ? status : LuaStatus.Ok;

        public static LuaStatus PCallMultiRet(LuaState L, int nArgs, int errorFuncIndex)
            => Lua.PCall(L, nArgs, MultRet, errorFuncIndex);
        
        public static void Pop(LuaState L, int n)
            => lua_pop(L.L, n);

        public static void PushBoolean(LuaState L, bool b)
            => lua_pushboolean(L.L, b ? 1 : 0);

        /// <summary>
        /// Calls 'lua_pushcclosure'.<br/>
        /// Pushes a new C closure onto the stack.<br/>
        /// When a C function is created, it is possible to associate some values with it, thus creating a C closure; these
        /// values are then accessible to the function whenever it is called. To associate values with a C function, first
        /// these values should be pushed onto the stack (when there are multiple values, the first value is pushed first).
        /// Then 'lua_pushcclosure' is called to create and push the C function onto the stack, with the argument n telling
        /// how many values should be associated with the function. 'lua_pushcclosure' also pops these values from the stack.<br/>
        /// The maximum value for n is 255.
        /// </summary>
        public static void PushCClosure(LuaState L, LuaCFunc cFunc, int n)
            => lua_pushcclosure(L.L, DelegateRegistry.GetFunctionPointer(L, cFunc), n);

        public static void PushCFunc(LuaState L, LuaCFunc cFunc)
            => lua_pushcfunction(L.L, DelegateRegistry.GetFunctionPointer(L, cFunc));
        
        public static void PushInteger(LuaState L, long integer)
            => lua_pushinteger(L.L, integer);

        public static void PushInt(LuaState L, int integer)
            => Lua.PushInteger(L, (long)integer);

        public static void PushLightUserdata(LuaState L, IntPtr p)
            => lua_pushlightuserdata(L.L, p);
        
        public static void PushNil(LuaState L)
            => lua_pushnil(L.L);
        
        public static void PushNumber(LuaState L, double number)
            => lua_pushnumber(L.L, number);
        
        public static void PushFloat(LuaState L, float f)
            => Lua.PushNumber(L, (double)f);

        /// <summary>
        /// Calls 'lua_pushlstring'.<br/>
        /// Pushes the string onto the stack. The string can contain embedded zeros.
        /// </summary>
        public static void PushString(LuaState L, string str)
            => PushStringUTF8(L, Encoding.UTF8.GetBytes(str));

        /// <summary>
        /// Calls 'lua_pushlstring'.<br/>
        /// Pushes byte array as a UTF-8 string onto the stack. The string can contain embedded zeros.
        /// </summary>
        public static unsafe void PushStringUTF8(LuaState L, byte[] str)
        {
            fixed (byte* p = str) {
                lua_pushlstring(L.L, new IntPtr(p), (nuint)str.Length);   
            }
        }
        
        /// <summary>
        /// Calls 'lua_pushlstring'.<br/>
        /// Pushes byte span as a UTF-8 string onto the stack. The string can contain embedded zeros.
        /// </summary>
        public static unsafe void PushStringUTF8(LuaState L, ReadOnlySpan<byte> str)
        {
            fixed (byte* p = str) {
                lua_pushlstring(L.L,  new IntPtr(p), (nuint)str.Length);
            }
        }

        public static void PushValue(LuaState L, int index)
            => lua_pushvalue(L.L, index);

        public static bool RawEqual(LuaState L, int index1, int index2)
            => lua_rawequal(L.L, index1, index2) != 0;
        
        /// <summary>
        /// Pushes onto the stack the value t[n], where t is the value at the given valid index. The access is raw; that is,
        /// it does not invoke metamethods.
        /// </summary>
        public static void RawGetI(LuaState L, int index, int n)
            => lua_rawgeti(L.L, index, n);

        public static void SetTop(LuaState L, int index)
            => lua_settop(L.L, index);
        
        public static bool ToBoolean(LuaState L, int index)
            => lua_toboolean(L.L, index) != 0;

        public static IntPtr ToUserdata(LuaState L, int index)
            => lua_touserdata(L.L, index);
        
        public static bool TryToInteger(LuaState L, int index, out long integer)
        {
            int isInt = 0;
            integer = lua_tointegerx(L.L, index, ref isInt);
            return isInt != 0;
        }

        public static bool TryToInt(LuaState L, int index, out int integer)
        {
            bool b = TryToInteger(L, index, out long l);
            integer = (int)l;
            return b;
        }

        public static bool TryToNumber(LuaState L, int index, out double number)
        {
            int isNum = 0;
            number = lua_tonumberx(L.L, index, ref isNum);
            return isNum != 0;
        }

        public static bool TryToFloat(LuaState L, int index, out float f)
        {
            bool b = TryToNumber(L, index, out double number);
            f = (float)number;
            return b;
        }
        
#nullable enable
        public static unsafe string? ToString(LuaState L, int index)
        {
            UIntPtr len;
            void* strPtr = lua_tolstring_unmanaged(L.L, index, &len).ToPointer();
            return strPtr == null ? null : Encoding.UTF8.GetString(new ReadOnlySpan<byte>(strPtr, (int)len));
        }
        
        public static unsafe bool TryToString(LuaState L, int index, out string? str)
        {
            UIntPtr len;
            void* strPtr = lua_tolstring_unmanaged(L.L, index, &len).ToPointer();
            if (strPtr == null)
            {
                str = null;
                return false;
            }
            str = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(strPtr, (int)len));
            return true;
        }
#nullable restore

        public static void NewTable(LuaState L)
            => lua_newtable(L.L);

        public static void SetGlobal(LuaState L, string name)
            => lua_setglobal(L.L, name);

        public static unsafe ref T NewUserdata<T>(LuaState L) where T : unmanaged
            => ref *((T*)(lua_newuserdata(L.L, (UIntPtr)(sizeof(T)))));

        public static ref T NewUserdata<T>(LuaState L, T value) where T : unmanaged
        {
            ref T ud = ref NewUserdata<T>(L);
            ud = value;
            return ref ud;
        }
        
        public static unsafe ref T NewUserdata<T>(LuaState L, string metatableName) where T : unmanaged
        {
            T* ud = (T*)(lua_newuserdata(L.L, (UIntPtr)(sizeof(T))));
            luaL_setmetatable(L.L, metatableName);
            return ref *ud;
        }

        public static ref T NewUserdata<T>(LuaState L, T value, string metatableName) where T : unmanaged
        {
            ref T ud = ref NewUserdata<T>(L, metatableName);
            ud = value;
            return ref ud;
        }

        /// <summary>
        /// Calls 'luaL_openlibs'.
        /// Opens all standard Lua libraries into the given state.
        /// </summary>
        public static void OpenLibs(LuaState L)
            => luaL_openlibs(L.L);

        public static void SetField(LuaState L, int index, string key)
            => lua_setfield(L.L, index, key);

        public static void SetField(LuaState L, int index, string key, LuaCFunc value)
        {
            Lua.PushCFunc(L, value);
            lua_setfield(L.L, index < 0 ? index - 1 : index, key);
        }

        public static void SetField<T>(LuaState L, int index, string key, T value) where T : unmanaged
        {
            ref T ud = ref NewUserdata<T>(L);
            ud = value;
            lua_setfield(L.L, index < 0 ? index - 1 : index, key);
        }
        
        public static void SetField<T>(LuaState L, int index, string key, T value, string metatableName) where T : unmanaged
        {
            ref T ud = ref NewUserdata<T>(L, metatableName);
            ud = value;
            lua_setfield(L.L, index < 0 ? index - 1 : index, key);
        }

        public static bool TryNewState(out LuaState luaState)
        {
            IntPtr L = luaL_newstate();
            if (L == IntPtr.Zero)
            {
                luaState = new LuaState(IntPtr.Zero);
                return false;
            }
            luaState = new LuaState(L);
            return true;
        }

        public static bool TryNewState(out LuaState luaState, LuaAllocatorBase allocator)
        {
            // Try to create a new Lua state with the allocator
            lua_Alloc alloc = allocator.LuaAlloc;
            IntPtr L = lua_newstate(alloc, IntPtr.Zero);
            if (L == IntPtr.Zero)
            {
                luaState = new LuaState(IntPtr.Zero);
                return false;
            }
            luaState = new LuaState(L);
            
            // Store the allocator function in the state's delegate registry to prevent it from being collected
            DelegateRegistry.GetFunctionPointer(luaState, (lua_Alloc)allocator.LuaAlloc);
            
            return true;
        }
        
        /// <summary>
        /// Calls 'luaL_newstate'.<br/>
        /// Creates a new Lua state. It calls 'lua_newstate' with an allocator based on the standard C 'realloc' function and
        /// then sets a panic function (see 'lua_atpanic') that prints an error message to the standard error output in case
        /// of fatal errors.
        /// </summary>
        /// <returns>Returns the new state</returns>
        public static LuaState NewState()
        {
            if (!TryNewState(out LuaState luaState)) {
                throw new LuaException("Failed to create the Lua state");
            }
            return luaState;
        }

        public static LuaState NewState(LuaAllocatorBase allocator)
        {
            if (!TryNewState(out LuaState luaState, allocator)) {
                throw new LuaException("Failed to create the Lua state");
            }
            return luaState;
        }

        public static bool TryNewMetatable(LuaState L, string typeName)
            => luaL_newmetatable(L.L, typeName) != 0;
        
        public static void NewMetatable(LuaState L, string typeName)
        {
            if (!TryNewMetatable(L, typeName)) {
                throw new LuaException($"Failed to create a new metatable with name '{typeName}'");
            }
        }

        public static bool TrySetMetatable(LuaState L, int index)
            => lua_setmetatable(L.L, index) != 0;

        public static bool TryGetMetatable(LuaState L, int index)
            => lua_getmetatable(L.L, index) != 0;
        
        public static bool TryGetMetaField(LuaState L, int index, string key)
            => luaL_getmetafield(L.L, index, key) != 0;

        /// <summary>
        /// Calls 'luaL_ref'.<br/>
        /// Creates and returns a reference, in the table at index 't', for the object at the top of the stack (and pops the object).
        /// </summary>
        public static LuaRef Ref(LuaState L, int t)
            => new LuaRef(luaL_ref(L.L, t));

        /// <summary>
        /// Calls 'luaL_unref'.<br/>
        /// Releases reference 'ref' from the table at index 't' (see luaL_ref). The entry is removed from the table, so that the referred object can be
        /// collected. The reference 'ref' is also freed to be used again.
        /// </summary>
        public static void Unref(LuaState L, int t, LuaRef @ref)
            => luaL_unref(L.L, t, @ref.Ref);

        public static LuaRef RegistryRef(LuaState L)
            => Ref(L, LUA_REGISTRYINDEX);

        public static void RegistryUnref(LuaState L, LuaRef @ref)
            => Unref(L, LUA_REGISTRYINDEX, @ref);

        public static int UpvalueIndex(int index)
            => lua_upvalueindex(index);

        public static int Yield(LuaState L, int nResults)
            => lua_yield(L.L, nResults);

        public static void Where(LuaState L, int level)
            => luaL_where(L.L, level);
        
        
        
        /* ===== Customs ===== */
        
        public static string ToStringNonNull(LuaState L, int index)
            => Lua.ToString(L, index) ?? string.Empty;
        
        public static bool TryFieldToFloat(LuaState L, int index, string name, out float f)
        {
            Lua.GetField(L, index, name);
            if (Lua.TryToFloat(L, -1, out f))
            {
                Lua.Pop(L, 1);
                return true;
            }
            Lua.Pop(L, 1);
            f = 0f;
            return false;
        }

        public static bool RawTryIToFloat(LuaState L, int index, int i, out float f)
        {
            Lua.RawGetI(L, index, i);
            if (Lua.TryToFloat(L, -1, out f))
            {
                Lua.Pop(L, 1);
                return true;
            }
            Lua.Pop(L, 1);
            f = 0f;
            return false;
        }
        
        /// <summary>
        /// Get the name of the function in the current context.
        /// </summary>
        public static string GetCurrentFunctionName(LuaState L)
        {
            lua_Debug debug = new lua_Debug();
            
            // Retrieve stack debug information in the current function
            if (lua_getstack(L.L, 1, ref debug) == 0) {
                return "?";
            }

            // Retrieve function information
            if (lua_getinfo(L.L, "n", ref debug) == 0) {
                return "?";
            }

            // Return the current function name, "?" when cannot find one
            return debug.name == IntPtr.Zero ? "?" : Marshal.PtrToStringAnsi(debug.name);
        }
        
#nullable enable
        public static BadArgException NewBadArgError(LuaState L, int index, string expected, string? actual = null)
            => new BadArgException(index, GetCurrentFunctionName(L), expected, actual);
#nullable restore

        public static BadArgException NewBadArgTypeError(LuaState L, int index, string expected)
            => NewBadArgError(L, index, expected, Lua.TypeName(L, index));



        /* ===== Helpers ===== */
        
        private static LuaStatus IntToLuaStatus(int i)
        {
#if ULUAJIT_DEBUG
            if (!Enum.IsDefined(typeof(LuaThreadStatus), i)) {
                throw new NotImplementedException($"'{nameof(LuaThreadStatus)}' member with int value of '{i}' is not defined");
            }
#endif
            return (LuaStatus)i;
        }
    }
}
