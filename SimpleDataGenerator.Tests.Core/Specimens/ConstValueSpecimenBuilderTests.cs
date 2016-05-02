using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ConstValueSpecimenBuilderTests
    {
        [Test]
        public void Should_Return_NoSpecimen_For_Not_Chosen_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var differentProperty = fixture.GetProperty<VehicleEntity>(x => x.IsActive);

            var sut = new ConstValueSpecimenBuilder(chosenProperty, "Name");

            //ACT

            var result = sut.Create(differentProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoSpecimen)));
        }

        [Test]
        public void Should_Return_ConstValue_For_Chosen_Proprety()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.Name);

            var sut = new ConstValueSpecimenBuilder(chosenProperty, "Name");

            //ACT

            var result = sut.Create(chosenProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result, Is.EqualTo("Name"));
        }
    }
}
