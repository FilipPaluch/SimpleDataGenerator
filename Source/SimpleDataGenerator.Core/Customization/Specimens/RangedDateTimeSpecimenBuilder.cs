using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;
using SimpleDataGenerator.Core.Model;

namespace SimpleDataGenerator.Core.Customization.Specimens
{
    public class RangedDateTimeSpecimenBuilder : ISpecimenBuilder
    {
        private readonly PropertyInfo _selectedProperty;
        private readonly DateTimeRange _dateTimeRange;
        private readonly Random _random;
        public RangedDateTimeSpecimenBuilder(PropertyInfo propertyInfo, DateTimeRange dateTimeRange)
        {
            _selectedProperty = propertyInfo;
            _dateTimeRange = dateTimeRange;
            _random = new Random();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (IsSelectedProperty(request))
            {
                return GenerateRandomDateFromRange(_dateTimeRange);
            }

            return new NoSpecimen();
        }

        private bool IsSelectedProperty(object request)
        {
            var property = request as PropertyInfo;
            return property != null && property.Equals(_selectedProperty);
        }


        public DateTime GenerateRandomDateFromRange(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
                throw new ArgumentOutOfRangeException(String.Format("From date {0} can not be greater than to date {1}", fromDate, toDate));

            var randTimeSpan = new TimeSpan((long)(_random.NextDouble() * (toDate - fromDate).Ticks));

            return fromDate + randTimeSpan;
        }

        public DateTime GenerateRandomDateFromRange(DateTimeRange dateRange)
        {
            return GenerateRandomDateFromRange(dateRange.FromDate, dateRange.ToDate);
        }
    }
}
