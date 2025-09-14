using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.Unsafe;

using System.Management;
using System.Runtime.InteropServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents a set of methods for memory blocks.</para>
    /// <para>Представляет набор методов для блоков памяти.</para>
    /// </summary>
    public static unsafe class MemoryBlock
    {
        private static readonly Lazy<int> _memoryChannelCount = new(() => DetectMemoryChannelCount());

        /// <summary>
        /// <para>Gets the number of memory channels available on the current system.</para>
        /// <para>Получает количество каналов памяти, доступных в текущей системе.</para>
        /// </summary>
        public static int MemoryChannelCount => _memoryChannelCount.Value;

        private static int DetectMemoryChannelCount()
        {
            try
            {
                // Try to detect memory channels on Windows using WMI
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return DetectMemoryChannelCountWindows();
                }
            }
            catch
            {
                // Fall through to default behavior if detection fails
            }
            
            // Default to 2 channels (dual-channel memory is most common)
            // But respect the processor count limitation to avoid wasting resources
            var defaultChannels = 2;
            var maxThreads = Math.Max(1, Environment.ProcessorCount / 2);
            return Math.Min(defaultChannels, maxThreads);
        }

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private static int DetectMemoryChannelCountWindows()
        {
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            using var results = searcher.Get();
            
            var interleaveDataDepth = 0;
            var memoryDeviceCount = 0;
            
            foreach (ManagementObject obj in results)
            {
                memoryDeviceCount++;
                
                // Try to get InterleaveDataDepth property
                var depth = obj["InterleaveDataDepth"];
                if (depth != null && depth is uint depthValue && depthValue > 0)
                {
                    interleaveDataDepth = Math.Max(interleaveDataDepth, (int)depthValue);
                }
            }
            
            var maxThreads = Math.Max(1, Environment.ProcessorCount / 2);
            
            // If we found InterleaveDataDepth, use it
            if (interleaveDataDepth > 0)
            {
                return Math.Min(interleaveDataDepth, maxThreads);
            }
            
            // Fallback: estimate based on memory device count
            // Common configurations: 2 DIMMs = dual channel, 4 DIMMs = quad channel
            if (memoryDeviceCount >= 4)
            {
                return Math.Min(4, maxThreads);
            }
            
            // Default to dual channel, but respect processor limits
            return Math.Min(2, maxThreads);
        }

        /// <summary>
        /// <para>Zeroes the number of bytes specified in <paramref name="capacity"/> starting from <paramref name="pointer"/>.</para>
        /// <para>Обнуляет количество байтов, указанное в <paramref name="capacity"/>, начиная с <paramref name="pointer"/>.</para>
        /// </summary>
        /// <param name="pointer"><para>The pointer.</para><para>Указатель.</para></param>
        /// <param name="capacity">
        /// <para>The capacity of the memory block (in bytes).</para>
        /// <para>Вместимость блока памяти (в байтах).</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Zero(void* pointer, long capacity)
        {
            // A way to prevent wasting resources due to Hyper-Threading.
            var maxThreads = Environment.ProcessorCount / 2;
            if (maxThreads <= 1)
            {
                ZeroBlock(pointer, 0, capacity);
            }
            else
            {
                // Use detected memory channel count, but cap it by processor count / 2
                // CPUs mostly just wait for memory here, so we optimize for memory bandwidth.
                var threads = Math.Min(MemoryChannelCount, maxThreads);
                Parallel.ForEach(Partitioner.Create(0L, capacity), new ParallelOptions { MaxDegreeOfParallelism = threads }, range => ZeroBlock(pointer, range.Item1, range.Item2));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ZeroBlock(void* pointer, long from, long to)
        {
            var offset = (byte*)pointer + from;
            var length = to - from;
            var uintMaxValue = uint.MaxValue;
            while (length > uintMaxValue)
            {
                InitBlock(offset, 0, uintMaxValue);
                length -= uintMaxValue;
                offset += uintMaxValue;
            }
            InitBlock(offset, 0, unchecked((uint)length));
        }
    }
}
