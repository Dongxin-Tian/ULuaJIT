using System;
using System.Collections.Generic;

namespace ULuaJIT.LowLevel.Extended
{
    public sealed class ManagedRegistry : IManagedRegistry
    {
        private readonly List<object> list = new List<object>();
        private readonly Queue<int> reusableIndexQueue = new Queue<int>();

        public ManagedRef Ref(object obj)
        {
            if (reusableIndexQueue.TryDequeue(out int i))
            {
                list[i] = obj;
                return new ManagedRef(i);
            }
            
            ManagedRef mf = new ManagedRef(list.Count);
            list.Add(obj);
            return mf;
        }

        public void Unref(ManagedRef mf)
        {
            int i = mf.Ref;
            if (i < 0 || i >= list.Count) {
                throw new ArgumentException("Attempting to unref an invalid managed reference", nameof(mf));
            }
            
            list[i] = null;
            reusableIndexQueue.Enqueue(i);
        }

        public object Get(ManagedRef mf)
        {
            int i = mf.Ref;
            if (i >= 0 && i < list.Count)
            {
                object obj = list[i];
                if (obj is not null) {
                    return obj;
                }
            }
            throw new ArgumentException("Attempting to get from an invalid managed reference", nameof(mf));
        }
    }
}
