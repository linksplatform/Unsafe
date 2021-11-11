using Platform.Exceptions;
using Platform.Collections;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>
    /// Represents the byte array extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public unsafe static class ByteArrayExtensions
    {
        /// <summary>
        /// <para>
        /// Returns the structure using the specified bytes.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TStruct">
        /// <para>The struct.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="bytes">
        /// <para>The bytes.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The structure.</para>
        /// <para></para>
        /// </returns>
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
