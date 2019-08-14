using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Platform.Unsafe
{
    public static unsafe class MemoryBlock
    {
        public static void Zero(void* pointer, long capacity)
        {
            var ulongs = capacity / sizeof(ulong);
            Parallel.ForEach(Partitioner.Create(0, ulongs), range =>
            {
                for (long i = range.Item1; i < range.Item2; i++)
                {
                    *((ulong*)pointer + i) = 0;
                }
            });
            for (var i = ulongs * sizeof(ulong); i < capacity; i++)
            {
                *((byte*)pointer + i) = 0;
            }
        }
    }
}
