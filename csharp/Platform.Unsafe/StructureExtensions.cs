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
        /// <para>Converts a <typeparamref name="TStruct"/> instance into an array of the same size.</para>
        /// <para>Преобразует экземпляр <typeparamref name="TStruct"/> в массив того же размера.</para>
        /// </summary>
        /// <param name="obj">
        /// <para>A structure instance of type <typeparamref name="TStruct"/>.</para>
        /// <para>Экземпляр структуры типа <typeparamref name="TStruct"/>.</para>
        /// </param>
        /// <returns>
        /// <para>The bytes whose source is <paramref name="obj"/> (the instance of <typeparamref name="TStruct"/> structure).</para>
        /// <para>Байты, источником которых является <paramref name="obj"/> (экземпляр структуры <typeparamref name="TStruct"/>).</para>
        /// </returns>
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
