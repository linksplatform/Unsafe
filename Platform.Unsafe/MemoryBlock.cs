using System.Collections.Concurrent;
using System.Threading.Tasks;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Unsafe
{
    public static unsafe class MemoryBlock
    {
        public static void Zero(void* pointer, long capacity)
        {
            Parallel.ForEach(Partitioner.Create(0, capacity), range =>
            {
                var from = range.Item1;
                var offset = (void*)((byte*)pointer + from);
                var length = (uint)(range.Item2 - from);
                System.Runtime.CompilerServices.Unsafe.InitBlock(offset, 0, length);
            });
        }
    }
}
