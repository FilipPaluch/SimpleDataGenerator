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
    public class RangedNumberSpecimenBuilderTests
    {
        [Test]
        public void Should_Return_NoSpecimen_For_Not_Chosen_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.IntKilometers);

            var differentProperty = fixture.GetProperty<VehicleEntity>(x => x.IsActive);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(1, 2));

            //ACT

            var result = sut.Create(differentProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoSpecimen)));
        }

        [TestCase(0, 1)]
        [TestCase(0, 100)]
        [TestCase(100, 100000)]
        [TestCase(-10, 10)]
        [TestCase(-20, -10)]
        public void Should_Generate_Number_From_Range_For_Int_Values(int minimum, int maximum)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.IntKilometers);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(minimum, maximum));

            //ACT

            var result = (int)sut.Create(chosenProperty, new SpecimenContext(new Fixture()));

            //ASSERT
            Assert.LessOrEqual(result, maximum);
            Assert.GreaterOrEqual(result, minimum);
        }

        [TestCase(0.00f, 1.00f)]
        [TestCase(0.34f, 100.00f)]
        [TestCase(100.10f, 100000.98f)]
        [TestCase(-10.01f, 10.47f)]
        [TestCase(-20.99f, -10.00f)]
        public void Should_Generate_Number_From_Range_For_Float_Values(float minimum, float maximum)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.FloatKilometers);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(minimum, maximum));

            //ACT

            var result = (float)sut.Create(chosenProperty, new SpecimenContext(new Fixture()));

            //ASSERT
            Assert.LessOrEqual(result, maximum);
            Assert.GreaterOrEqual(result, minimum);
        }

        [TestCase(0.00, 1.00)]
        [TestCase(0.34, 100.00)]
        [TestCase(100.10, 100000.98)]
        [TestCase(-10.01, 10.47)]
        [TestCase(-20.99, -10.00)]
        public void Should_Generate_Number_From_Range_For_Double_Values(double minimum, double maximum)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.DoubleKilometers);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(minimum, maximum));

            //ACT

            var result = (double)sut.Create(chosenProperty, new SpecimenContext(new Fixture()));

            //ASSERT
            Assert.LessOrEqual(result, maximum);
            Assert.GreaterOrEqual(result, minimum);
        }
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        public void Should_Throw_Exception_When_Minimum_Value_Is_Greater_Or_Equal_Than_Maximum_For_Int_Values(int minimum, int maximum)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.IntKilometers);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(minimum, maximum));

            //ACT && ASSERT

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(chosenProperty, new SpecimenContext(new Fixture())));
        }

        [TestCase(1.00f, 1.00f)]
        [TestCase(2.00f, 1.00f)]
        public void Should_Throw_Exception_When_Minimum_Value_Is_Greater_Or_Equal_Than_Maximum_For_Float_Values(
            float minimum, float maximum)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.FloatKilometers);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(minimum, maximum));

            //ACT && ASSERT

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(chosenProperty, new SpecimenContext(new Fixture())));
        }

        [TestCase(1.00, 1.00)]
        [TestCase(2.22, 1.00)]
        public void Should_Throw_Exception_When_Minimum_Value_Is_Greater_Or_Equal_Than_Maximum_For_Double_Values(
            double minimum, double maximum)
        {
            //ARRANGE

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.DoubleKilometers);

            var sut = new RangedNumberSpecimenBuilder(chosenProperty, new NumberRange(minimum, maximum));

            //ACT && ASSERT

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(chosenProperty, new SpecimenContext(new Fixture())));
        }

    }
}
