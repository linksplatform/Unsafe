using System.Runtime.InteropServices;
using Xunit;

namespace Platform.Unsafe.Tests
{
    public class IntPtrExtensionsTests
    {
        [Fact]
        public void ReadAndWriteOperationsForPointerValuesTest()
        {
            var pointer = Marshal.AllocHGlobal(sizeof(ulong));
            pointer.SetValue(42UL);
            Assert.True(pointer.GetValue<ulong>() == 42UL);
            Marshal.FreeHGlobal(pointer);
        }
    }
}