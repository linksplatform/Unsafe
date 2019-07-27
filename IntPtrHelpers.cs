using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Platform.Reflection;
using Platform.Reflection.Sigil;

namespace Platform.Unsafe
{
    public static class IntPtrHelpers<T>
    {
        public static readonly Func<IntPtr, T> GetValue;
        public static readonly Action<IntPtr, T> SetValue;

        static IntPtrHelpers()
        {
            GetValue = DelegateHelpers.Compile<Func<IntPtr, T>>(emiter =>
            {
                if (CachedTypeInfo<T>.IsNumeric)
                {
                    emiter.LoadArgument(0);
                    emiter.LoadIndirect<T>();
                    emiter.Return();
                }
                else
                {
                    emiter.LoadArguments(0);
                    emiter.Call(typeof(Marshal).GetGenericMethod("PtrToStructure", Types.Get<T>().ToArray(), Types<IntPtr, Type, bool>.List.ToArray()));
                    emiter.Return();
                }
            });
            SetValue = DelegateHelpers.Compile<Action<IntPtr, T>>(emiter =>
            {
                if (CachedTypeInfo<T>.IsNumeric)
                {
                    emiter.LoadArguments(0, 1);
                    emiter.StoreIndirect<T>();
                    emiter.Return();
                }
                else
                {
                    emiter.LoadArguments(0, 1);
                    emiter.LoadConstant(true);
                    emiter.Call(typeof(Marshal).GetTypeInfo().GetMethod("StructureToPtr", Types<object, IntPtr, bool>.List.ToArray()));
                    emiter.Return();
                }
            });
        }
    }
}
