using Platform.Exceptions;
using Platform.Collections;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public unsafe static class ByteArrayExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TStruct ToStructure<TStruct>(this byte[] bytes)
            where TStruct : struct
        {
            Ensure.OnDebug.ArgumentNotEmpty(bytes, nameof(bytes));
            var structureSize = System.Runtime.CompilerServices.Unsafe.SizeOf<TStruct>();
            Ensure.OnDebug.ArgumentMeetsCriteria(bytes, array => array.Length == structureSize, nameof(bytes), "Bytes array should be the same length as struct size.");
            TStruct structure = default;
            fixed (byte* pointer = bytes)
            {
                System.Runtime.CompilerServices.Unsafe.Copy(ref structure, pointer);
            }
            return structure;
        }
    }
}
