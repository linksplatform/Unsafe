using Platform.Exceptions;
using Platform.Collections;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents an extension of an array of bytes of the type <typeparamref name="TStruct"/>.</para>
    /// <para>Представляет расширения массивa байтов типа <typeparamref name="TStruct"/>.</para>
    /// </summary>
    public unsafe static class ByteArrayExtensions
    {
        /// <summary>
        /// <para>Returns <paramref name="structure"/> when pinned <paramref name="pointer"/> equal to the specified <paramref name="bytes"/> so that it does not move when copying.</para>
        /// <para>Возвращает <paramref name="structure"/> при закреплённом <paramref name="pointer"/> равный указанным <paramref name="bytes"/>, чтобы не он перемещался при копировании.</para>
        /// </summary>
        /// <typeparam name="TStruct"><para>The element`s structure type.</para><para>Тип структуры элемента.</para></typeparam>
        /// <param name="bytes"><para>The bytes.</para><para>Байты.</para></param>
        /// <param name="nameof(bytes)"><para>The string literal <paramref name="bytes"/>.</para><para>Строковый литерал <paramref name="bytes"/>.</para></param>
        /// <param name="structure"><para>An array of bytes of the type <typeparamref name="TStruct"/>.</para><para>Массив байтов типа <typeparamref name="TStruct"/>.</para></param>
        /// <returns><para>The <paramref name="structure"/>.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TStruct ToStructure<TStruct>(this byte[] bytes)
            where TStruct : struct
        {
            Ensure.OnDebug.ArgumentNotEmpty(bytes, nameof(bytes));
            Ensure.OnDebug.ArgumentMeetsCriteria(bytes, HasSameSizeAs<TStruct>, nameof(bytes), "Bytes array should be the same length as struct size.");
            TStruct structure = default;
            fixed (byte* pointer = bytes)
            {
                Copy(ref structure, pointer);
            }
            return structure;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasSameSizeAs<TStruct>(byte[] array) where TStruct : struct => array.Length == Structure<TStruct>.Size;
    }
}
