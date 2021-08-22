using System.Runtime.InteropServices;
using Xunit;

namespace Platform.Unsafe.Tests
{
    /// <summary>
    /// <para>
    /// Represents the size of tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class SizeOfTests
    {
        /// <summary>
        /// <para>
        /// The .
        /// </para>
        /// <para></para>
        /// </summary>
        public struct X<T>
        {
            /// <summary>
            /// <para>
            /// The .
            /// </para>
            /// <para></para>
            /// </summary>
            public readonly T F1;
            /// <summary>
            /// <para>
            /// The .
            /// </para>
            /// <para></para>
            /// </summary>
            public readonly T F2;
        }

        /// <summary>
        /// <para>
        /// Tests that unsafe class size of test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void UnsafeClassSizeOfTest()
        {
            var size = System.Runtime.CompilerServices.Unsafe.SizeOf<X<int>>();
            Assert.Equal(8, size);
        }

        /// <summary>
        /// <para>
        /// Tests that marshal size of test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void MarshalSizeOfTest()
        {
            var size = Marshal.SizeOf(default(X<int>));
            Assert.Equal(8, size);
        }

        /// <summary>
        /// <para>
        /// Tests that structure property test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void StructurePropertyTest()
        {
            var size = Structure<X<int>>.Size;
            Assert.Equal(8, size);
        }
    }
}
