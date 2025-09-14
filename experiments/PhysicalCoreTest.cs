using System;
using System.Management;
using System.Runtime.InteropServices;

namespace PhysicalCoreExperiments
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine($"Environment.ProcessorCount: {Environment.ProcessorCount}");
            
            // Test different approaches to get physical core count
            Console.WriteLine("\n=== Different approaches to get physical cores ===");
            
            try
            {
                // Approach 1: WMI (Windows only)
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    int physicalCores = GetPhysicalCoresWmi();
                    Console.WriteLine($"Physical cores (WMI): {physicalCores}");
                }
                
                // Approach 2: /proc/cpuinfo parsing (Linux)
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    int physicalCores = GetPhysicalCoresLinux();
                    Console.WriteLine($"Physical cores (Linux): {physicalCores}");
                }
                
                // Current approach
                int currentApproach = Environment.ProcessorCount / 2;
                Console.WriteLine($"Current approach (ProcessorCount/2): {currentApproach}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        static int GetPhysicalCoresWmi()
        {
            int physicalCores = 0;
            using (var searcher = new ManagementObjectSearcher("SELECT NumberOfCores FROM Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    physicalCores += int.Parse(item["NumberOfCores"].ToString());
                }
            }
            return physicalCores;
        }
        
        static int GetPhysicalCoresLinux()
        {
            try
            {
                var cpuInfo = System.IO.File.ReadAllText("/proc/cpuinfo");
                var lines = cpuInfo.Split('\n');
                var physicalIds = new System.Collections.Generic.HashSet<string>();
                
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
                }
                
                return physicalIds.Count * GetCoresPerPhysicalCpu();
            }
            catch
            {
                return Environment.ProcessorCount / 2; // fallback
            }
        }
        
        static int GetCoresPerPhysicalCpu()
        {
            try
            {
                var cpuInfo = System.IO.File.ReadAllText("/proc/cpuinfo");
                var lines = cpuInfo.Split('\n');
                
                foreach (var line in lines)
                {
                    if (line.StartsWith("cpu cores"))
                    {
                        var parts = line.Split(':');
                        if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out int cores))
                        {
                            return cores;
                        }
                    }
                }
            }
            catch { }
            
            return 1; // fallback
        }
    }
}