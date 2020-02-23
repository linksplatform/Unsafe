using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.Unsafe;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Unsafe.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public unsafe class CopyBenchmarks
    {
        [StructLayout(LayoutKind.Explicit)]
        struct B64
        {
            [FieldOffset(0)]
            public fixed byte headerBytes[64];
        }

        [StructLayout(LayoutKind.Explicit)]
        struct B128
        {
            [FieldOffset(0)]
            public fixed byte headerBytes[128];
        }

        [StructLayout(LayoutKind.Explicit)]
        struct B2048
        {
            [FieldOffset(0)]
            public fixed byte headerBytes[2048];
        }

        [StructLayout(LayoutKind.Explicit)]
        struct B4194304
        {
            [FieldOffset(0)]
            public fixed byte headerBytes[4096 * 1024];
        }

        private static byte[] _array;
        private static B64 _b64;
        private static B128 _b128;
        private static B2048 _b2048;
        private static B4194304 _b4194304;

        [GlobalSetup]
        public static void Setup()
        {
            _array = new byte[4096 * 1024];
        }

        [Benchmark]
        public void Copy64BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy((void*)pointer, ref _b64);
            }
        }

        [Benchmark]
        public void CopyBlock64BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock((void*)pointer, AsPointer(ref _b64), (uint)Structure<B64>.Size);
            }
        }

        [Benchmark]
        public void Copy128BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy((void*)pointer, ref _b128);
            }
        }

        [Benchmark]
        public void CopyBlock128BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock((void*)pointer, AsPointer(ref _b128), (uint)Structure<B128>.Size);
            }
        }

        [Benchmark]
        public void Copy2048BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy((void*)pointer, ref _b2048);
            }
        }

        [Benchmark]
        public void CopyBlock2048BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock((void*)pointer, AsPointer(ref _b2048), (uint)Structure<B2048>.Size);
            }
        }

        [Benchmark]
        public void Copy4194304BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy((void*)pointer, ref _b4194304);
            }
        }

        [Benchmark]
        public void CopyBlock4194304BytesToArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock((void*)pointer, AsPointer(ref _b4194304), (uint)Structure<B4194304>.Size);
            }
        }

        [Benchmark]
        public void Copy64BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy(AsPointer(ref _b64), ref AsRef<B64>((void*)pointer));
            }
        }

        [Benchmark]
        public void CopyBlock64BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock(AsPointer(ref _b64), (void*)pointer, (uint)Structure<B64>.Size);
            }
        }

        [Benchmark]
        public void Copy128BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy(AsPointer(ref _b128), ref AsRef<B128>((void*)pointer));
            }
        }

        [Benchmark]
        public void CopyBlock128BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock(AsPointer(ref _b128), (void*)pointer, (uint)Structure<B128>.Size);
            }
        }

        [Benchmark]
        public void Copy2048BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy(AsPointer(ref _b2048), ref AsRef<B2048>((void*)pointer));
            }
        }

        [Benchmark]
        public void CopyBlock2048BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock(AsPointer(ref _b2048), (void*)pointer, (uint)Structure<B2048>.Size);
            }
        }

        [Benchmark]
        public void Copy4194304BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                Copy(AsPointer(ref _b4194304), ref AsRef<B4194304>((void*)pointer));
            }
        }

        [Benchmark]
        public void CopyBlock4194304BytesFromArray()
        {
            fixed (byte* pointer = _array)
            {
                CopyBlock(AsPointer(ref _b4194304), (void*)pointer, (uint)Structure<B4194304>.Size);
            }
        }
    }
}
