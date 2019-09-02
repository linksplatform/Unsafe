using System;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;
using Platform.Diagnostics;

namespace Platform.Unsafe.Tests
{
    public unsafe class IntPtrExtensionsTests
    {
        private const int N = 10000000;

        private readonly ITestOutputHelper _output;

        public IntPtrExtensionsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ReadAndWriteOperationsForPointerValuesDelegatesTest()
        {
            var pointer = Marshal.AllocHGlobal(sizeof(ulong));
            ulong result = default;
            for (var i = 0; i < N; i++)
            {
                result = Delegates(pointer);
            }
            Assert.Equal(42UL, result);
            Marshal.FreeHGlobal(pointer);
        }

        private static ulong Delegates(IntPtr pointer)
        {
            ulong result;
            IntPtr<ulong>.SetValue(pointer, 42UL);
            result = IntPtr<ulong>.GetValue(pointer);
            return result;
        }

        [Fact]
        public void ReadAndWriteOperationsForPointerValuesExtensionMethodsTest()
        {
            var pointer = Marshal.AllocHGlobal(sizeof(ulong));
            ulong result = default;
            for (var i = 0; i < N; i++)
            {
                result = ExtensionMethods(pointer);
            }
            Assert.Equal(42UL, result);
            Marshal.FreeHGlobal(pointer);
        }

        private static ulong ExtensionMethods(IntPtr pointer)
        {
            ulong result;
            pointer.SetValue(42UL);
            result = pointer.GetValue<ulong>();
            return result;
        }

        [Fact]
        public void ReadAndWriteOperationsForPointerValuesUnsafeClassMethodsTest()
        {
            void* pointer = (void*)Marshal.AllocHGlobal(sizeof(ulong));
            ulong result = default;
            for (var i = 0; i < N; i++)
            {
                result = ReadAndWriteMethods(pointer);
            }
            Assert.Equal(42UL, result);
            Marshal.FreeHGlobal((IntPtr)pointer);
        }

        private static ulong ReadAndWriteMethods(void* pointer)
        {
            ulong result;
            System.Runtime.CompilerServices.Unsafe.Write(pointer, 42UL);
            result = System.Runtime.CompilerServices.Unsafe.Read<ulong>(pointer);
            return result;
        }

        [Fact]
        public void ReadAndWriteOperationsComparisionTest()
        {
            var t1 = Performance.Measure(ReadAndWriteOperationsForPointerValuesDelegatesTest);
            var t2 = Performance.Measure(ReadAndWriteOperationsForPointerValuesExtensionMethodsTest);
            var t3 = Performance.Measure(ReadAndWriteOperationsForPointerValuesUnsafeClassMethodsTest);
            var message = $"{t1} {t2} {t3}";
            _output.WriteLine(message);
        }

        [Fact]
        public void ElementOffsetOperationsForPointerValuesExtensionMethods()
        {
            var pointer = Marshal.AllocHGlobal(sizeof(ulong) * 10);
            ulong result = default;
            for (var i = 0; i < N; i++)
            {
                result = GetElementExtensionMethods(pointer);
            }
            Assert.Equal(5UL * 8UL, result - (ulong)pointer);
            Marshal.FreeHGlobal(pointer);
        }

        private static ulong GetElementExtensionMethods(IntPtr pointer)
        {
            ulong result;
            result = (ulong)pointer.GetElement(8, 5);
            return result;
        }

        [Fact]
        public void ElementOffsetOperationsForPointerValuesUnsafeClassMethodsTest()
        {
            void* pointer = (void*)Marshal.AllocHGlobal(sizeof(ulong) * 10);
            ulong result = default;
            for (var i = 0; i < N; i++)
            {
                result = GetElementMethods(pointer);
            }
            Assert.Equal(5UL * 8UL, result - (ulong)pointer);
            Marshal.FreeHGlobal((IntPtr)pointer);
        }

        private static ulong GetElementMethods(void* pointer)
        {
            ulong result;
            result = (ulong)System.Runtime.CompilerServices.Unsafe.Add<ulong>(pointer, 5);
            return result;
        }

        [Fact]
        public void GetElementOperationsComparisionTest()
        {
            var t1 = Performance.Measure(ElementOffsetOperationsForPointerValuesExtensionMethods);
            var t2 = Performance.Measure(ElementOffsetOperationsForPointerValuesUnsafeClassMethodsTest);
            var message = $"{t1} {t2}";
            _output.WriteLine(message);
        }
    }
}