using ReservationSystem_PoC.Domain.Core.Extensions;
using System;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.ValidatorTests
{
    public class GuidValidatorTest
    {

        [Fact]
        public void GuidEmpty_False()
        {
            //arrange
            var guidCandidate = Guid.Empty;

            //act
            var result = guidCandidate.ToString().IsValidGuid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void Guid_Ok()
        {
            //arrange
            var guidCandidate = Guid.NewGuid();

            //act
            var result = guidCandidate.ToString().IsValidGuid();

            //assert
            Assert.True(result);

        }

        [Fact]
        public void Guid_null_False()
        {
            //arrange
            var guidCandidate = string.Empty;

            //act
            var result = guidCandidate.IsValidGuid();

            //assert
            Assert.False(result);

        }

    }
}
