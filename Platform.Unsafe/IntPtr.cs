using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Platform.Reflection;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <remarks>
    /// Please use System.Runtime.CompilerServices.Unsafe instead.
    /// </remarks>
    [Obsolete("Please use System.Runtime.CompilerServices.Unsafe instead.")]
    public static class IntPtr<T>
    {
        public static readonly Func<IntPtr, T> GetValue = CompileGetValueDelegate();
        public static readonly Action<IntPtr, T> SetValue = CompileSetValueDelegate();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static private Func<IntPtr, T> CompileGetValueDelegate()
        {
            return DelegateHelpers.Compile<Func<IntPtr, T>>(emiter =>
            {
                if (NumericType<T>.IsNumeric)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static private Action<IntPtr, T> CompileSetValueDelegate()
        {
            return DelegateHelpers.Compile<Action<IntPtr, T>>(emiter =>
            {
                if (NumericType<T>.IsNumeric)
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
