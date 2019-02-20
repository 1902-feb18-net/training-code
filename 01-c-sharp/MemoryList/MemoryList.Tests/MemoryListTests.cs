using ML.Library;
using System;
using Xunit;

namespace ML.Tests
{
    public class MemoryListTests
    {
        // second type of xUnit test: Theory
        // Facts don't allow any parameters.
        // Theories accept sets of parameters, to run the test against all of them.
        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        [InlineData(10000)]
        public void AddedItemsShouldBeContained(int value)
        {
            var list = new MemoryList<int>();

            list.Add(value);

            Assert.True(list.Contains(value));
        }
    }
}
