﻿using System.Runtime.InteropServices;

namespace Platform.Unsafe
{
    public static class StructureExtensions
    {
        public static byte[] ToBytes<TStruct>(this TStruct obj)
            where TStruct : struct
        {
            var structureSize = Structure<TStruct>.Size;
            var bytes = new byte[structureSize];
            var pointer = Marshal.AllocHGlobal(structureSize);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, bytes, 0, structureSize);
            Marshal.FreeHGlobal(pointer);
            return bytes;
        }
    }
}
