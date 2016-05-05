using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using SimpleDataGenerator.Core;
using SimpleDataGenerator.Core.Mapping.Implementations;
using SimpleDataGenerator.Tests.Core.Entities;
using Ploeh.AutoFixture;

namespace SimpleDataGenerator.Tests.Core.Generator
{
    [TestFixture]
    public class SimpleAutoDataGeneratorTests
    {
        [Test]
        public void Should_Generate_Property_With_Expected_Length()
        {
            //ARRANGE

            var vehicleMapping = new EntityConfiguration<VehicleEntity>();
            vehicleMapping.For(x => x.Name).WithLength(10);
            vehicleMapping.For(x => x.Description).WithLength(1);
            vehicleMapping.For(x => x.Comments).WithLength(255);

            var sut = new SimpleAutoDataGenerator();

            sut.WithConfiguration(vehicleMapping);

            //ACT
            var result = sut.Fixture.Create<VehicleEntity>();

            //ASSERT
            Assert.That(result.Name.Length, Is.EqualTo(10));
            Assert.That(result.Description.Length, Is.EqualTo(1));
            Assert.That(result.Comments.Length, Is.EqualTo(255));
        }

        [Test]
        public void Should_Not_Generate_Property_When_Is_Marked_As_Empty()
        {
            //ARRANGE

            var vehicleMapping = new EntityConfiguration<VehicleEntity>();
            vehicleMapping.For(x => x.Name).IsEmpty();
            vehicleMapping.For(x => x.IsDeactivated).IsEmpty();
            vehicleMapping.For(x => x.IntKilometers).IsEmpty();
            vehicleMapping.For(x => x.NullableIsActive).IsEmpty();
            vehicleMapping.For(x => x.IsActive).IsEmpty();
            vehicleMapping.For(x => x.User).IsEmpty();

            var sut = new SimpleAutoDataGenerator();

            sut.WithConfiguration(vehicleMapping);

            //ACT
            var result = sut.Fixture.Create<VehicleEntity>();

            //ASSERT
            Assert.That(result.Name, Is.EqualTo(null));
            Assert.That(result.IsDeactivated, Is.EqualTo(null));
            Assert.That(result.IntKilometers, Is.EqualTo(0));
            Assert.That(result.NullableIsActive, Is.EqualTo(null));
            Assert.That(result.IsActive, Is.EqualTo(false));
            Assert.That(result.User, Is.EqualTo(null));
        }

        [Test]
        public void Should_Generate_Property_From_Range()
        {
            //ARRANGE

            var vehicleMapping = new EntityConfiguration<VehicleEntity>();
            vehicleMapping.For(x => x.IntKilometers).InRange(1, 20000);
            vehicleMapping.For(x => x.FloatKilometers).InRange(100.9f, 20000.1f);
            vehicleMapping.For(x => x.DoubleKilometers).InRange(200.3, 20000.8);
            vehicleMapping.For(x => x.Name).WithLengthRange(1, 5);
            vehicleMapping.For(x => x.CreatedOn).InRange(new DateTime(2016, 01, 01), new DateTime(2016, 02, 01));

            var sut = new SimpleAutoDataGenerator();

            sut.WithConfiguration(vehicleMapping);

            //ACT
            var result = sut.Fixture.Create<VehicleEntity>();

            //ASSERT
            Assert.LessOrEqual(result.IntKilometers, 20000);
            Assert.GreaterOrEqual(result.IntKilometers, 1);

            Assert.LessOrEqual(result.FloatKilometers, 20000.1f);
            Assert.GreaterOrEqual(result.FloatKilometers, 100.9f);

            Assert.LessOrEqual(result.DoubleKilometers, 20000.8);
            Assert.GreaterOrEqual(result.DoubleKilometers, 200.3);

            Assert.LessOrEqual(result.Name.Length, 5);
            Assert.GreaterOrEqual(result.Name.Length, 1);

            Assert.LessOrEqual(result.CreatedOn, new DateTime(2016, 02, 01));
            Assert.GreaterOrEqual(result.CreatedOn, new DateTime(2016, 01, 01));
        }

        [Test]
        public void Should_Generate_Property_From_Range_For_Nullable_Properties()
        {
            //ARRANGE

            var vehicleMapping = new EntityConfiguration<VehicleEntity>();
            vehicleMapping.For(x => x.NullableIntKilometers).InRange(1, 20000);
            vehicleMapping.For(x => x.NullableFloatKilometers).InRange(100.9f, 20000.1f);
            vehicleMapping.For(x => x.NullableDoubleKilometers).InRange(200.3, 20000.8);
            vehicleMapping.For(x => x.NullableCreatedOn).InRange(new DateTime(2016, 01, 01), new DateTime(2016, 02, 01));

            var sut = new SimpleAutoDataGenerator();

            sut.WithConfiguration(vehicleMapping);

            //ACT
            var result = sut.Fixture.Create<VehicleEntity>();

            //ASSERT
            Assert.LessOrEqual(result.NullableIntKilometers, 20000);
            Assert.GreaterOrEqual(result.NullableIntKilometers, 1);

            Assert.LessOrEqual(result.NullableFloatKilometers, 20000.1f);
            Assert.GreaterOrEqual(result.NullableFloatKilometers, 100.9f);

            Assert.LessOrEqual(result.NullableDoubleKilometers, 20000.8);
            Assert.GreaterOrEqual(result.NullableDoubleKilometers, 200.3);

            Assert.LessOrEqual(result.NullableCreatedOn, new DateTime(2016, 02, 01));
            Assert.GreaterOrEqual(result.NullableCreatedOn, new DateTime(2016, 01, 01));
        }

        [Test]
        public void Should_Generate_Email_Property()
        {
            //ARRANGE

            var userMapping = new EntityConfiguration<UserEntity>();
            userMapping.For(x => x.Email).IsEmail();

            var sut = new SimpleAutoDataGenerator();

            sut.WithConfiguration(userMapping);

            //ACT
            var result = sut.Fixture.Create<UserEntity>();

            //ASSERT
            Assert.IsTrue(Regex.IsMatch(result.Email,
                 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                 RegexOptions.IgnoreCase));
        }

        [Test]
        public void Should_Generate_Property_With_Const_Value()
        {
            //ARRANGE

            var vehicleMapping = new EntityConfiguration<VehicleEntity>();
            vehicleMapping.For(x => x.Name).WithConstValue("TestName");
            vehicleMapping.For(x => x.IsActive).WithConstValue(true);
            vehicleMapping.For(x => x.CreatedOn).WithConstValue(new DateTime(2015, 01,01));
            vehicleMapping.For(x => x.IntKilometers).WithConstValue(500);
            vehicleMapping.For(x => x.IsDeactivated).WithConstValue(null);

            var sut = new SimpleAutoDataGenerator();

            sut.WithConfiguration(vehicleMapping);

            //ACT
            var result = sut.Fixture.Create<VehicleEntity>();

            //ASSERT
            Assert.That(result.Name, Is.EqualTo("TestName"));
            Assert.That(result.IsActive, Is.EqualTo(true));
            Assert.That(result.CreatedOn, Is.EqualTo(new DateTime(2015, 01, 01)));
            Assert.That(result.IntKilometers, Is.EqualTo(500));
            Assert.That(result.IsDeactivated, Is.EqualTo(null));
        }
    }
}
