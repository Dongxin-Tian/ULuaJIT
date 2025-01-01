# ULuaJIT

*ULuaJIT* is a binding library for using *LuaJIT* in Unity with C#. It contains direct wrappers of *LuaJIT* C API functions through P/Invoke in static classes like `lua`, `lualib`, `lauxlib` and `luajit`, but they can be unsafe to use in C# since the C API uses long jumps to handle Lua errors. So managed wrappers of the *LuaJIT* C API are also included. They are provided with "safer" reimplementations of some *LuaJIT* C API functions (including `Lua.Error`, `Lua.CheckAny`, `Lua.OptInteger`, etc.). They are designed to match C#'s naming conventions and styles better while aligning with their *LuaJIT* C API counterparts (in terms of usage and performance. Most functions are thin wrappers of the *LuaJIT* C API functions). They are much safer to use in C# but can still result in undefined behaviors if used improperly.

There's also a highly abstract class named `LuaRuntime` that provides a more managed and safer interface for interacting with the *LuaJIT* runtime but at the cost of some performance.

In addition, *ULuaJIT* also provided some extra or extended functionalities like *managed registry* (wrapping C# managed objects to *Lua* userdata without pinning the C# object in GC), etc. in the `ULuaJIT.LowLevel.Extended` namespace. A subclass of `LuaRuntime` named `LuaXRuntime` is also provided.

This library is currently developed as an extension (or separation) from a game named *Touhou Shinshuuyume*'s *LuaJIT* interface. Feel free to use it in your projects.
