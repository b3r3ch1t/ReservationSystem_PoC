using ReservationSystem_PoC.Domain.Core.Extensions;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.EntityTests
{
    public class PhoneValidatorTest
    {
        [Theory]
        [InlineData("555 555 1212")]
        [InlineData("813.555.1212")]
        [InlineData("206.867.1234")]
        [InlineData("206-123-4567")]
        [InlineData("206-555-6666")]
        [InlineData("2068671234")]
        [InlineData("206 8671234")]
        [InlineData("8002223333")]
        [InlineData("206 867 1234")]
        [InlineData("(100) 455 1212")]
        [InlineData("(206) 867-1234")]
        [InlineData("(800) 155 1212")]
        [InlineData("(800) 901 1212")]

        [InlineData("(833) 333-3333")]
        [InlineData("(800) 555 1212")]
        [InlineData("(800) 910 1212")]
        [InlineData("(800) 911 1212")]
        public void PhoneValid_True(string phoneCandidate)
        {
            //act
            var result = phoneCandidate.IsPhoneValid();


            //assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("12068671234")]
        [InlineData("1-006-867-1234")]
        public void PhoneValid_False(string phoneCandidate)
        {
            //act
            var result = phoneCandidate.IsPhoneValid();


            //assert
            Assert.False(result);
        }

        [Fact]
        public void PhoneEmpty_False()
        {

            //arrange
            var phoneCandidate = string.Empty;

            //act
            var result = phoneCandidate.IsPhoneValid();

            //assert
            Assert.False(result);
        }


        [Fact]
        public void PhoneNull_False()
        {

            //arrange
            var phoneCandidate = (string)null;

            //act
            var result = phoneCandidate.IsPhoneValid();

            //assert
            Assert.False(result);
        }

        [Fact]
        public void PhoneWhiteSpace_False()
        {

            //arrange
            var phoneCandidate = " ";

            //act
            var result = phoneCandidate.IsPhoneValid();

            //assert
            Assert.False(result);
        }
    }
}
