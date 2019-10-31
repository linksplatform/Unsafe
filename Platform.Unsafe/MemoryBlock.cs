﻿using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public static unsafe class MemoryBlock
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Zero(void* pointer, long capacity)
        {
            // A way to prevent wasting resources due to Hyper-threading.
            var threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                InitBlock(pointer, 0, (uint)capacity);
            }
            else
            {
                // Using 2 threads, because two-channel memory architecture is the most available type.
                // CPUs are mostly just wait for memory here.
                threads = 2;
                Parallel.ForEach(Partitioner.Create(0L, capacity), new ParallelOptions { MaxDegreeOfParallelism = threads }, range => ZeroBlock(pointer, range.Item1, range.Item2));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ZeroBlock(void* pointer, long from, long to)
        {
            var offset = (void*)((byte*)pointer + from);
            var length = (uint)(to - from);
            InitBlock(offset, 0, length);
        }
    }
}
