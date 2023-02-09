using Platform.Exceptions;
using Platform.Collections;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents the byte array extensions.</para><para>Представляет расширения байтового массива.</para>
    /// </summary>
    public unsafe static class ByteArrayExtensions
    {
        /// <summary>
        /// <para>Returns the structure using the specified bytes.</para>
        /// <para>Возвращает структуру, использующую указанные байты.</para>
        /// </summary>
        /// <typeparam name="TStruct"><para>Type - structure.</para><para>Тип - структура.</para></typeparam>
        /// <param name="bytes"><para>The bytes.</para><para>Байты.</para></param>
        /// <param name="nameof(bytes)"><para>String literal.</para><para>Строковый литерал.</para></param>
        /// <param name="structure"><para>The structure.</para><para>Структура.</para></param>
        /// <returns><para>The structure.</para><para>Стурктура.</para></returns>
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
        /// <summary>
        /// <para>Checks whether the argument meets the criteria.</para><para>Проверяет, соответствует ли аргумент критериям.</para>
        /// </summary>
        /// <returns><para>Returns true if array length equal to a size of structure instance of TStruct type and false otherwise.</para><para>Возвращает true, если длина массива равна размеру экземпляра структуры типа TStruct, и false в противном случае.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasSameSizeAs<TStruct>(byte[] array) where TStruct : struct => array.Length == Structure<TStruct>.Size;
    }
}
