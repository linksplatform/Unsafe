using System.Runtime.CompilerServices;
using Platform.Hardware.Cpu;

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
                obj.CopyTo(pointer, structureSize);
            }
            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<TStruct>(this ref TStruct source, void* destination)
            where TStruct : struct
        {
            var size = System.Runtime.CompilerServices.Unsafe.SizeOf<TStruct>();
            CopyTo(ref source, destination, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<TStruct>(this ref TStruct source, void* destination, int size)
            where TStruct : struct
        {
            if (CacheLine.Size >= size)
            {
                System.Runtime.CompilerServices.Unsafe.Copy(destination, ref source);
            }
            else
            {
                System.Runtime.CompilerServices.Unsafe.CopyBlock(destination, System.Runtime.CompilerServices.Unsafe.AsPointer(ref source), (uint)size);
            }
        }
    }
}
