using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressablesServices.Tests
{
    public static class CAssert
    {
        public static async Task<T> ThrowsAsync<T>(Func<Task> code) where T : Exception
        {
            var actual = default(T);

            try
            {
                await code();
                Assert.Fail($"Expected exception of type: {typeof (T)}");
            }
            catch (T rex)
            {
                actual = rex;
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected exception of type: {typeof(T)} but was {ex.GetType()} instead");
            }

            return actual;
        }
    }
}