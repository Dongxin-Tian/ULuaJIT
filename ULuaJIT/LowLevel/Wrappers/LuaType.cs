using static ULuaJIT.LowLevel.lua;

namespace ULuaJIT.LowLevel
{
    public enum LuaType : int
    {
        None          = LUA_TNONE,
        Nil           = LUA_TNIL,
        Boolean       = LUA_TBOOLEAN,
        LightUserdata = LUA_TLIGHTUSERDATA,
        Number        = LUA_TNUMBER,
        String        = LUA_TSTRING,
        Table         = LUA_TTABLE,
        Function      = LUA_TFUNCTION,
        Userdata      = LUA_TUSERDATA,
        Thread        = LUA_TTHREAD
    }

    public static class LuaTypeExts
    {
        public static int ToTypeCode(this LuaType type)
            => (int)type;
    }
}
