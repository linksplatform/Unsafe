using BenchmarkDotNet.Attributes;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Benchmarks
{
    /// <summary>
    /// <para>
    /// Represents the size of benchmarks.
    /// </para>
    /// <para></para>
    /// </summary>
    [SimpleJob]
    [MemoryDiagnoser]
    public class SizeOfBenchmarks
    {
        /// <summary>
        /// <para>
        /// Structures the size.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public int StructureSize() => Structure<ulong>.Size;

        /// <summary>
        /// <para>
        /// Sizes the of expression.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public int SizeOfExpression() => sizeof(ulong);

        /// <summary>
        /// <para>
        /// Unsafes the size of.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The int</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public int UnsafeSizeOf() => SizeOf<ulong>();
    }
}
