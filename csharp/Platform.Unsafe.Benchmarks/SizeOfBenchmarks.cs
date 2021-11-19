using BenchmarkDotNet.Attributes;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class SizeOfBenchmarks
    {
        [Benchmark]
        public int StructureSize() => Structure<ulong>.Size;

        [Benchmark]
        public int SizeOfExpression() => sizeof(ulong);

        [Benchmark]
        public int UnsafeSizeOf() => SizeOf<ulong>();
    }
}
