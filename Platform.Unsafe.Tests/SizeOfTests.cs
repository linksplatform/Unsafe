using System.Runtime.InteropServices;
using Xunit;

namespace Platform.Unsafe.Tests
{
    public static class SizeOfTests
    {
        public struct X<T>
        {
            public readonly T F1;
            public readonly T F2;
        }

        [Fact]
        public static void UnsafeClassSizeOfTest()
        {
            var size = System.Runtime.CompilerServices.Unsafe.SizeOf<X<int>>();
            Assert.Equal(8, size);
        }

        [Fact]
        public static void MarshalSizeOfTest()
        {
            var size = Marshal.SizeOf(default(X<int>));
            Assert.Equal(8, size);
        }

        [Fact]
        public static void StructurePropertyTest()
        {
            var size = Structure<X<int>>.Size;
            Assert.Equal(8, size);
        }
    }
}
