using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleDataGenerator.Core.Customization.Specimens;
using SimpleDataGenerator.Core.Mapping.Interfaces;
using SimpleDataGenerator.Core.Model;

namespace SimpleDataGenerator.Core.Mapping.Implementations
{
    public class DateTimePropertyConfiguration : PropertyConfiguration<DateTime>, IDateTimePropertyConfiguration
    {
        public DateTimePropertyConfiguration(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            
        }

        public void InRange(DateTime fromDate, DateTime toDate)
        {
            AddCustomSpecimen(new RangedDateTimeSpecimenBuilder(SelectedProperty, new DateTimeRange(fromDate, toDate)));
        } 
    }
}
