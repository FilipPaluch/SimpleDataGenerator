using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Kernel;

namespace SimpleDataGenerator.Core.Customization.Specimens
{
    public class ConstValueSpecimenBuilder : ISpecimenBuilder
    {
        private readonly PropertyInfo _selectedProperty;
        private readonly object _constValue;
        public ConstValueSpecimenBuilder(PropertyInfo propertyInfo, object constValue)
        {
            _selectedProperty = propertyInfo;
            _constValue = constValue;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (IsSelectedProperty(request))
            {
                return _constValue;
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
