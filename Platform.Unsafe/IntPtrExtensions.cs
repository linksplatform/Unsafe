﻿using System;
using System.Runtime.CompilerServices;
using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <remarks>
    /// Please use System.Runtime.CompilerServices.Unsafe instead.
    /// </remarks>
    public unsafe static class IntPtrExtensions
    {
        [Obsolete("GetValue method is deprecated, please use System.Runtime.CompilerServices.Unsafe.Read method instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TElement GetValue<TElement>(this IntPtr pointer) => IntPtr<TElement>.GetValue(pointer);

        [Obsolete("SetValue method is deprecated, please use System.Runtime.CompilerServices.Unsafe.Write method instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<TElement>(this IntPtr pointer, TElement value) => IntPtr<TElement>.SetValue(pointer, value);

        [Obsolete("GetElement method is deprecated, please use System.Runtime.CompilerServices.Unsafe.Add method instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetElement(this IntPtr pointer, int elementSize, int index) => pointer + (elementSize * index);

        [Obsolete("GetElement method is deprecated, please use System.Runtime.CompilerServices.Unsafe.Add method instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetElement(this IntPtr pointer, long elementSize, long index) => new IntPtr((byte*)pointer.ToPointer() + (elementSize * index));

        [Obsolete("GetElement method is deprecated, please use System.Runtime.CompilerServices.Unsafe.Add method instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetElement<TIndex>(this IntPtr pointer, int elementSize, TIndex index) => pointer.GetElement((long)elementSize, (Integer)(Integer<TIndex>)index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteElementValue<TValue>(this IntPtr pointer, long index, TValue value) => System.Runtime.CompilerServices.Unsafe.Write((byte*)pointer + (System.Runtime.CompilerServices.Unsafe.SizeOf<TValue>() * index), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue ReadElementValue<TValue>(this IntPtr pointer, long index) => System.Runtime.CompilerServices.Unsafe.Read<TValue>((byte*)pointer + (System.Runtime.CompilerServices.Unsafe.SizeOf<TValue>() * index));
    }
}