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
    public class RangedNumberSpecimenBuilder : ISpecimenBuilder
    {
        private readonly PropertyInfo _selectedProperty;
        private readonly NumberRange _numberRange;
        public RangedNumberSpecimenBuilder(PropertyInfo propertyInfo, NumberRange numberRange)
        {
            _selectedProperty = propertyInfo;
            _numberRange = numberRange;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (IsSelectedProperty(request))
            {
                var propertyType = _selectedProperty.PropertyType;
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                var expectedType = underlyingType ?? propertyType;
                var value = context.Resolve(new RangedNumberRequest(expectedType, _numberRange.Minimum, _numberRange.Maximum));
                return value;
            }

            return new NoSpecimen();
        }

        private bool IsSelectedProperty(object request)
        {
            var property = request as PropertyInfo;
            return property != null && property.Equals(_selectedProperty);
        }
    }
}
