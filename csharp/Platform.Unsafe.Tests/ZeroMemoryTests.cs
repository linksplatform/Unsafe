using Xunit;

namespace Platform.Unsafe.Tests
{
    /// <summary>
    /// <para>
    /// Represents the zero memory tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static unsafe class ZeroMemoryTests
    {
        /// <summary>
        /// <para>
        /// Tests that zero memory test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ZeroMemoryTest()
        {
            var bytes = new byte[1024];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = unchecked((byte)i);
            }
            fixed (byte* pointer = bytes)
            {
                MemoryBlock.Zero(pointer, bytes.Length);
            }
            for (int i = 0; i < bytes.Length; i++)
            {
                Assert.Equal(0, bytes[i]);
            }
        }
    }
}
