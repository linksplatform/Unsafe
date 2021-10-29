using System;
using BenchmarkDotNet.Attributes;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Benchmarks
{
    /// <summary>
    /// <para>
    /// Represents the memory block benchmarks.
    /// </para>
    /// <para></para>
    /// </summary>
    [SimpleJob]
    [MemoryDiagnoser]
    public unsafe class MemoryBlockBenchmarks
    {
        /// <summary>
        /// <para>
        /// The array.
        /// </para>
        /// <para></para>
        /// </summary>
        private static byte[] _array;

        /// <summary>
        /// <para>
        /// Setup.
        /// </para>
        /// <para></para>
        /// </summary>
        [GlobalSetup]
        public static void Setup() => _array = new byte[4096 * 1024];

        /// <summary>
        /// <para>
        /// Arrays the clear.
        /// </para>
        /// <para></para>
        /// </summary>
        [Benchmark]
        public void ArrayClear() => Array.Clear(_array, 0, _array.Length);

        /// <summary>
        /// <para>
        /// Fors the loop.
        /// </para>
        /// <para></para>
        /// </summary>
        [Benchmark]
        public void ForLoop()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                _array[i] = 0;
            }
        }

        /// <summary>
        /// <para>
        /// Unsafes the init block.
        /// </para>
        /// <para></para>
        /// </summary>
        [Benchmark]
        public void UnsafeInitBlock()
        {
            fixed (byte* pointer = _array)
            {
                InitBlock((void*)pointer, 0, (uint)_array.Length);
            }
        }

        /// <summary>
        /// <para>
        /// Memories the block zero.
        /// </para>
        /// <para></para>
        /// </summary>
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
