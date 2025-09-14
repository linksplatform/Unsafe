using System;
using Platform.Unsafe;

namespace PhysicalCoreTest
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Physical Core Detection Test ===");
            Console.WriteLine($"Environment.ProcessorCount (logical processors): {Environment.ProcessorCount}");
            Console.WriteLine($"MemoryBlock.PhysicalCoreCount (physical cores): {MemoryBlock.PhysicalCoreCount}");
            Console.WriteLine($"Previous approach (ProcessorCount / 2): {Environment.ProcessorCount / 2}");
            
            Console.WriteLine("\n=== Memory Block Zero Test ===");
            
            unsafe
            {
                const int testSize = 1024 * 1024; // 1MB
                var buffer = new byte[testSize];
                
                fixed (byte* ptr = buffer)
                {
                    // Fill with non-zero values first
                    for (int i = 0; i < testSize; i++)
                    {
                        buffer[i] = (byte)(i % 256);
                    }
                    
                    Console.WriteLine($"Before Zero: buffer[100] = {buffer[100]}, buffer[500] = {buffer[500]}");
                    
                    // Test our Zero method
                    MemoryBlock.Zero(ptr, testSize);
                    
                    Console.WriteLine($"After Zero: buffer[100] = {buffer[100]}, buffer[500] = {buffer[500]}");
                    
                    // Verify all bytes are zero
                    bool allZero = true;
                    for (int i = 0; i < testSize; i++)
                    {
                        if (buffer[i] != 0)
                        {
                            allZero = false;
                            break;
                        }
                    }
                    
                    Console.WriteLine($"All bytes are zero: {allZero}");
                }
            }
            
            Console.WriteLine("\nPhysical core detection test completed successfully!");
        }
    }
}