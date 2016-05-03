# SimpleDataGenerator
Its primary goal is to allow developers to do easier Test-Driven Development by the useful extensions AutoFixture library.

Nuget: https://www.nuget.org/packages/SimpleDataGenerator.Core/

## Project Description

SimpleDataGenerator is AutoFixture library extension, makes it easier to define how to generate the selected properties.

The library allows define:
  - the range of the generated values for the numeric properties
  - the constant value
  - the length for [string] type property 
  - the range of lengths for [string] type property 
  - which properties should be empty
  - which properties should be generated as email

## Example



~~~
    public class Vehicle
    {
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Kilometers { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
    }
~~~

~~~

 public void Demo()
 {
     var conf = new EntityConfiguration<Vehicle>();

     conf.For(v => v.Name).WithConstValue("TestName");
     conf.For(v => v.Kilometers).InRange(10, 20000);
     conf.For(v => v.IsActive).WithConstValue(true);
     conf.For(v => v.Description).WithLengthRange(1, 255);
     conf.For(v => v.CreatedOn).InRange(new DateTime(2015, 01, 01), new DateTime(2016, 01, 01));
     conf.For(v => v.Comments).IsEmpty();

     var generator = new SimpleAutoDataGenerator();

     generator.WithConfiguration(conf);

     var result = generator.Fixture.Create<VehicleEntity>();

 }

~~~


