using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public unsafe static class StructureExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes<TStruct>(this ref TStruct obj)
            where TStruct : struct
        {
            var structureSize = System.Runtime.CompilerServices.Unsafe.SizeOf<TStruct>();
            var bytes = new byte[structureSize];
            fixed (byte* pointer = bytes)
            {
                System.Runtime.CompilerServices.Unsafe.Copy(pointer, ref obj);
            }
            return bytes;
        }
    }
}
