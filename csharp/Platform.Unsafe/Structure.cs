using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents a set of helper properties for structures of type <typeparamref name="TStruct"/>.</para>
    /// <para>Представляет набор вспомогательных свойств для структур типа <typeparamref name="TStruct"/>.</para>
    /// </summary>
    public static class Structure<TStruct>
        where TStruct : struct
    {
        /// <summary>
        /// <para>
        /// Returns the size of an unmanaged type in bytes.
        /// This property do this without throwing exceptions for generic types as <see cref="Marshal.SizeOf{T}()"/> and <see cref="Marshal.SizeOf(Type)"/> do.
        /// </para>
        /// <para>
        /// Возвращает размер неуправляемого типа в байтах.
        /// Это свойство делает это без выбрасывания исключений для универсальных типов, как это делают <see cref="Marshal.SizeOf{T}()"/> и <see cref="Marshal.SizeOf(Type)"/>.
        /// </para>
        /// </summary>
        public static int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        } = SizeOf<TStruct>();
    }
}
