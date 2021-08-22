using System;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>
    /// Represents the int ptr extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public unsafe static class IntPtrExtensions
    {
        /// <summary>
        /// <para>
        /// Writes the element value using the specified pointer.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TValue">
        /// <para>The value.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="pointer">
        /// <para>The pointer.</para>
        /// <para></para>
        /// </param>
        /// <param name="index">
        /// <para>The index.</para>
        /// <para></para>
        /// </param>
        /// <param name="value">
        /// <para>The value.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteElementValue<TValue>(this IntPtr pointer, long index, TValue value) => Write((byte*)pointer + (SizeOf<TValue>() * index), value);

        /// <summary>
        /// <para>
        /// Reads the element value using the specified pointer.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TValue">
        /// <para>The value.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="pointer">
        /// <para>The pointer.</para>
        /// <para></para>
        /// </param>
        /// <param name="index">
        /// <para>The index.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The value</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue ReadElementValue<TValue>(this IntPtr pointer, long index) => Read<TValue>((byte*)pointer + (SizeOf<TValue>() * index));
    }
}
