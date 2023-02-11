using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents a set of extension methods for strucrs.</para>
    /// <para>Представляет набор методов расширения для структур.</para>
    /// </summary>
    public unsafe static class StructureExtensions
    {
        /// <summary>
        /// <para>Returns an array of the type <typeparamref name="TStruct"/> the length of which is equal to the size of the structure.</para>
        /// <para>Возвращает массив типа <typeparamref name="TStruct"/>, длина которого равна размеру структуры.</para>
        /// </summary>
        /// <param name="obj"><para>An object of type <typeparamref name="TStruct"/>.</para><para>Объект типа <typeparamref name="TStruct"/>.</para></param>
        /// <returns><para>The <paramref name="bytes"/></para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes<TStruct>(this ref TStruct obj)
            where TStruct : struct
        {
            var bytes = new byte[Structure<TStruct>.Size];
            fixed (byte* pointer = bytes)
            {
                Copy(pointer, ref obj);
            }
            return bytes;
        }
    }
}
