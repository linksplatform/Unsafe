﻿using System.Runtime.InteropServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public static class Structure<TStruct>
        where TStruct : struct
    {
        /// <summary>
        /// <para>
        /// Returns the size of an unmanaged type in bytes.
        /// This method do this without throwing exceptions for generic types as <see cref="Marshal.SizeOf{T}()"/> and <see cref="Marshal.SizeOf(System.Type)"/> do.
        /// </para>
        /// <para>
        /// Возвращает размер неуправляемого типа в байтах.
        /// Этот метод делает это без выбрасывания исключений для универсальных типов, как это делают <see cref="Marshal.SizeOf{T}()"/> и <see cref="Marshal.SizeOf(System.Type)"/>.
        /// </para>
        /// </summary>
        /// <remarks>
        /// <para>
        /// Implemented based on <see href="https://stackoverflow.com/a/18167584/710069">proposed solution</see> at StackOverflow.
        /// </para>
        /// <para>
        /// Actual differences: 
        /// <a href="https://github.com/Microsoft/referencesource/blob/f82e13c3820cd04553c21bf6da01262b95d9bd43/mscorlib/system/runtime/interopservices/marshal.cs#L202">code of</a>
        /// <see cref="Marshal.SizeOf(object)"/>
        /// and
        /// <a href="https://github.com/Microsoft/referencesource/blob/f82e13c3820cd04553c21bf6da01262b95d9bd43/mscorlib/system/runtime/interopservices/marshal.cs#L219-L222">code of</a>
        /// <see cref="Marshal.SizeOf(System.Type)"/>
        /// </para>
        /// <para>
        /// Note that this behaviour can be changed in future versions of .NET.
        /// </para>
        /// <para>
        /// Реализовано на основе <see href="https://stackoverflow.com/a/18167584/710069">предложенного решения</see> в StackOverflow.
        /// </para>
        /// <para>
        /// Фактические различия:
        /// <a href="https://github.com/Microsoft/referencesource/blob/f82e13c3820cd04553c21bf6da01262b95d9bd43/mscorlib/system/runtime/interopservices/marshal.cs#L202">код</a>
        /// <see cref="Marshal.SizeOf(object)"/>
        /// и
        /// <a href="https://github.com/Microsoft/referencesource/blob/f82e13c3820cd04553c21bf6da01262b95d9bd43/mscorlib/system/runtime/interopservices/marshal.cs#L219-L222">код</a>
        /// <see cref="Marshal.SizeOf(System.Type)"/>
        /// </para>
        /// <para>
        /// Обратите внимание, что это поведение может быть изменено в будущих версиях .NET.
        /// </para>
        /// </remarks>
        public static int Size { get; } = Marshal.SizeOf(default(TStruct));
    }
}
