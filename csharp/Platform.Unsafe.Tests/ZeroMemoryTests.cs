using System;
using Xunit;

namespace Platform.Unsafe.Tests
{
    public static unsafe class ZeroMemoryTests
    {
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

        [Fact]
        public static void PhysicalCoreCountTest()
        {
            // Test that physical core count is reasonable
            var physicalCores = MemoryBlock.PhysicalCoreCount;
            var logicalProcessors = Environment.ProcessorCount;
            
            // Physical cores should be at least 1
            Assert.True(physicalCores >= 1, $"Physical cores should be at least 1, got {physicalCores}");
            
            // Physical cores should not exceed logical processors
            Assert.True(physicalCores <= logicalProcessors, 
                $"Physical cores ({physicalCores}) should not exceed logical processors ({logicalProcessors})");
            
            // On most systems, physical cores should be at least half of logical processors
            // (allowing for hyper-threading)
            var expectedMinimum = Math.Max(1, logicalProcessors / 2);
            Assert.True(physicalCores >= expectedMinimum, 
                $"Physical cores ({physicalCores}) should be at least {expectedMinimum}");
        }
    }
}
