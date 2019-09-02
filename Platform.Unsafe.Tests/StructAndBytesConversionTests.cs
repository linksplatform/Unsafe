using Xunit;

namespace Platform.Unsafe.Tests
{
    public class StructAndBytesConversionTests
    {
        [Fact]
        public void StructToBytesTest()
        {
            ulong source = ulong.MaxValue;
            var result = source.ToBytes();
            for (int i = 0; i < result.Length; i++)
            {
                Assert.Equal(byte.MaxValue, result[i]);
            }
        }

        [Fact]
        public void BytesToStructTest()
        {
            byte[] bytes = new[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue };
            ulong result = bytes.ToStructure<ulong>();
            Assert.Equal(ulong.MaxValue, result);
        }
    }
}
