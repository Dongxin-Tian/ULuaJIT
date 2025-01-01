#nullable enable

namespace ULuaJIT
{
    public sealed class BadArgException : LuaException
    {
        public int Index { get; set; }
        public string FuncName { get; set; }
        public string Expected { get; set; }
        public string? Actual { get; set; }

        public BadArgException(int index, string funcName, string expected, string? actual = null)
        {
            Index = index;
            FuncName = funcName;
            Expected = expected;
            Actual = actual;
        }
        
        public override string Message => GenerateMessage();

        private string GenerateMessage()
        {
            return Actual is null ? $"bad argument #{Index} to '{FuncName}' ({Expected} expected)"
                                  : $"bad argument #{Index} to '{FuncName}' ({Expected} expected, got {Actual})";
        }
    }
}
