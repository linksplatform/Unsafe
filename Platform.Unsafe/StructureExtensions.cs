using System.Runtime.CompilerServices;
using Platform.Hardware.Cpu;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public unsafe static class StructureExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes<TStruct>(this ref TStruct obj)
            where TStruct : struct
        {
            var structureSize = SizeOf<TStruct>();
            var bytes = new byte[structureSize];
            fixed (byte* pointer = bytes)
            {
                obj.CopyTo(pointer, structureSize);
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<TStruct>(this ref TStruct source, void* destination)
            where TStruct : struct
        {
            var size = SizeOf<TStruct>();
            CopyTo(ref source, destination, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<TStruct>(this ref TStruct source, void* destination, int size)
            where TStruct : struct
        {
            if (CacheLine.Size >= size)
            {
                Copy(destination, ref source);
            }
            else
            {
                CopyBlock(destination, AsPointer(ref source), (uint)size);
            }
        }
    }
}
