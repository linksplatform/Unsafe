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
        public static void Zero(void* pointer, long capacity) => InitBlock(pointer, 0, (uint)capacity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ParallelZero(void* pointer, long capacity) => Parallel.ForEach(Partitioner.Create(0, capacity), range => ZeroBlock(pointer, range.Item1, range.Item2));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ZeroBlock(void* pointer, long from, long to)
        {
            var offset = (void*)((byte*)pointer + from);
            var length = (uint)(to - from);
            InitBlock(offset, 0, length);
        }
    }
}
