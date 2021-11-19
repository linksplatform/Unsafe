using System;
using BenchmarkDotNet.Attributes;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public unsafe class MemoryBlockBenchmarks
    {
        private static byte[] _array;

        [GlobalSetup]
        public static void Setup() => _array = new byte[4096 * 1024];

        [Benchmark]
        public void ArrayClear() => Array.Clear(_array, 0, _array.Length);

        [Benchmark]
        public void ForLoop()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                _array[i] = 0;
            }
        }

        [Benchmark]
        public void UnsafeInitBlock()
        {
            fixed (byte* pointer = _array)
            {
                InitBlock((void*)pointer, 0, (uint)_array.Length);
            }
        }

        [Benchmark]
        public void MemoryBlockZero()
        {
            fixed (byte* pointer = _array)
            {
                MemoryBlock.Zero(pointer, _array.Length);
            }
        }
    }
}
