﻿using System;

namespace ULuaJIT
{
    public class LuaException : Exception
    {
        public LuaException() { }

        public LuaException(string message) : base(message) { }
        
        public LuaException(string message, Exception inner) : base(message, inner) { }
    }
}
