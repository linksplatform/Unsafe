using Xunit;

namespace Platform.Unsafe.Tests
{
    public static class StructAndBytesConversionTests
    {
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

        [Fact]
        public static void BytesToStructTest()
        {
            byte[] bytes = new[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue };
            ulong result = bytes.ToStructure<ulong>();
            Assert.Equal(ulong.MaxValue, result);
        }
    }
}
