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
    public class RangedDateTimeSpecimenBuilderTests
    {
        [Test]
        public void Should_Return_NoSpecimen_For_Not_Chosen_Property()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.CreatedOn);

            var differentProperty = fixture.GetProperty<VehicleEntity>(x => x.IsActive);

            var sut = new RangedDateTimeSpecimenBuilder(chosenProperty, new DateTimeRange(DateTime.UtcNow, DateTime.UtcNow));

            //ACT

            var result = sut.Create(differentProperty, specimenContextMock.Object);

            //ASSERT
            Assert.That(result.GetType(), Is.EqualTo(typeof(NoSpecimen)));
        }

        [Test, TestCaseSource("ShouldReturnDateTimeBetweenRangeTestCases")]
        public void Should_Return_DateTime_Between_Range(DateTime fromDate, DateTime toDate)
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.CreatedOn);

            var sut = new RangedDateTimeSpecimenBuilder(chosenProperty, new DateTimeRange(fromDate, toDate));

            //ACT

            var result = (DateTime)sut.Create(chosenProperty, specimenContextMock.Object);

            //ASSERT
            Assert.GreaterOrEqual(result, fromDate);
            Assert.LessOrEqual(result, toDate);
        }

        static readonly object[] ShouldReturnDateTimeBetweenRangeTestCases =
        {
            new object[] { new DateTime(2015, 01, 01), new DateTime(2016, 01, 01) },
            new object[] { new DateTime(2015, 01, 01, 06, 10, 20), new DateTime(2020, 12, 30, 1, 15, 20) },
            new object[] { DateTime.MinValue, DateTime.MaxValue },
            new object[] { new DateTime(2015, 01, 01), new DateTime(2015, 01, 01) } 
        };

        [Test]
        public void Should_Throw_ArgumentException_When_From_Date_Is_Greater_Than_To_Date()
        {
            //ARRANGE

            var specimenContextMock = new Mock<ISpecimenContext>();

            var fixture = new SpecimenBuilderFixture();

            var chosenProperty = fixture.GetProperty<VehicleEntity>(x => x.CreatedOn);

            var sut = new RangedDateTimeSpecimenBuilder(chosenProperty, new DateTimeRange(new DateTime(2016, 01, 01), new DateTime(2015, 01, 01)));

            //ACT && ASSERT

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(chosenProperty, specimenContextMock.Object));

        }
    }
}
