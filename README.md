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

~~~
    public class Vehicle
    {
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Kilometers { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
~~~
