using BenchmarkDotNet.Running;

namespace Platform.Unsafe.Benchmarks
{
    /// <summary>
    /// <para>
    /// Represents the program.
    /// </para>
    /// <para></para>
    /// </summary>
    static class Program
    {
        /// <summary>
        /// <para>
        /// Main.
        /// </para>
        /// <para></para>
        /// </summary>
        static void Main()
        {
            BenchmarkRunner.Run<CopyBenchmarks>();
            BenchmarkRunner.Run<SizeOfBenchmarks>();
            BenchmarkRunner.Run<MemoryBlockBenchmarks>();
        }
    }
}
