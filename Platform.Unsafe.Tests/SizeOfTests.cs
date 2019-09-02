using System.Runtime.InteropServices;
using Xunit;

namespace Platform.Unsafe.Tests
{
    public class SizeOfTests
    {
        public struct X<T>
        {
            public readonly T F1;
            public readonly T F2;
        }

        [Fact]
        public void UnsafeClassSizeOfTest()
        {
            var size = System.Runtime.CompilerServices.Unsafe.SizeOf<X<int>>();
            Assert.Equal(8, size);
        }

        [Fact]
        public void MarshalSizeOfTest()
        {
            var size = Marshal.SizeOf(default(X<int>));
            Assert.Equal(8, size);
        }

        [Fact]
        public void StructurePropertyTest()
        {
            var size = Structure<X<int>>.Size;
            Assert.Equal(8, size);
        }
    }
}
