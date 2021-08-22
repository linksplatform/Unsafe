using Xunit;

namespace Platform.Unsafe.Tests
{
    /// <summary>
    /// <para>
    /// Represents the struct and bytes conversion tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class StructAndBytesConversionTests
    {
        /// <summary>
        /// <para>
        /// Tests that struct to bytes test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void StructToBytesTest()
        {
            ulong source = ulong.MaxValue;
            var result = source.ToBytes();
            for (int i = 0; i < result.Length; i++)
            {
                Assert.Equal(byte.MaxValue, result[i]);
            }
        }

        /// <summary>
        /// <para>
        /// Tests that bytes to struct test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BytesToStructTest()
        {
            byte[] bytes = new[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue };
            ulong result = bytes.ToStructure<ulong>();
            Assert.Equal(ulong.MaxValue, result);
        }
    }
}
