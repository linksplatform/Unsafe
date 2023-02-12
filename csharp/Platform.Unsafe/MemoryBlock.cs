using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    /// <summary><para>Represetns creating a custom memory block via <see cref="Parallel"/>.</para><para>Представляет создание пользовательского блока памяти через <see cref="Parallel"/>.</para>
    /// </summary>
    public static unsafe class MemoryBlock
    {
        /// <summary>
        /// <para>Zeroes the data pointed to by <paramref name="pointer"/> while only the number of bytes specified in <paramref name="capacity"/> are set to zero.</para>
        /// <para>Обнуляет данные, на которые указывает <paramref name="pointer"/>, в то время как только количество байтов указанных в <paramref name="capacity"/> устанавливается равным нулю.</para>
        /// </summary>
        /// <param name="pointer"><para>The pointer.</para><para>Указатель.</para></param>
        /// <param name="capacity">
        /// <para>The capacity of the memory block (in bytes).</para>
        /// <para>Вместимость блока памяти (в байтах).</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Zero(void* pointer, long capacity)
        {
            // A way to prevent wasting resources due to Hyper-Threading.
            var threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                ZeroBlock(pointer, 0, capacity);
            }
            else
            {
                // Using 2 threads because two-channel memory architecture is the most available type.
                // CPUs mostly just wait for memory here.
                threads = 2;
                Parallel.ForEach(Partitioner.Create(0L, capacity), new ParallelOptions { MaxDegreeOfParallelism = threads }, range => ZeroBlock(pointer, range.Item1, range.Item2));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ZeroBlock(void* pointer, long from, long to)
        {
            var offset = (byte*)pointer + from;
            var length = to - from;
            var uintMaxValue = uint.MaxValue;
            while (length > uintMaxValue)
            {
                InitBlock(offset, 0, uintMaxValue);
                length -= uintMaxValue;
                offset += uintMaxValue;
            }
            InitBlock(offset, 0, unchecked((uint)length));
        }
    }
}
