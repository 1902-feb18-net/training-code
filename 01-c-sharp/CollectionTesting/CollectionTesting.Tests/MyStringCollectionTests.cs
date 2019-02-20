using CollectionTesting.Library;
using System;
using Xunit;

namespace CollectionTesting.Tests
{
    // usually, we write one test class to test each of our real classes
    public class MyStringCollectionTests
    {
        [Fact]
        public void Test1()
        {
            // testing in general... make sure the code does what we expect.
            // manual testing... run the code in our IDEs, we plug in different
            // inputs, and make sure we get the expected output.

            // automated testing... we write the instructions for a test
            // and the expected results, then we re-run lots of tests automatically.

            // this helps us find and solve bugs quicker for subsequent development.
            // it also helps us design well in the first place.
            // testable code is better designed code

            // unit testing is a particular kind of automated testing,
            // where we resolve to test the smallest pieces we can at a time.

            // the alternative would be integration testing
        }

        // we put our test methods inside an otherwise ordinary class
        // FactAttribute is our first example of a C# attribute
        // our first example of an xUnit test is a "Fact" test.
        [Fact]
        public void AddShouldNotThrowException()
        {
            // three general steps to a unit test
            // 1. arrange
            var collection = new MyStringCollection();

            // 2. act
            collection.Add("abc");
            // if an exception is thrown, that's caught by the test runner
            // and counted as a failure of the test.

            // 3. assert
            // (none needed here)
        }

        [Fact]
        public void ContainsShouldBeTrueForContained()
        {
            //arrange
            var collection = new MyStringCollection();
            collection.Add("asdf");

            //act
            var result = collection.Contains("asdf");

            //assert
            Assert.True(result);
            // (xUnit provides Assert class with static methods
            // to help with asserting different things)
        }

        [Fact]
        public void ContainsShouldBeFalseForNotContained()
        {
            //arrange
            var collection = new MyStringCollection();

            //act
            var result = collection.Contains("asdf");

            //assert
            Assert.False(result);
        }

        //[Fact]
        //public void FailingTest()
        //{
        //    Assert.True(false);
        //}

        [Fact]
        public void RemoveEmptyShouldRemoveOneEmpty()
        {
            // sometimes we call the object that's being tested
            // the "subject under test" (SUT)

            // arrange
            var sut = new MyStringCollection();
            sut.Add("");

            // act
            sut.RemoveEmpty();

            // assert
            Assert.False(sut.Contains(""));
        }

        [Fact]
        public void GetFirstShouldGetFirstFromNonEmptyList()
        {
            var sut = new MyStringCollection();
            sut.Add("asdf");
            sut.Add("ghjk");

            var result = sut.GetFirst();

            // expected, then actual
            Assert.Equal("asdf", result);
        }

        [Fact]
        public void GetFirstShouldThrowOnEmptyList()
        {
            var sut = new MyStringCollection();

            //try
            //{
            //    sut.GetFirst();
            //}
            //catch (InvalidOperationException e)
            //{
            //    return;
            //}
            //Assert.True(false, "should have thrown InvalidOperationException");

            // assertion succeeds if the right exception (or a subtype) was thrown
            // fails if it was not thrown.
            Assert.ThrowsAny<InvalidOperationException>(() => sut.GetFirst());
        }
    }
}
