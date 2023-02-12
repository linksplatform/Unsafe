using Platform.Exceptions;
using Platform.Collections;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents a set of extension methods for byte arrays.</para>
    /// <para>Представляет набор методов расширения массивов байт.</para>
    /// </summary>
    public unsafe static class ByteArrayExtensions
    {
        /// <summary>
        /// <para>Returns <paramref name="structure"/> if the length of the byte array with the specified <paramref name="bytes"/> is equal to the size of the structure.</para>
        /// <para>Возвращает <paramref name="structure"/>, если длина байтового массива при указанных <paramref name="bytes"/> равна размеру структуры.</para>
        /// </summary>
        /// <typeparam name="TStruct"><para>The element's structure type.</para><para>Тип структуры элемента.</para></typeparam>
        /// <param name="bytes">
        /// <para>An array of bytes of type <typeparamref name="TStruct"/>.</para>
        /// <para>Массив байтов типа <typeparamref name="TStruct"/>.</para>
        /// </param>
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
