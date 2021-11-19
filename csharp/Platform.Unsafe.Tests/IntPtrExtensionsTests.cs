using System;
using System.Runtime.InteropServices;
using Xunit;
using static System.Runtime.CompilerServices.Unsafe;

namespace Platform.Unsafe.Tests
{
    public unsafe class IntPtrExtensionsTests
    {
        [Fact]
        public void ReadAndWriteOperationsForPointerValuesUnsafeClassMethodsTest()
        {
            void* pointer = (void*)Marshal.AllocHGlobal(sizeof(ulong));
            Write(pointer, 42UL);
            Assert.Equal(42UL, Read<ulong>(pointer));
            Marshal.FreeHGlobal((IntPtr)pointer);
        }

        [Fact]
        public void ElementOffsetOperationsForPointerValuesTest()
        {
            void* pointer = (void*)Marshal.AllocHGlobal(sizeof(ulong) * 10);
            ulong result = (ulong)Add<ulong>(pointer, 5);
            Assert.Equal(5UL * 8UL, result - (ulong)pointer);
            Marshal.FreeHGlobal((IntPtr)pointer);
        }
    }
}