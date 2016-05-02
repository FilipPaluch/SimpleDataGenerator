using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Customization.Specimens;
using SimpleDataGenerator.Core.Model;
using SimpleDataGenerator.Tests.Core.Entities;

namespace SimpleDataGenerator.Tests.Core.Specimens
{
    [TestFixture]
    public class EmtpySpecimenBuilderTests
    {
        [Test]
        public void Should_Return_NoSpecimen_For_Not_Chosen_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var differentProperty = fixture.GetProperty<VehicleEntity>(x => x.IsActive);

            var sut = new EmtpySpecimenBuilder(chosenProperty);

            //ACT

            var result = sut.Create(differentProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoSpecimen)));
        }

        [Test]
        public void Should_Return_Default_Value_For_Value_Type_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.IntKilometers);

            var sut = new EmtpySpecimenBuilder(chosenProperty);

            //ACT

            var result = sut.Create(chosenProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Should_Return_Null_For_Not_Value_Type_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.IsDeactivated);

            var sut = new EmtpySpecimenBuilder(chosenProperty);

            //ACT

            var result = sut.Create(chosenProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result, Is.EqualTo(null));
        }
    }
}
