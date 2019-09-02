using System.Runtime.InteropServices;
using Platform.Exceptions;
using Platform.Collections;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public static class ByteArrayExtensions
    {
        public static TTStruct ToStructure<TTStruct>(this byte[] bytes)
            where TTStruct : struct
        {
            Ensure.Always.ArgumentNotEmpty(bytes, nameof(bytes));
            var structureSize = Structure<TTStruct>.Size;
            Ensure.Always.ArgumentMeetsCriteria(bytes, array => array.Length == structureSize, nameof(bytes), "Bytes array should be the same length as struct size.");
            var pointer = Marshal.AllocHGlobal(structureSize);
            Marshal.Copy(bytes, 0, pointer, structureSize);
            var structure = Marshal.PtrToStructure<TTStruct>(pointer);
            Marshal.FreeHGlobal(pointer);
            return structure;
        }
    }
}
