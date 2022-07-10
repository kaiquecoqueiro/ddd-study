using System;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using Xunit;

namespace DDD.Study.Test.Entity
{
    public class AddressTest
    {
        [Fact]
        public void Address_ToString_ReturnAddress()
        {
            var subject = new Address("Elm Street", 123, "10001", "New York");
            subject.ToString().Should().Be("Elm Street 123, 10001 - New York");
        }

        [Theory]
        [InlineData("", 123, "10001", "New York")]
        [InlineData(null, 123, "10001", "New York")]
        [InlineData("Elm Street", 0, "10001", "New York")]
        [InlineData("Elm Street", -1, "10001", "New York")]
        [InlineData("Elm Street", 123, "", "New York")]
        [InlineData("Elm Street", 123, null, "New York")]
        [InlineData("Elm Street", 123, "10001", "")]
        [InlineData("Elm Street", 123, "10001", null)]
        public void Address_Invalid_ShouldThrowException(string street, int number, string zip, string city)
        {
            Assert.Throws<InvalidOperationException>(() => new Address(street, number, zip, city));
        }
    }
}