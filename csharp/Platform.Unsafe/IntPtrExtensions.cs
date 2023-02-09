using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents the IntPtr extensions.</para><para>Представляет расширения IntPtr.</para>
    /// </summary>
    public unsafe static class IntPtrExtensions
    {
        /// <summary>
        /// <para>Writes the element value using the specified pointer.</para><para>Записывает значение элемента, используя заданный указатель.</para>
        /// </summary>
        /// <typeparam name="TValue"><para>The value type.</para><para>Тип элемента.</para></typeparam>
        /// <param name="pointer"><para>The pointer.</para><para>Указатель.</para></param>
        /// <param name="index"><para>The index.</para><para>Индекс.</para></param>
        /// <param name="value"><para>The value.</para><para>Значение элемента.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteElementValue<TValue>(this IntPtr pointer, long index, TValue value) => Write((byte*)pointer + (SizeOf<TValue>() * index), value);

        /// <summary>
        /// <para>Reads the element value using the specified pointer.</para><para>Считывает значение элемента, используя данный указатель.</para>
        /// </summary>
        /// <typeparam name="TValue"><para>The value type.</para><para>Тип элемента.</para></typeparam>
        /// <param name="pointer"><para>The pointer.</para><para>Указатель.</para></param>
        /// <param name="index"><para>The index.</para><para>Индекс.</para></param>
        /// <returns><para>The value.</para><para>Значение элемента.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue ReadElementValue<TValue>(this IntPtr pointer, long index) => Read<TValue>((byte*)pointer + (SizeOf<TValue>() * index));
    }
}
