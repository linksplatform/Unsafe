using System;
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
            GetValue = CompileGetValueDelegate();
            SetValue = CompileSetValueDelegate();
        }

        static private Func<IntPtr, T> CompileGetValueDelegate()
        {
            return DelegateHelpers.Compile<Func<IntPtr, T>>(emiter =>
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
                    emiter.Call(typeof(Marshal).GetGenericMethod(nameof(Marshal.PtrToStructure), Types<T>.Array, Types<IntPtr, Type, bool>.Array));
                    emiter.Return();
                }
            });
        }

        static private Action<IntPtr, T> CompileSetValueDelegate()
        {
            return DelegateHelpers.Compile<Action<IntPtr, T>>(emiter =>
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
                    emiter.Call(typeof(Marshal).GetTypeInfo().GetMethod(nameof(Marshal.StructureToPtr), Types<object, IntPtr, bool>.Array));
                    emiter.Return();
                }
            });
        }
    }
}
