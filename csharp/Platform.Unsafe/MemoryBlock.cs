using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary>
    /// <para>Represents a set of methods for memory blocks.</para>
    /// <para>Представляет набор методов для блоков памяти.</para>
    /// </summary>
    public static unsafe class MemoryBlock
    {
        private static readonly Lazy<int> _physicalCoreCount = new(() => GetPhysicalCoreCount());
        
        /// <summary>
        /// <para>Gets the number of physical CPU cores.</para>
        /// <para>Получает количество физических ядер ЦП.</para>
        /// </summary>
        public static int PhysicalCoreCount => _physicalCoreCount.Value;
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
            var physicalCores = PhysicalCoreCount;
            if (physicalCores <= 1)
            {
                ZeroBlock(pointer, 0, capacity);
            }
            else
            {
                // For memory operations, optimal thread count is typically min(physical_cores, memory_channels).
                // Most systems have dual-channel memory, so we limit to 2 threads for optimal memory bandwidth utilization.
                // More threads would compete for memory bandwidth without providing benefits.
                var threads = Math.Min(physicalCores, 2);
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

        private static int GetPhysicalCoreCount()
        {
            try
            {
                // Try platform-specific detection first
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return GetPhysicalCoreCountWindows();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return GetPhysicalCoreCountLinux();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return GetPhysicalCoreCountMacOS();
                }
            }
            catch
            {
                // If platform-specific detection fails, fall back to the original approach
            }
            
            // Fallback: assume hyper-threading and divide by 2
            // This maintains backward compatibility with the original behavior
            return Math.Max(1, Environment.ProcessorCount / 2);
        }

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Method is only called on Windows platform as checked by caller")]
        private static int GetPhysicalCoreCountWindows()
        {
            // Use WMI to get actual physical core count on Windows
            try
            {
                using var searcher = new System.Management.ManagementObjectSearcher("SELECT NumberOfCores FROM Win32_Processor");
                int totalCores = 0;
                foreach (System.Management.ManagementObject obj in searcher.Get())
                {
                    totalCores += (int)(uint)obj["NumberOfCores"];
                }
                return totalCores > 0 ? totalCores : Math.Max(1, Environment.ProcessorCount / 2);
            }
            catch
            {
                return Math.Max(1, Environment.ProcessorCount / 2);
            }
        }

        private static int GetPhysicalCoreCountLinux()
        {
            // Parse /proc/cpuinfo to get physical core count on Linux
            try
            {
                var cpuInfo = System.IO.File.ReadAllText("/proc/cpuinfo");
                var lines = cpuInfo.Split('\n');
                var physicalIds = new System.Collections.Generic.HashSet<string>();
                int coresPerPhysicalCpu = 1;

                foreach (var line in lines)
                {
                    if (line.StartsWith("physical id"))
                    {
                        var parts = line.Split(':');
                        if (parts.Length > 1)
                        {
                            physicalIds.Add(parts[1].Trim());
                        }
                    }
                    else if (line.StartsWith("cpu cores"))
                    {
                        var parts = line.Split(':');
                        if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out int cores))
                        {
                            coresPerPhysicalCpu = cores;
                        }
                    }
                }

                var physicalCoreCount = physicalIds.Count * coresPerPhysicalCpu;
                return physicalCoreCount > 0 ? physicalCoreCount : Math.Max(1, Environment.ProcessorCount / 2);
            }
            catch
            {
                return Math.Max(1, Environment.ProcessorCount / 2);
            }
        }

        private static int GetPhysicalCoreCountMacOS()
        {
            // On macOS, use sysctl to get physical core count
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "sysctl",
                        Arguments = "-n hw.physicalcpu",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                var output = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();
                
                if (int.TryParse(output, out int physicalCores) && physicalCores > 0)
                {
                    return physicalCores;
                }
            }
            catch
            {
                // Fall through to fallback
            }
            
            return Math.Max(1, Environment.ProcessorCount / 2);
        }
    }
}
