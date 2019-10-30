using BenchmarkDotNet.Attributes;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public unsafe class MemoryBlockBenchmarks
    {
        [Params(10000, 1000000, 100000000)]
        public int N { get; set; }

        private static byte[] _array;

        [GlobalSetup]
        public static void Setup() => _array = new byte[4096 * 1024];

        [Benchmark]
        public void ZeroForLoop()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                _array[i] = 0;
            }
        }

        [Benchmark]
        public void Zero()
        {
            fixed (byte* pointer = _array)
            {
                MemoryBlock.Zero(pointer, _array.Length);
            }
        }

        [Benchmark]
        public void ParallelZero()
        {
            fixed (byte* pointer = _array)
            {
                MemoryBlock.ParallelZero(pointer, _array.Length);
            }
        }
    }
}
