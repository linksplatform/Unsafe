using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents a set of extension methods for <see cref="IntPtr"/> structs.</para>
    /// <para>Представляет набор методов расширения для структур <see cref="IntPtr"/>.</para>
    /// </summary>
    public unsafe static class IntPtrExtensions
    {
        /// <summary>
        /// <para>Writes the element's <paramref name="value"/> at a position defined by the specified <paramref name="index"/> relative to the specified <paramref name="pointer"/>.</para>
        /// <para>Записывает <paramref name="value"/> элемента в позицию определяемую указанным <paramref name="index"/> относительно указанного <paramref name="pointer"/>.</para>
        /// </summary>
        /// <typeparam name="TValue"><para>The element`s value type.</para><para>Тип значения элемента.</para></typeparam>
        /// <param name="pointer"><para>The pointer.</para><para>Указатель.</para></param>
        /// <param name="index">
        /// <para>The ordinal number of an element of type <typeparamref name="TValue"/> that is used as an offset relative to <paramref name="pointer"/>. Elements are counted from zero.</para>
        /// <para>Порядковый номер элемента типа <typeparamref name="TValue"/>, который используется в качестве смещения относительно <paramref name="pointer"/>. Отсчёт элементов начинается с нуля.</para>
        /// </param>
        /// <param name="value"><para>The element's value.</para><para>Значение элемента.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteElementValue<TValue>(this IntPtr pointer, long index, TValue value) => Write((byte*)pointer + (SizeOf<TValue>() * index), value);

        /// <summary>
        /// <para>Reads the element`s <paramref name="value"/> at a position defined by the specified <paramref name="index"/> relative to the specified <paramref name="pointer"/>.</para>
        /// <para>Считывает <paramref name="value"/> элемента в позицию определяемую указанным <paramref name="index"/> относительно указанного <paramref name="pointer"/>.</para>
        /// </summary>
        /// <typeparam name="TValue"><para>The element`s value type.</para><para>Тип значение элемента.</para></typeparam>
        /// <param name="pointer"><para>The pointer.</para><para>Указатель.</para></param>
        /// <param name="index">
        /// <para>The ordinal number of an element of type <typeparamref name="TValue"/> that is used as an offset relative to <paramref name="pointer"/>. Elements are counted from zero.</para>
        /// <para>Порядковый номер элемента типа <typeparamref name="TValue"/>, который используется в качестве смещения относительно <paramref name="pointer"/>. Отсчёт элементов начинается с нуля.</para>
        /// </param>
        /// <returns><para>The eleent`s value.</para><para>Значение элемента.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue ReadElementValue<TValue>(this IntPtr pointer, long index) => Read<TValue>((byte*)pointer + (SizeOf<TValue>() * index));
    }
}
