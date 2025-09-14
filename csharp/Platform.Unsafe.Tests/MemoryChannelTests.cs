using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Platform.Unsafe.Tests
{
    public static class MemoryChannelTests
    {
        [Fact]
        public static void MemoryChannelCountIsValid()
        {
            // The detected memory channel count should be at least 1 and reasonable
            var channelCount = MemoryBlock.MemoryChannelCount;
            
            Assert.True(channelCount >= 1, $"Memory channel count should be at least 1, got {channelCount}");
            Assert.True(channelCount <= 16, $"Memory channel count seems unreasonable, got {channelCount}");
        }
        
        [Fact]
        public static void MemoryChannelCountIsConsistent()
        {
            // The detected memory channel count should be consistent across multiple calls
            var channelCount1 = MemoryBlock.MemoryChannelCount;
            var channelCount2 = MemoryBlock.MemoryChannelCount;
            
            Assert.Equal(channelCount1, channelCount2);
        }
        
        [Fact]
        public static void MemoryChannelCountRespectsProcessorLimit()
        {
            // The memory channel count should not exceed max(1, Environment.ProcessorCount / 2)
            var channelCount = MemoryBlock.MemoryChannelCount;
            var maxExpectedChannels = Math.Max(1, Environment.ProcessorCount / 2);
            
            Assert.True(channelCount <= maxExpectedChannels, 
                $"Memory channel count ({channelCount}) should not exceed max(1, ProcessorCount/2) ({maxExpectedChannels})");
        }
        
        [Fact]
        public static void ZeroMemoryWithDetectedChannels()
        {
            // Test that memory zeroing works correctly with the detected channel count
            var bytes = new byte[4096]; // Larger buffer to benefit from parallelization
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = unchecked((byte)(i % 256));
            }
            
            unsafe
            {
                fixed (byte* pointer = bytes)
                {
                    MemoryBlock.Zero(pointer, bytes.Length);
                }
            }
            
            for (int i = 0; i < bytes.Length; i++)
            {
                Assert.Equal(0, bytes[i]);
            }
        }
        
        [Fact]
        public static void DefaultChannelCountForNonWindows()
        {
            // On non-Windows platforms, we should get the default behavior
            // This is more of a behavioral test since we can't easily mock the platform
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var channelCount = MemoryBlock.MemoryChannelCount;
                var maxExpectedChannels = Math.Max(1, Environment.ProcessorCount / 2);
                // Should fall back to default behavior, but respect processor limits
                Assert.True(channelCount >= 1 && channelCount <= maxExpectedChannels,
                    $"Non-Windows channel count ({channelCount}) should be between 1 and {maxExpectedChannels}");
            }
        }
    }
}