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
        /// <para>Converts an array <paramref name="bytes"/> into a structure of type <typeparamref name="TStruct"/>.</para>
        /// <para>Преобразует массив <paramref name="bytes"/> в структуру типа <typeparamref name="TStruct"/>.</para>
        /// </summary>
        /// <typeparam name="TStruct"><para>The element's structure type.</para><para>Тип структуры элемента.</para></typeparam>
        /// <param name="bytes">
        /// <para>An array of bytes that will be converted to <typeparamref name="TStruct"/> type.</para>
        /// <para>Массив байтов, который будет преобразован в тип <typeparamref name="TStruct"/>.</para>
        /// </param>
        /// <exeption cref="ArgumentNullException">
        /// <para>Thrown when <paramref name="bytes"/> array is empty.</para>
        /// <para>Выбрасывается, когда массив <paramref name="bytes"/> пустой.</para>
        /// </exeption>
        /// <exeption cref="ArgumentExeption">
        /// <para>Thrown when the length of the <paramref name="bytes"/> array is not the same as the size of the <typeparamref name="TStruct"/>.</para>
        /// <para>Выбрасывается, когда длина массива <paramref name="bytes"/> не совпадает с размером <typeparamref name="TStruct"/>.</para>
        /// </exeption>
        /// <returns><para>The structure.</para><para>Структуру.</para></returns>
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
