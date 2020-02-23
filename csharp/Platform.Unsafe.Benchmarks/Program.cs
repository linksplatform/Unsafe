using BenchmarkDotNet.Running;

namespace Platform.Unsafe.Benchmarks
{
    static class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<CopyBenchmarks>();
            BenchmarkRunner.Run<SizeOfBenchmarks>();
            BenchmarkRunner.Run<MemoryBlockBenchmarks>();
        }
    }
}
