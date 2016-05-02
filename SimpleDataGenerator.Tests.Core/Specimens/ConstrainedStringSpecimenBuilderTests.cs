using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Customization.Specimens;
using SimpleDataGenerator.Core.Model;
using SimpleDataGenerator.Tests.Core.Entities;

namespace SimpleDataGenerator.Tests.Core.Specimens
{
    [TestFixture]
    public class ConstrainedStringSpecimenBuilderTests
    {
        [Test]
        public void Should_Return_NoSpecimen_For_Not_Chosen_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var differentProperty = fixture.GetProperty<VehicleEntity>(x => x.IsActive);

            var sut = new ConstrainedStringSpecimenBuilder(chosenProperty, new ConstrainedString(0, 10));

            //ACT

            var result = sut.Create(differentProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoSpecimen)));
        }

        [TestCase(1,1)]
        [TestCase(0, 1)]
        [TestCase(0, 0)]
        [TestCase(0, 10)]
        [TestCase(10, 100)]
        [TestCase(100, 255)]
        public void Should_Return_Constrained_String(int minimumLength, int maximumLength)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var sut = new ConstrainedStringSpecimenBuilder(chosenProperty, new ConstrainedString(minimumLength, maximumLength));

            //ACT

            var result = (string)sut.Create(chosenProperty, new SpecimenContext(new Fixture()));

            //ASSERT
            Assert.GreaterOrEqual(result.Length, minimumLength);
            Assert.LessOrEqual(result.Length, maximumLength);
        }

        [Test]
        public void Should_Throw_Exception_When_Minimum_Length_Is_Greater_Than_Maximum_Length()
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var sut = new ConstrainedStringSpecimenBuilder(chosenProperty, new ConstrainedString(10, 1));

            //ACT && ASSET
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(chosenProperty, new SpecimenContext(new Fixture())));
        }

        [TestCase(-1, 10)]
        [TestCase(-2, -1)]
        [TestCase(1, -10)]
        public void Should_Throw_Exception_When_Minimum_Or_Maximum_Length_Is_Negative_Value(int minimumLength, int maximumLength)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var sut = new ConstrainedStringSpecimenBuilder(chosenProperty, new ConstrainedString(minimumLength, maximumLength));

            //ACT && ASSET
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(chosenProperty, new SpecimenContext(new Fixture())));
        }
    }
}
