using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>
    /// this process does something
    /// </para>
    /// <para>
    /// этот процесс что-то делает
    /// </para>
    /// </summary>
    public unsafe static class StructureExtensions
    {
        /// <summary>
        /// <para>
        /// this process does something
        /// </para>
        /// <para>
        /// этот процесс что-то делает
        /// </para>
        /// </summary>
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
