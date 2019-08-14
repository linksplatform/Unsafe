using System.Runtime.InteropServices;

namespace Platform.Unsafe
{
    public static class StructureExtensions
    {
        public static byte[] ToBytes<TStruct>(this TStruct obj)
            where TStruct : struct
        {
            var structureSize = Structure.SizeOf<TStruct>();
            var bytes = new byte[structureSize];
            var pointer = Marshal.AllocHGlobal(structureSize);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, bytes, 0, structureSize);
            Marshal.FreeHGlobal(pointer);
            return bytes;
        }
    }
}
