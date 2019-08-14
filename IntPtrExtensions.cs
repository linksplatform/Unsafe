using System;
using System.Runtime.CompilerServices;
using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public static class IntPtrExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TElement GetValue<TElement>(this IntPtr pointer) => IntPtr<TElement>.GetValue(pointer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<TElement>(this IntPtr pointer, TElement value) => IntPtr<TElement>.SetValue(pointer, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetElement(this IntPtr pointer, int elementSize, int index) => pointer + (elementSize * index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe IntPtr GetElement(this IntPtr pointer, long elementSize, long index) => new IntPtr((byte*)pointer.ToPointer() + (elementSize * index));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr GetElement<TIndex>(this IntPtr pointer, int elementSize, TIndex index) => pointer.GetElement((long)elementSize, (Integer)(Integer<TIndex>)index);
    }
}
