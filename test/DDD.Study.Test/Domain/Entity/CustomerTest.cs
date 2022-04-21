using System;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using Xunit;

namespace DDD.Study.Test.Entity
{
    public class CustomerTest
    {
        [Fact]
        public void Customer_Create_CreateCustomer()
        {
            var subject = new Customer(Guid.NewGuid(), "John");
            subject.Name.Should().Be("John");
        }

        [Fact]
        public void Customer_ChangeName_ShouldChangeName()
        {
            var subject = new Customer(Guid.NewGuid(), "John");
            subject.ChangeName("Jane");
            subject.Name.Should().Be("Jane");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Customer_ChangeNameWithAInvalidName_ShouldThrowException(string name)
        {
            var subject = new Customer(Guid.NewGuid(), "John");
            Assert.Throws<InvalidOperationException>(() => subject.ChangeName(name));
        }

        [Fact]
        public void Customer_Activate_ShouldActivate()
        {
            var subject = new Customer(Guid.NewGuid(), "John");
            var address = new Address("Elm Street", 123, "10001", "New York");
            subject.ChangeAddress(address);

            subject.Activate();

            subject.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Customer_ActivateWithInvalidAddress_ShouldThrowException()
        {
            var subject = new Customer(Guid.NewGuid(), "John");

            subject.Invoking(x => x.Activate()).Should().Throw<InvalidOperationException>().WithMessage("Address is required.");
        }

        [Fact]
        public void Customer_Deactivate_ShouldDeactivate()
        {
            var subject = new Customer(Guid.NewGuid(), "John");
            subject.Deactivate();
            subject.IsActive.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Customer_BlankName_ShouldThrowException(string name)
        {
            Assert.Throws<InvalidOperationException>(() => new Customer(Guid.NewGuid(), name));
        }

        [Fact]
        public void Customer_IdIsEmpty_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => new Customer(Guid.Empty, "John"));
        }

        [Fact]
        public void Customer_AddRewardPoints_ShouldAddRewardPoints()
        {
            var subject = new Customer(Guid.NewGuid(), "John");

            subject.RewardPoints.Should().Be(0);

            subject.AddRewardPoints(100);
            subject.RewardPoints.Should().Be(100);

            subject.AddRewardPoints(50);
            subject.RewardPoints.Should().Be(150);
        }

        [Fact]
        public void Customer_AddRewardPointsWithDeactivateStatus_ShouldThrowException()
        {
            var subject = new Customer(Guid.NewGuid(), "John");
            subject.Deactivate();

            Assert.Throws<InvalidOperationException>(() => subject.AddRewardPoints(100));
        }

        [Fact]
        public void Customer_AddRewardPointsWithNegativePoints_ShouldThrowException()
        {
            var subject = new Customer(Guid.NewGuid(), "John");

            Assert.Throws<ArgumentException>(() => subject.AddRewardPoints(-1));
        }
    }
}