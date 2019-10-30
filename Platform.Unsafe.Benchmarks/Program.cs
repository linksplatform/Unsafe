using BenchmarkDotNet.Running;

namespace Platform.Unsafe.Benchmarks
{
    static class Program
    {
        static void Main() => BenchmarkRunner.Run<MemoryBlockBenchmarks>();
    }
}
