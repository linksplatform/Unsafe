using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Platform.Unsafe.Experiments;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Experiments
{
    /// <summary>
    /// Benchmark comparing current MemoryBlock.Zero with improved SIMD versions
    /// </summary>
    [SimpleJob]
    [MemoryDiagnoser]
    public unsafe class ImprovedMemoryBlockBenchmark
    {
        private static byte[] _smallArray;
        private static byte[] _mediumArray;
        private static byte[] _largeArray;

        [GlobalSetup]
        public static void Setup()
        {
            _smallArray = new byte[256];       // 256 bytes - small block
            _mediumArray = new byte[64 * 1024]; // 64KB - medium block
            _largeArray = new byte[4 * 1024 * 1024]; // 4MB - large block
        }

        // Small block benchmarks (256 bytes)
        [Benchmark]
        public void SmallBlock_Current()
        {
            fixed (byte* pointer = _smallArray)
            {
                MemoryBlock.Zero(pointer, _smallArray.Length);
            }
        }

        [Benchmark]
        public void SmallBlock_SIMD()
        {
            fixed (byte* pointer = _smallArray)
            {
                ImprovedMemoryZero.ZeroSIMD(pointer, _smallArray.Length);
            }
        }

        [Benchmark]
        public void SmallBlock_Adaptive()
        {
            fixed (byte* pointer = _smallArray)
            {
                ImprovedMemoryZero.ZeroAdaptive(pointer, _smallArray.Length);
            }
        }

        // Medium block benchmarks (64KB)
        [Benchmark]
        public void MediumBlock_Current()
        {
            fixed (byte* pointer = _mediumArray)
            {
                MemoryBlock.Zero(pointer, _mediumArray.Length);
            }
        }

        [Benchmark]
        public void MediumBlock_SIMD()
        {
            fixed (byte* pointer = _mediumArray)
            {
                ImprovedMemoryZero.ZeroSIMD(pointer, _mediumArray.Length);
            }
        }

        [Benchmark]
        public void MediumBlock_Adaptive()
        {
            fixed (byte* pointer = _mediumArray)
            {
                ImprovedMemoryZero.ZeroAdaptive(pointer, _mediumArray.Length);
            }
        }

        // Large block benchmarks (4MB)
        [Benchmark]
        public void LargeBlock_Current()
        {
            fixed (byte* pointer = _largeArray)
            {
                MemoryBlock.Zero(pointer, _largeArray.Length);
            }
        }

        [Benchmark]
        public void LargeBlock_SIMD()
        {
            fixed (byte* pointer = _largeArray)
            {
                ImprovedMemoryZero.ZeroSIMD(pointer, _largeArray.Length);
            }
        }

        [Benchmark]
        public void LargeBlock_MultiThreadedSIMD()
        {
            fixed (byte* pointer = _largeArray)
            {
                ImprovedMemoryZero.ZeroMultiThreadedSIMD(pointer, _largeArray.Length);
            }
        }

        [Benchmark]
        public void LargeBlock_Adaptive()
        {
            fixed (byte* pointer = _largeArray)
            {
                ImprovedMemoryZero.ZeroAdaptive(pointer, _largeArray.Length);
            }
        }
    }

    /// <summary>
    /// Simple console program to run the benchmarks
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Platform.Unsafe Memory Zeroing Performance Comparison");
            Console.WriteLine("====================================================");
            
            var summary = BenchmarkRunner.Run<ImprovedMemoryBlockBenchmark>();
        }
    }
}