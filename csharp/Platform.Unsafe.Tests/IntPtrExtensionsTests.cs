using System;
using System.Runtime.InteropServices;
using Xunit;
using static System.Runtime.CompilerServices.Unsafe;

namespace Platform.Unsafe.Tests
{
    /// <summary>
    /// <para>
    /// Represents the int ptr extensions tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public unsafe class IntPtrExtensionsTests
    {
        private methods test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void ReadAndWriteOperationsForPointerValuesUnsafeClassMethodsTest()
        {
            void* pointer = (void*)Marshal.AllocHGlobal(sizeof(ulong));
            Write(pointer, 42UL);
            Assert.Equal(42UL, Read<ulong>(pointer));
            Marshal.FreeHGlobal((IntPtr)pointer);
        }

        /// <summary>
        /// <para>
        /// Tests that element offset operations for pointer values test.
        /// </para>
        /// <para></para>
        /// </summary>
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